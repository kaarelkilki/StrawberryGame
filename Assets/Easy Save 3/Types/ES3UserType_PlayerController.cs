using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("count", "highScore", "timeRemaining", "extraTime")]
	public class ES3UserType_PlayerController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerController() : base(typeof(PlayerController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayerController)obj;
			
			writer.WriteProperty("count", instance.count, ES3Type_int.Instance);
			writer.WriteProperty("highScore", instance.highScore, ES3Type_int.Instance);
			writer.WriteProperty("timeRemaining", instance.timeRemaining, ES3Type_float.Instance);
			writer.WriteProperty("extraTime", instance.extraTime, ES3Type_float.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "count":
						instance.count = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "highScore":
						instance.highScore = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "timeRemaining":
						instance.timeRemaining = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "extraTime":
						instance.extraTime = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerControllerArray() : base(typeof(PlayerController[]), ES3UserType_PlayerController.Instance)
		{
			Instance = this;
		}
	}
}