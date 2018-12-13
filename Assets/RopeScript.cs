using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

    Rigidbody rbody;
    ConfigurableJoint conJoint;
    public GameObject startPoint;
    public Rigidbody playerRbody;
    public bool canShoot = true;
	// Use this for initialization
	void Start () {
        rbody = this.gameObject.GetComponent<Rigidbody>();
        conJoint = this.gameObject.GetComponent<ConfigurableJoint>();
        canShoot = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.X) && conJoint.connectedBody == null&&canShoot)
        {
            rbody.WakeUp();
            rbody.AddRelativeForce(1000 * Vector3.forward);
            canShoot = false;
        }
        //if (conJoint.connectedBody == null && !Input.GetKey(KeyCode.X)){
        //    this.transform.position = startPoint.transform.position;
        //}
        if (Input.GetKey(KeyCode.C))
        {
            conJoint.connectedBody = null;
            rbody.Sleep();
            canShoot = true;
        }

	}
	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.tag == "Untagged" && Input.GetKeyDown(KeyCode.X)&&canShoot){
            conJoint.connectedBody = collision.rigidbody;
            conJoint.xMotion = ConfigurableJointMotion.Limited;
            conJoint.yMotion = ConfigurableJointMotion.Limited;
            conJoint.zMotion = ConfigurableJointMotion.Limited;
            canShoot = false;
        }
	}
	private void OnCollisionStay(Collision collision)
	{
        Vector3 relaPos = this.gameObject.transform.position - startPoint.transform.position;
        if(relaPos.magnitude > 1){
            //playerRbody.velocity = relaPos.normalized * 100;
            playerRbody.AddForce(relaPos * 10);
        }

	}
}
