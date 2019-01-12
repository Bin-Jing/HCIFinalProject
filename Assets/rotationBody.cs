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
        Vector3 rightDir = rightHand.localPosition - cameraTransform.localPosition;
        Vector3 leftDir = leftHand.localPosition - cameraTransform.localPosition;
        Vector3 mergeDir = (rightDir + leftDir) * 10;
        this.transform.rotation = Quaternion.LookRotation((new Vector3(mergeDir.x, 0, mergeDir.z)));
        //transform.rotation *= Quaternion.AngleAxis(-90f/*this might need to be positive*/, transform.forward);
    }
}
