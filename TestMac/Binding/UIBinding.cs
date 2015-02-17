﻿//
// UIBinding.cs
//
// Author:
//       Martin Baulig <martin.baulig@xamarin.com>
//
// Copyright (c) 2015 Xamarin, Inc.
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
using AppKit;
using Xamarin.AsyncTests.UI;

namespace TestMac
{
	public static class UIBinding
	{
		public static void Bind (Command command, NSButton button)
		{
			#if FIXME
			button.Activated += (sender, e) => {
				command.Execute ();
			};
			button.Enabled = command.NotifyStateChanged.State;
			#endif
			command.NotifyStateChanged.StateChanged += (sender, e) => {
				button.InvokeOnMainThread (() => button.Enabled = e);
			};
		}

		public static void Bind (Command command, NSMenuItem item)
		{
			item.Activated += (sender, e) => command.Execute ();
			command.NotifyStateChanged.StateChanged += (sender, e) => {
				item.InvokeOnMainThread (() => item.Enabled = e);
			};
			item.Enabled = command.NotifyStateChanged.State;
		}

		public static void Bind (Property<string> property, NSTextField label)
		{
			property.PropertyChanged += (sender, e) => {
				label.InvokeOnMainThread (() => label.StringValue = e);
			};
			label.StringValue = property.Value;
		}
	}
}

