using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationBody : MonoBehaviour {
    public Transform cameraTransform;
    public Transform rightHand;
    public Transform leftHand;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rightDir = rightHand.position - cameraTransform.position;
        Vector3 leftDir = leftHand.position - cameraTransform.position;
        Vector3 mergeDir = (rightDir + leftDir);
        this.transform.rotation = Quaternion.LookRotation((new Vector3(mergeDir.x, 0, mergeDir.z)).normalized);
        //transform.rotation *= Quaternion.AngleAxis(-90f/*this might need to be positive*/, transform.forward);
        transform.position = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
    }
}
