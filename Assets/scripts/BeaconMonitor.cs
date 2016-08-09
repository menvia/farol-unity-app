using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

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
    void UpdateStatusIndicators()
    {
		string uuid = "64657665-6c6f-7064-6279-6d656e766961";
		string major = "4";
		string minor = "29";  

		var distance = farolBeaconPlugin.GetDistance(uuid, major, minor);
//		var currentText = farolBeaconPlugin.GetText();

		print("current " +distance);

		beaconDistanceText.text = distance;
    }
}
