using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Farol.Beacons;

namespace Farol.WS {

	public interface FarolWS {

		List<Beacon> GetBeacons ();

		List<Location> GetLocations ();

		List<Trigger> GetTriggers ();

		void checkin();

		void checkout();
	}
}