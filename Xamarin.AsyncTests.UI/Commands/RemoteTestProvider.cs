﻿//
// RemoteTestProvider.cs
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
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.AsyncTests.UI
{
	using Framework;

	public class RemoteTestProvider : TestProvider
	{
		TestServer server;
		IServerConnection connection;

		public RemoteTestProvider (TestApp app, TestServer server, IServerConnection connection)
			: base (app, connection.Name)
		{
			this.server = server;
			this.connection = connection;
		}

		internal override async Task<bool> Run (CancellationToken cancellationToken)
		{
			if (server != null)
				await server.Run (cancellationToken);
			return false;
		}

		internal override async Task Stop (CancellationToken cancellationToken)
		{
			if (server != null) {
				try {
					await server.Shutdown ();
				} catch {
					;
				}
				server.Stop ();
				server = null;
				connection = null;
			}

			if (connection != null) {
				connection.Close ();
				connection = null;
			}
		}

		public override async Task<TestSuite> LoadTestSuite (CancellationToken cancellationToken)
		{
			if (server == null)
				return null;
			return await server.StartServer (cancellationToken);
		}
	}
}
