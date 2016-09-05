using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Farol.Beacons {

	public class FarolBeaconUnityPlayer : IFarolBeacon {
		
		private static FarolBeaconUnityPlayer _instance;
		private static bool initialized = false;

		private FarolBeaconUnityPlayer ()
		{
			initialized = true;

		}

		public static FarolBeaconUnityPlayer Initialize ()
		{
			if (_instance == null) {
				_instance = new FarolBeaconUnityPlayer();
			}
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

			string strBeacon = String.Empty;
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

			string strNearestBeacon = String.Empty;

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
			return "0.0";
		}

		/// <summary>
		/// Find the current beacon list.
		/// </summary>
		public List<Beacon> GetBeacons ()
		{	
			if (!initialized) {
				Initialize();
			}

			return null;
		}

		private Beacon GetBeacon(string strBeacon)
		{
			if (!initialized) {
				Initialize();
			}

			return null;
		}
	}
}