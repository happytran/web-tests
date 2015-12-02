﻿//
// DependencyInjector.cs
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
using System.Reflection;
using System.Collections.Generic;

namespace Xamarin.AsyncTests
{
	public static class DependencyInjector
	{
		static Dictionary<Type,object> dict = new Dictionary<Type,object> ();
		static Dictionary<string,object> assemblies = new Dictionary<string,object> ();
		static Dictionary<Type,object> extensionProviders = new Dictionary<Type,object> ();
		static Dictionary<Type,object> collections = new Dictionary<Type,object> ();
		static Dictionary<Type,Tuple<int,object>> defaults = new Dictionary<Type,Tuple<int,object>> ();
		static object syncRoot = new object ();

		static void Register<T> (T instance)
		{
			lock (syncRoot) {
				if (dict.ContainsKey (typeof (T)))
					throw new InvalidOperationException ();
				dict.Add (typeof (T), instance);
			}
		}

		public static void RegisterDependency<T> (Func<T> constructor)
		{
			lock (syncRoot) {
				if (dict.ContainsKey (typeof(T)))
					return;
				var instance = constructor ();
				dict.Add (typeof(T), instance);
			}
		}

		public static void RegisterDependency<T,U> (Func<T> constructor)
			where T : U
		{
			lock (syncRoot) {
				if (dict.ContainsKey (typeof(U)))
					return;
				var instance = constructor ();
				dict.Add (typeof(U), instance);
				dict.Add (typeof(T), instance);
			}
		}

		public static void RegisterAssembly (Assembly assembly)
		{
			lock (syncRoot) {
				var aname = assembly.FullName;
				if (assemblies.ContainsKey (aname))
					return;
				assemblies.Add (aname, assembly);
				foreach (var cattr in assembly.GetCustomAttributes<DependencyProviderAttribute> ()) {
					var provider = (IDependencyProvider)Activator.CreateInstance (cattr.Type);
					provider.Initialize ();
				}
			}
		}

		public static T Get<T> ()
		{
			lock (syncRoot) {
				if (!dict.ContainsKey (typeof(T)))
					throw new InvalidOperationException (string.Format ("Missing dependency: `{0}'", typeof(T)));
				return (T)dict [typeof(T)];
			}
		}

		public static object Get (Type type)
		{
			lock (syncRoot) {
				if (!dict.ContainsKey (type))
					throw new InvalidOperationException (string.Format ("Missing dependency: `{0}'", type));
				return dict [type];
			}
		}

		public static bool TryGet<T> (out T dependency)
			where T : class
		{
			lock (syncRoot) {
				object value;
				if (dict.TryGetValue (typeof(T), out value)) {
					dependency = (T)value;
					return true;
				}
				dependency = null;
				return false;
			}
		}

		public static bool IsAvailable (Type type)
		{
			lock (syncRoot) {
				return dict.ContainsKey (type);
			}
		}

		public static void RegisterCollection<T> (T item)
		{
			lock (syncRoot) {
				List<T> list;
				object value;
				if (collections.TryGetValue (typeof(T), out value))
					list = (List<T>)value;
				else {
					list = new List<T> ();
					collections.Add (typeof(T), list);
				}
				list.Add (item);
			}
		}

		public static ICollection<T> GetCollection<T> ()
		{
			lock (syncRoot) {
				object value;
				if (collections.TryGetValue (typeof(T), out value))
					return (List<T>)value;
				return new T [0];
			}
		}

		public static void RegisterExtension<T> (IExtensionProvider<T> provider)
		{
			lock (syncRoot) {
				if (extensionProviders.ContainsKey (typeof(T)))
					throw new InvalidOperationException ();
				extensionProviders.Add (typeof(T), provider);
			}
		}

		public static void RegisterExtension<T> (Func<T,IExtensionObject<T>> provider)
		{
			lock (syncRoot) {
				if (extensionProviders.ContainsKey (typeof(T)))
					throw new InvalidOperationException ();
				extensionProviders.Add (typeof (T), new ExtensionProvider<T> (provider));
			}
		}

		public static bool TryGetExtension<T> (out IExtensionProvider<T> provider)
		{
			lock (syncRoot) {
				object value;
				if (extensionProviders.TryGetValue (typeof(T), out value)) {
					provider = (IExtensionProvider<T>)value;
					return true;
				}
				provider = null;
				return false;
			}
		}

		public static E GetExtension<T,E> (T instance)
			where E : IExtensionObject<T>
		{
			IExtensionProvider<T> provider;
			if (!TryGetExtension (out provider))
				throw new InvalidOperationException ();
			return (E)provider.GetExtensionObject (instance);
		}

		class ExtensionProvider<T> : IExtensionProvider<T>
		{
			readonly Func<T,IExtensionObject<T>> provider;

			public ExtensionProvider (Func<T,IExtensionObject<T>> provider)
			{
				this.provider = provider;
			}

			public IExtensionObject<T> GetExtensionObject (T instance)
			{
				return provider (instance);
			}
		}

		public static bool RegisterDefaults<T> (int priority, T instance)
		{
			lock (syncRoot) {
				Tuple<int,object> value;
				if (!defaults.TryGetValue (typeof(T), out value)) {
					value = new Tuple<int,object> (priority, instance);
					defaults.Add (typeof(T), value);
					return true;
				}
				if (priority <= value.Item1)
					return false;
				value = new Tuple<int,object> (priority, instance);
				defaults [typeof(T)] = value;
				return true;
			}
		}

		public static T GetDefaults<T> (int minPriority = 0)
		{
			lock (syncRoot) {
				Tuple<int,object> value;
				if (!defaults.TryGetValue (typeof(T), out value))
					return default (T);
				if (value.Item1 < minPriority)
					return default (T);
				return (T)value.Item2;
			}
		}
	}
}

