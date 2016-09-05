using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Farol.Beacons;

public class BeaconMonitor : MonoBehaviour 
{
    public Text beaconDistanceText;
    public IFarolBeacon farolBeacon;

   	void Awake ()
	{
		print ("Awake");

		switch(Application.platform)
		{
			case RuntimePlatform.Android:
				#if UNITY_ANDROID
				farolBeacon = FarolBeaconAndroid.Initialize();
				#endif
				break;
			case RuntimePlatform.IPhonePlayer:
//				farolBeacon = FarolBeaconiOS.Initialize(); setar IPHONE Init
				break;
			default:
				farolBeacon = FarolBeaconUnityPlayer.Initialize();
				break;
		}
	}

    void Start ()
	{
		print ("Start");
	}

	// Update is called once per frame
	void Update () 
    {
		UpdateStatusIndicators();
	}

 	/// <summary>
    /// Find the current beacon device battery level and update indicators in the 
    /// UI accordingly.
    /// </summary>
    void UpdateStatusIndicators ()
	{
		// beacon configuration with parameters 
		string uuid = "64657665-6c6f-7064-6279-6d656e766961";
		string major = "4";
		string minor = "40";  

		// Get Distance
		var distance = farolBeacon.GetDistance(uuid, major, minor);
		beaconDistanceText.text = distance;

		// Get and print beacon 
		Beacon beacon = farolBeacon.GetBeacon (uuid, major, minor);
		if (beacon != null) {
			Console.WriteLine ("Beacon information " + beacon.Uuid + " " + beacon.Major + " " + beacon.Minor + " " + beacon.Distance);
		}

		// Get and print nearest beacon 
		Beacon nearestBeacon = farolBeacon.NearestBeacon ();
		if (nearestBeacon != null) {
			Console.WriteLine ("Nearest beacon information " + nearestBeacon.Uuid + " " + nearestBeacon.Major + " " + nearestBeacon.Minor + " " + nearestBeacon.Distance);
		}

		// Get and print the beacons list 
		List<Beacon> beacons = farolBeacon.GetBeacons ();
	
		if (beacons != null) {
			Console.WriteLine("total out "+ beacons.Count);
			beacons.ForEach(beaconOfList => Console.WriteLine("Beacon information list uuid {0}, major {1}, minor {2}, distance {3}", 
				beaconOfList.Uuid , beaconOfList.Major, beaconOfList.Minor, beaconOfList.Distance ));
		}
    }
}