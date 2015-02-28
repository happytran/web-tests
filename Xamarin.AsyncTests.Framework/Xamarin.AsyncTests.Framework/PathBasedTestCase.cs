﻿//
// PathBasedTestCase.cs
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
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.AsyncTests.Framework
{
	class PathBasedTestCase : TestCase
	{
		public TestSuite Suite {
			get;
			private set;
		}

		public TestName Name {
			get;
			private set;
		}

		public TestPathNode Node {
			get;
			private set;
		}

		ITestPath TestCase.Path {
			get { return Node.Path; }
		}

		public PathBasedTestCase (TestPathNode node)
		{
			Node = node;
			Suite = node.Tree.Builder.Suite;
			Name = node.Path.Name;
		}

		public bool HasParameters {
			get { return Node.HasParameters; }
		}

		public Task<IReadOnlyCollection<TestCase>> GetParameters (TestContext ctx, CancellationToken cancellationToken)
		{
			return Task.Run<IReadOnlyCollection<TestCase>> (() => ResolveParameters (ctx));
		}

		public Task<IReadOnlyCollection<TestCase>> GetChildren (CancellationToken cancellationToken)
		{
			return Task.Run<IReadOnlyCollection<TestCase>> (() => ResolveChildren ());
		}

		List<PathBasedTestCase> parameters;
		List<PathBasedTestCase> children;

		List<PathBasedTestCase> ResolveParameters (TestContext ctx)
		{
			if (parameters != null)
				return parameters;

			Node.Resolve (ctx);
			parameters = new List<PathBasedTestCase> ();
			AddParameters (ctx, Node);
			return parameters;
		}

		List<PathBasedTestCase> ResolveChildren ()
		{
			if (children != null)
				return children;

			Node.Resolve ();
			children = new List<PathBasedTestCase> ();
			AddChildren (Node);
			return children;
		}

		void AddChildren (TestPathNode node)
		{
			foreach (var child in node.GetChildren ()) {
				if (child.Path.IsHidden || !child.Path.IsBrowseable) {
					AddChildren (child);
				} else {
					children.Add (new PathBasedTestCase (child));
				}
			}
		}

		void AddParameters (TestContext ctx, TestPathNode node)
		{
			foreach (var child in node.GetParameters (ctx)) {
				if (child.Path.IsHidden || !child.Path.IsBrowseable) {
					AddParameters (ctx, child);
				} else {
					parameters.Add (new PathBasedTestCase (child));
				}
			}
		}

		public async Task<TestResult> Run (TestContext ctx, CancellationToken cancellationToken)
		{
			TestSerializer.Debug ("RUN: {0}", this);

			var result = new TestResult (Name);
			var childCtx = ctx.CreateChild (Name, result);

			var invoker = Node.CreateInvoker (ctx);
			var ok = await invoker.Invoke (childCtx, null, cancellationToken);

			if (!ok && result.Status == TestStatus.Success)
				result.Status = TestStatus.Error;

			TestSerializer.Debug ("RUN DONE: {0} {1}", ok, result);

			return result;
		}

		public XElement Serialize ()
		{
			return TestSerializer.SerializePath (Node.Path);
		}

		public override string ToString ()
		{
			return string.Format ("[PathBasedTestCase: {0}]", Node);
		}
	}
}

