using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj
    {
        get { return this.GetComponent<SteamVR_TrackedObject>();  }
    }

    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public void Shake(ushort interval)
    {
        SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(interval);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
