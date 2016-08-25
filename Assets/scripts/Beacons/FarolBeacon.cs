using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Farol.Beacons {

	public interface FarolBeacon {

		List<Beacon> GetBeacons ();

		Beacon GetBeacon (string uuid, string major, string minor);

		string GetDistance (string uuid, string major, string minor);

		Beacon NearestBeacon ();
	}
}