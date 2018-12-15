using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

    Rigidbody rbody;
    //ConfigurableJoint conJoint;
    public GameObject startPoint;
    public Rigidbody playerRbody;
    public GameObject player;
    Vector3 relaPos;
	// Use this for initialization
	void Start () {
        rbody = this.gameObject.GetComponent<Rigidbody>();
        //conJoint = this.gameObject.GetComponent<ConfigurableJoint>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.X) && conJoint.connectedBody == null)
        //{
        //    rbody.WakeUp();
        //    rbody.AddRelativeForce(1000 * Vector3.forward);
        //}
        if (Input.GetKey(KeyCode.X))
        {
            rbody.WakeUp();
            rbody.AddRelativeForce(1000 * Vector3.forward);
        }else{
            this.transform.position = startPoint.transform.position;
        }
        //if (conJoint.connectedBody == null && !Input.GetKey(KeyCode.X)){
        //    this.transform.position = startPoint.transform.position;
        //}
        if (Input.GetKeyUp(KeyCode.X))
        {
            //conJoint.xMotion = ConfigurableJointMotion.Free;
            //conJoint.yMotion = ConfigurableJointMotion.Free;
            //conJoint.zMotion = ConfigurableJointMotion.Free;
            //conJoint.connectedBody = null;
            rbody.Sleep();
            rbody.constraints = RigidbodyConstraints.FreezeRotation;
            transform.parent = player.transform;

        }
        this.transform.eulerAngles = player.transform.eulerAngles;
        relaPos = this.gameObject.transform.position - startPoint.transform.position;
        if(transform.parent == null && relaPos.magnitude > 0.1f){
            playerRbody.AddForce(relaPos * 100 * Time.deltaTime);
        }

	}
	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.tag == "Untagged" && Input.GetKey(KeyCode.X)){
            rbody.constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = null;

            //conJoint.connectedBody = collision.rigidbody;
            //relaPos = this.gameObject.transform.position - startPoint.transform.position;
            //conJoint.xMotion = ConfigurableJointMotion.Limited;
            //conJoint.yMotion = ConfigurableJointMotion.Limited;
            //conJoint.zMotion = ConfigurableJointMotion.Limited;
        }
	}
	
}
