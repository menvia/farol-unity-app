using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Farol.Beacons {

	#if UNITY_ANDROID
	public class FarolBeaconAndroid : IFarolBeacon {

		private AndroidJavaObject androidPlugin;
		private NumberFormatInfo provider = new NumberFormatInfo();
		private static string[] ATTRIBUTE_SEPARATOR = new string[]{";"};
		private static string[] BEACON_SEPARATOR = new string[]{"|"};

		private static string androidUnityPlayerClass = "com.unity3d.player.UnityPlayer";
		private static string androidPluginClass = "com.menvia.farolbeacon.plugin.AndroidPlugin";

		private static FarolBeaconAndroid _instance;
		private static bool initialized = false;

		private FarolBeaconAndroid ()
		{
			Console.WriteLine ("vai inicializar");
			AndroidJavaClass javaUnityPlayer = new AndroidJavaClass(androidUnityPlayerClass);
			AndroidJavaObject currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			androidPlugin = new AndroidJavaObject(androidPluginClass, currentActivity);
			provider.NumberDecimalSeparator = ",";
			initialized = true;
			
		}

		public static FarolBeaconAndroid Initialize ()
		{
			if (_instance == null) {
				_instance = new FarolBeaconAndroid();
			}

			Console.WriteLine ("ja incializado");
			return _instance;
		}

		/// <summary>
	    /// Find the current beacon by uuid, major and minor.
	    /// </summary>
		public Beacon GetBeacon (string uuid, string major, string minor)
		{	
			if (!initialized) {
				Initialize();
			}
			Console.WriteLine("GetBeacon");
			string strBeacon = androidPlugin.Call<string> ("getStringBeacon", new object[]{ uuid, major, minor });
			return GetBeacon(strBeacon);
	    }
	
		/// <summary>
	    /// Find the nearest beacon.
	    /// </summary>
		public Beacon NearestBeacon ()
		{	
			if (!initialized) {
				Initialize();
			}
			Console.WriteLine("NearestBeacon");
			string strNearestBeacon = androidPlugin.Call<string>("nearestStringBeacon");
			return GetBeacon(strNearestBeacon);
	    }

	    /// <summary>
	    /// Find the current beacon distance by uuid, major and minor.
	    /// </summary>
		public string GetDistance (string uuid, string major, string minor)
		{
			if (!initialized) {
				Initialize();
			}
			Console.WriteLine("GetDistance");
			return androidPlugin.Call<string>("getStringDistance", new object[]{uuid, major, minor});
	    }

		/// <summary>
	    /// Find the current beacon list.
	    /// </summary>
		public List<Beacon> GetBeacons ()
		{	
			if (!initialized) {
				Initialize();
			}

			Console.WriteLine ("GetBeacons");

			List<Beacon> beacons = new List<Beacon> ();

			string strBeacons = androidPlugin.Call<string> ("getStringBeacons");

			if (strBeacons != "" && strBeacons != "NOT_FOUND"
				&& strBeacons != null && strBeacons != "null") {
			
				string[] arrBeacons = strBeacons.Split (BEACON_SEPARATOR, StringSplitOptions.None);

				foreach (string strBeacon in arrBeacons) {
					beacons.Add (GetBeacon (strBeacon));
				}
				Console.WriteLine("total in "+ beacons.Count);
				return beacons;
			}
	      	return null;
	    }

		private Beacon GetBeacon (string strBeacon)
		{
			Console.WriteLine("strBeacon >> "+strBeacon);
			if (strBeacon != "" && strBeacon != "NOT_FOUND"
			    && strBeacon != null && strBeacon != "null") {
			    string[] arrBeaconsAttributes = strBeacon.Split (ATTRIBUTE_SEPARATOR, StringSplitOptions.None);

				return new Beacon (
					arrBeaconsAttributes[0],
					Convert.ToInt32(arrBeaconsAttributes[1]),
					Convert.ToInt32(arrBeaconsAttributes[2]),
					Convert.ToDouble(arrBeaconsAttributes[3], provider));
			}
			return null;
		}
	}
	#endif
}