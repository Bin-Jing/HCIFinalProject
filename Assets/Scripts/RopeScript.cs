using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

    Rigidbody rbody;
    //ConfigurableJoint conJoint;
    public GameObject startPoint;
    public Rigidbody playerRbody;
    public GameObject player;
    public NeckDetector ND;
    Vector3 relaPos;
    public Vector3 NeckPos;
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
            if(ND.findEnemy){
                rbody.AddForce(1000 * (NeckPos - startPoint.transform.position));
            }else{
                rbody.AddRelativeForce(1000 * Vector3.forward);
            }

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
        if (this.transform.parent != null&&(ND.findEnemy || this.transform.parent.CompareTag("Enemy")))
        {
            if(this.transform.parent.CompareTag("Enemy")){
                NeckPos = this.transform.parent.Find("Neck").position;
            }
            relaPos = NeckPos - startPoint.transform.position;
        }
        else{
            relaPos = this.gameObject.transform.localPosition - startPoint.transform.position;
        }

        if((transform.parent == null||transform.parent.tag == "Enemy") && relaPos.magnitude > 1f){
            Vector3 offset = Vector3.up * 20;
            playerRbody.AddForce((relaPos+offset) * 150 * Time.deltaTime);
        }

	}
	private void OnCollisionEnter(Collision collision)
	{
        if((collision.gameObject.tag == "Untagged" 
            || collision.gameObject.tag == "Enemy" 
            || collision.gameObject.tag == "Neck") && Input.GetKey(KeyCode.X)){
            rbody.constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = null;
            if(collision.gameObject.tag == "Enemy"|| collision.gameObject.tag == "Neck"){
                if(collision.gameObject.GetComponent<EnemyHealth>().getHealth() > 0){
                    transform.parent = collision.transform;
                }else{
                    transform.parent = null;
                }

            }
            //conJoint.connectedBody = collision.rigidbody;
            //relaPos = this.gameObject.transform.position - startPoint.transform.position;
            //conJoint.xMotion = ConfigurableJointMotion.Limited;
            //conJoint.yMotion = ConfigurableJointMotion.Limited;
            //conJoint.zMotion = ConfigurableJointMotion.Limited;
        }
	}
	
}
