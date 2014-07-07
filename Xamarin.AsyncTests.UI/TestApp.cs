﻿//
// App.cs
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
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.AsyncTests.UI
{
	using Framework;

	public class TestApp : INotifyPropertyChanged
	{
		public Assembly Assembly {
			get;
			private set;
		}

		public TestContext Context {
			get;
			private set;
		}

		public MainPage MainPage {
			get;
			private set;
		}

		public Page Root {
			get;
			private set;
		}

		public TestResultModel RootTestResult {
			get;
			private set;
		}

		public TestRunnerModel RootTestRunner {
			get;
			private set;
		}

		string statusMessage;
		public string StatusMessage {
			get { return statusMessage; }
			set {
				statusMessage = value;
				OnPropertyChanged ("StatusMessage");
			}
		}

		TestRunnerModel currentRunner;
		public TestRunnerModel CurrentTestRunner {
			get { return currentRunner; }
			set {
				currentRunner = value;
				OnPropertyChanged ("CurrentTestRunner");
				OnPropertyChanged ("CanRun");
				OnPropertyChanged ("CanStop");
				OnPropertyChanged ("IsStopped");
			}
		}

		bool running;
		public bool IsRunning {
			get { return running; }
			set {
				running = value;
				OnPropertyChanged ("IsRunning");
				OnPropertyChanged ("CanStop");
				OnPropertyChanged ("CanRun");
				OnPropertyChanged ("IsStopped");
			}
		}

		public bool CanStop {
			get { return running; }
		}

		public bool CanRun {
			get { return !running && CurrentTestRunner.Test != null; }
		}

		public bool IsStopped {
			get { return !running; }
		}

		public ISettingsHost SettingsHost {
			get;
			private set;
		}

		public IServerHost ServerHost {
			get;
			private set;
		}

		public OptionsModel Options {
			get;
			private set;
		}

		public ServerControlModel ServerControl {
			get;
			private set;
		}

		public ServerControlPage ServerControlPage {
			get;
			private set;
		}

		public TestApp (ISettingsHost settings, IServerHost server, Assembly assembly)
		{
			SettingsHost = settings;
			ServerHost = server;
			Assembly = assembly;

			Context = new TestContext ();
			Context.TestFinishedEvent += (sender, e) => OnTestFinished (e);

			var result = new TestResult (new TestName (null));
			RootTestResult = new TestResultModel (this, result);
			RootTestRunner = currentRunner = new TestRunnerModel (this, RootTestResult, true);

			ServerControl = new ServerControlModel (this);
			ServerControlPage = new ServerControlPage (ServerControl);

			Options = new OptionsModel (this, Context.Configuration);

			MainPage = new MainPage (this);
			Root = new NavigationPage (MainPage);

			Task.Factory.StartNew (async () => {
				try {
					await ServerControl.Initialize ();
				} catch {
					;
				}
			});
		}

		public OptionsPage GetOptionsPage ()
		{
			return new OptionsPage (Options);
		}

		internal async Task LoadAssembly (CancellationToken cancellationToken)
		{
			var suite = await ServerControl.LoadTestSuite ();
			if (suite == null)
				return;

			OnPropertyChanged ("CanRun");
		}

		internal void ClearAll ()
		{
			ServerControl.UnloadTestSuite ();
			Clear ();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

		CancellationTokenSource cancelCts;
		string message;

		internal async void Run (bool repeat)
		{
			if (!CanRun || IsRunning)
				return;

			cancelCts = new CancellationTokenSource ();
			IsRunning = true;

			Context.ResetStatistics ();

			try {
				message = "Running";
				StatusMessage = GetStatusMessage ();
				await CurrentTestRunner.Run (repeat, cancelCts.Token);
				message = "Done";
			} catch (OperationCanceledException) {
				message = "Canceled!";
			} catch (Exception ex) {
				message = string.Format ("ERROR: {0}", ex.Message);
			} finally {
				IsRunning = false;
				StatusMessage = GetStatusMessage ();
				cancelCts.Dispose ();
				cancelCts = null;
			}
		}

		internal void Stop ()
		{
			if (!IsRunning || cancelCts == null)
				return;

			cancelCts.Cancel ();
		}

		void OnTestFinished (TestResult result)
		{
			StatusMessage = GetStatusMessage ();
		}

		internal void Clear ()
		{
			Context.ResetStatistics ();
			message = null;
			StatusMessage = GetStatusMessage ();
		}

		string GetStatusMessage ()
		{
			if (ServerControl.TestSuite == null)
				return "No test loaded.";
			var sb = new StringBuilder ();
			sb.AppendFormat ("{0} tests passed", Context.CountSuccess);
			if (Context.CountErrors > 0)
				sb.AppendFormat (", {0} errors", Context.CountErrors);
			if (Context.CountIgnored > 0)
				sb.AppendFormat (", {0} ignored", Context.CountIgnored);

			if (message != null)
				return string.Format ("{0} ({1})", message, sb);
			else
				return sb.ToString ();
		}
	}
}
