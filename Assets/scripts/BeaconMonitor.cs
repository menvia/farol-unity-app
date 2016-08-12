using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BeaconMonitor : MonoBehaviour 
{
    public Text beaconDistanceText;
	public FarolBeaconPlugin farolBeaconPlugin;

   	void Awake ()
	{
		print ("Awake");
		farolBeaconPlugin = new FarolBeaconPlugin();
	}

    void Start ()
	{
		print ("Start");
	}

	// Update is called once per frame
	void Update () 
    {
		print ("Update");
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
		string minor = "29";  

		// Get Distance
		var distance = farolBeaconPlugin.GetDistance(uuid, major, minor);
		beaconDistanceText.text = distance;

		// Get and print beacon 
		Beacon beacon = farolBeaconPlugin.GetBeacon(uuid, major, minor);
		print ("Beacon information " + beacon.Uuid + " " + beacon.Major + " " + beacon.Minor + " " + beacon.Distance );

		// Get and print nearest beacon 
		Beacon nearestBeacon = farolBeaconPlugin.NearestBeacon();
		print ("Nearest beacon information " + nearestBeacon.Uuid + " " + nearestBeacon.Major + " " + nearestBeacon.Minor + " " + nearestBeacon.Distance );

		// Get and print the beacons list 
		List<Beacon> beacons = farolBeaconPlugin.GetBeacons ();

		if (beacons != null) {
			Console.WriteLine("total out "+ beacons.Count);
			beacons.ForEach(beaconOfList => Console.WriteLine("Beacon information list uuid {0}, major {1}, minor {2}, distance {3}", 
				beaconOfList.Uuid , beaconOfList.Major, beaconOfList.Minor, beaconOfList.Distance ));
		}
    }
}
