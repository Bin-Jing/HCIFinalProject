using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {
    public GameObject trackedObj;
    public GameObject teleportReticlePrefab;
    public Transform headTransform;

	private void Start()
	{
	}

	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 1000)){
            if(Vector3.Distance(headTransform.position,hit.point) > 10){
                teleportReticlePrefab.SetActive(true);
                teleportReticlePrefab.transform.position = hit.point;
                teleportReticlePrefab.transform.LookAt(headTransform);
            }
        }else
        {
            teleportReticlePrefab.SetActive(false);
        }

	}
}
