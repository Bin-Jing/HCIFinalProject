using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    private int interval;
    private ushort amplitude;

    private SteamVR_TrackedObject trackedObj
    {
        get { return this.GetComponent<SteamVR_TrackedObject>();  }
    }

    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public void Shake(ushort _amplitude, int _interval)
    {
        interval = _interval;
        amplitude = _amplitude;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (interval != 0) {
            SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(amplitude);
            interval -= 1;
        }	
	}
}
