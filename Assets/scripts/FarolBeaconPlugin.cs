using UnityEngine;
using System;
using System.Collections;

public class FarolBeaconPlugin {

	public FarolBeaconPlugin ()
	{
		
	}

	/// <summary>
    /// Test parameter pass to android.
    /// </summary>
	public static string GetText()
	{
		if (Application.platform == RuntimePlatform.Android)
        {
            using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
					using (var androidPlugin = new AndroidJavaObject("com.menvia.farolbeacon.plugin.AndroidPlugin", currentActivity))
                    {
						return androidPlugin.Call<string>("getText", new object[]{"Passando parametro string"});
                    }
                }
            }
        }
	    return "Sem Retorno";
	}

	/// <summary>
    /// Get beacon distance information.
    /// </summary>
	public static string GetDistance(string uuid, string major, string minor)
	{
		if (Application.platform == RuntimePlatform.Android)
        {
            using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
					using (var androidPlugin = new AndroidJavaObject("com.menvia.farolbeacon.plugin.AndroidPlugin", currentActivity))
                    {
						return androidPlugin.Call<string>("getDistance", new object[]{uuid, major, minor});
                    }
                }
            }
        }
	    return "Beacon não encontrado";
	}
}
