using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BeaconMonitor : MonoBehaviour 
{
    public Text beaconDistanceText;
    public Text batteryIcon; 

    static readonly string BATTERY_ICON  = Char.ConvertFromUtf32(0xf243);

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
    void UpdateStatusIndicators()
    {
    	// set battery icon
		batteryIcon.text = BATTERY_ICON;

		// set beacon informations
		string uuid = "8492E75F-4FD6-469D-B132-04FE94921D8";
		string major = "12768";
		string minor = "3289";

		// invoke FarolBeaconPlugin
		var distance = FarolBeaconPlugin.GetDistance(uuid, major, minor);

//		var distance = FarolBeaconPlugin.GetText();


		// set beacon distance
		beaconDistanceText.text = distance;
    }
}
