﻿//
// HttpTest.cs
//
// Author:
//       Martin Baulig <martin.baulig@xamarin.com>
//
// Copyright (c) 2014 Xamarin Inc. (http://www.xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Xamarin.WebTests.Runners
{
	using Handlers;
	using Framework;

	public abstract class TestRunner
	{
		public abstract void Start ();

		public abstract void Stop ();

		protected abstract HttpWebRequest CreateRequest (Handler handler);

		static IPAddress address;

		public static IPAddress GetAddress ()
		{
			if (address == null)
				address = LookupAddress ();
			return address;
		}

		static IPAddress LookupAddress ()
		{
			try {
				#if __IOS__
				var interfaces = NetworkInterface.GetAllNetworkInterfaces ();
				foreach (var iface in interfaces) {
					if (iface.NetworkInterfaceType != NetworkInterfaceType.Ethernet && iface.NetworkInterfaceType != NetworkInterfaceType.Wireless80211)
						continue;
					foreach (var address in iface.GetIPProperties ().UnicastAddresses) {
						if (address.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback (address.Address))
							return address.Address;
					}
				}
				#else
				var hostname = Dns.GetHostName ();
				var hostent = Dns.GetHostEntry (hostname);
				foreach (var address in hostent.AddressList) {
					if (address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback (address))
						return address;
				}
				#endif
			} catch {
				;
			}

			return IPAddress.Loopback;
		}

		protected void Debug (int level, Handler handler, string message, params object[] args)
		{
			if (Handler.DebugLevel < level)
				return;
			var sb = new StringBuilder ();
			sb.AppendFormat ("{0}:{1}: {2}", this, handler, message);
			for (int i = 0; i < args.Length; i++) {
				sb.Append (" ");
				sb.Append (args [i] != null ? args [i].ToString () : "<null>");
			}
			Console.Error.WriteLine (sb.ToString ());
		}

		public void Run (Handler handler, HttpStatusCode expectedStatus = HttpStatusCode.OK)
		{
			Run (handler, expectedStatus, expectedStatus != HttpStatusCode.OK);
		}

		public void Run (Handler handler, HttpStatusCode expectedStatus, bool expectException)
		{
			Debug (0, handler, "RUN");

			var request = CreateRequest (handler);

			Debug (1, handler, "RUN #1", request.RequestUri);

			handler.SendRequest (request);

			try {
				var response = (HttpWebResponse)request.GetResponse ();
				Debug (1, handler, "GOT RESPONSE", response.StatusCode, response.StatusDescription);
				Assert.AreEqual (expectedStatus, response.StatusCode, "status code");
				Assert.IsFalse (expectException, "success status");

				using (var reader = new StreamReader (response.GetResponseStream ())) {
					var content = reader.ReadToEnd ();
					Debug (5, handler, "GOT RESPONSE BODY", content);
				}

				response.Close ();
			} catch (WebException wexc) {
				var response = (HttpWebResponse)wexc.Response;
				if (response == null) {
					Debug (0, handler, "RUN - GOT WEB EXCEPTION WITH NULL RESPONSE", wexc);
					Assert.Fail ("{0}:{1}: Got WebException will null response: {2}", this, handler, wexc);
					throw;
				}

				if (expectException) {
					Assert.AreEqual (expectedStatus, response.StatusCode, "error status code");
					response.Close ();
					return;
				}

				using (var reader = new StreamReader (response.GetResponseStream ())) {
					var content = reader.ReadToEnd ();
					Debug (0, handler, "RUN - GOT WEB EXCEPTION", wexc.Status, response.StatusCode, content, wexc);
					Assert.Fail ("{0}: {1}", handler, content);
				}
				response.Close ();
				throw;
			} catch (Exception ex) {
				Debug (0, handler, "RUN - GOT EXCEPTION", ex);
				throw;
			} finally {
				Debug (0, handler, "RUN DONE");
			}
		}

		protected virtual string MyToString ()
		{
			return null;
		}

		public override string ToString ()
		{
			var description = MyToString ();
			var padding = string.IsNullOrEmpty (description) ? string.Empty : ": ";
			return string.Format ("[{0}{1}{2}]", GetType ().Name, padding, description);
		}

	}
}

