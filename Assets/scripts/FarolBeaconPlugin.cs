using UnityEngine;
using System;
using System.Collections;

public class FarolBeaconPlugin {

	private AndroidJavaObject androidPlugin;

	public FarolBeaconPlugin ()
	{
		AndroidJavaClass javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		androidPlugin = new AndroidJavaObject("com.menvia.farolbeacon.plugin.AndroidPlugin", currentActivity);
	}

	public string GetText()
	{
		return androidPlugin.Call<string>("getText", new object[]{"Passando parametro string"});
    }

	public string GetDistance(string uuid, string major, string minor)
	{
		return androidPlugin.Call<string>("getDistance", new object[]{uuid, major, minor});
    }
}
