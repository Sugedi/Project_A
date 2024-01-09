using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("datas")]
	public class ES3UserType_DataManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_DataManager() : base(typeof(DataManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (DataManager)obj;
			
			writer.WriteProperty("datas", instance.datas, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(Datas)));
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (DataManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "datas":
						instance.datas = reader.Read<Datas>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_DataManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_DataManagerArray() : base(typeof(DataManager[]), ES3UserType_DataManager.Instance)
		{
			Instance = this;
		}
	}
}