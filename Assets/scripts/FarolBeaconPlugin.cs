using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class FarolBeaconPlugin {

	private AndroidJavaObject androidPlugin;
	private NumberFormatInfo provider = new NumberFormatInfo();
	private static string[] ATTRIBUTE_SEPARATOR = new string[]{";"};
	private string[] BEACON_SEPARATOR = new string[]{"|"};

	public FarolBeaconPlugin ()
	{
		initAndroid();
		provider.NumberDecimalSeparator = ",";
	}

	private void initAndroid ()
	{
		AndroidJavaClass javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		androidPlugin = new AndroidJavaObject("com.menvia.farolbeacon.plugin.AndroidPlugin", currentActivity);
	}

	/// <summary>
    /// Find the current beacon by uuid, major and minor.
    /// </summary>
	public Beacon GetBeacon (string uuid, string major, string minor)
	{	
		string strBeacon = androidPlugin.Call<string> ("getStringBeacon", new object[]{ uuid, major, minor });
		return GetBeacon(strBeacon);
    }

	/// <summary>
    /// Find the nearest beacon.
    /// </summary>
	public Beacon NearestBeacon ()
	{
		string strNearestBeacon = androidPlugin.Call<string>("nearestStringBeacon");
		return GetBeacon(strNearestBeacon);
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

	/// <summary>
    /// Find the current beacon distance by uuid, major and minor.
    /// </summary>
	public string GetDistance (string uuid, string major, string minor)
	{
		return androidPlugin.Call<string>("getStringDistance", new object[]{uuid, major, minor});
    }

	/// <summary>
    /// Find the current beacon list.
    /// </summary>
	public List<Beacon> GetBeacons ()
	{
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

	
}