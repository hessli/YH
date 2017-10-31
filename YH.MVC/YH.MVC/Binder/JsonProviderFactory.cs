using System;
using System.IO;
using System.Web;
using System.Dynamic;
using System.Web.Mvc;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YH.MVC.Binder
{
	public class JsonProviderFactory : ValueProviderFactory
	{
		private void AddToBackingStore (Dictionary<string, object> backingStore, string prefix, object value)
		{
			IDictionary<string, object> d = value as IDictionary<string, object>;
			if (d != null) {
				foreach (KeyValuePair<string, object> entry in d) {
					AddToBackingStore (backingStore, MakePropertyKey (prefix, entry.Key), entry.Value);
				}
				return;
			}

			IList l = value as IList;
			if (l != null) {
				for (int i = 0; i < l.Count; i++) {
					AddToBackingStore (backingStore, MakeArrayKey (prefix, i), l [i]);
				}
				return;
			}

			// primitive
			backingStore [prefix] = value;
		}

		private object GetDeserializedObject (ControllerContext controllerContext)
		{
            var isJson= controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.InvariantCultureIgnoreCase);
            if (!isJson)
            {
				return null;
			}
            StreamReader reader = new StreamReader (controllerContext.HttpContext.Request.InputStream);
			string bodyText = reader.ReadToEnd ();

			HttpRequestBase request = controllerContext.HttpContext.Request;
			
			if (String.IsNullOrEmpty (bodyText)) {
				return null;
			}

			var jsonData = JsonConvert.DeserializeObject<ExpandoObject> (bodyText);
			return jsonData;
		}

		public override IValueProvider GetValueProvider (ControllerContext controllerContext)
		{
			if (controllerContext == null) {
				throw new ArgumentNullException ("controllerContext");
			}

			object jsonData = GetDeserializedObject (controllerContext);
			if (jsonData == null) {
				return null;
			}

			Dictionary<string, object> backingStore = new Dictionary<string, object> (StringComparer.OrdinalIgnoreCase);
			AddToBackingStore (backingStore, String.Empty, jsonData);
			return new DictionaryValueProvider<object> (backingStore, CultureInfo.CurrentCulture);
		}

		private string MakeArrayKey (string prefix, int index)
		{
			return prefix + "[" + index.ToString (CultureInfo.InvariantCulture) + "]";
		}

		private string MakePropertyKey (string prefix, string propertyName)
		{
			return (String.IsNullOrEmpty (prefix)) ? propertyName : prefix + "." + propertyName;
		}
	}
}