﻿//
// ParameterAttributeHost.cs
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
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.AsyncTests.Framework.Reflection
{
	class ParameterAttributeTestHost : ParameterizedTestHost
	{
		public TestParameterSourceAttribute Attribute {
			get;
			private set;
		}

		public string Filter {
			get;
			private set;
		}

		public ParameterAttributeTestHost (string name, TypeInfo type, TestParameterSourceAttribute attr)
			: base (name, type)
		{
			Attribute = attr;
			Flags |= attr.Flags;

			var paramAttr = attr as TestParameterAttribute;
			if (paramAttr != null)
				Filter = paramAttr.Filter;
		}

		internal override TestInstance CreateInstance (TestContext context, TestInstance parent)
		{
			var instanceType = typeof(ParameterSourceInstance<>).GetTypeInfo ();
			var genericInstance = instanceType.MakeGenericType (ParameterType.AsType ());
			return (ParameterizedTestInstance)Activator.CreateInstance (
				genericInstance, this, parent, Attribute.SourceType, Filter);
		}
	}
}
