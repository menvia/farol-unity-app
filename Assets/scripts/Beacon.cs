using UnityEngine;
using System.Collections;
using System; 

public class Beacon : IComparable<Beacon>
{
    private string uuid;
	private int major;
	private int minor;
	private double distance;
    
	public Beacon(string uuid, int major, int minor)
    {
        this.uuid = uuid;
        this.major = major;
		this.minor = minor;
    }

	public Beacon(string uuid, int major, int minor, double distance)
    {
		this.uuid = uuid;
        this.major = major;
		this.minor = minor;
		this.distance = distance;
    }

    public string Uuid {
		get {return uuid;}
    }

	public int Major {
		get {return major;}
    }

	public int Minor {
		get {return minor;}
    }

	public double Distance {
		get {return distance;}
    }

    public int CompareTo(Beacon other)
    {
        if(other == null)
        {
            return 1;
        }

		return Convert.ToInt32(this.distance) - Convert.ToInt32(other.distance);
    }
}