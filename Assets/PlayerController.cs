using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public GameObject rightRope;
    public GameObject leftRope;
    bool jumpable = true;
    Rigidbody rbody;
    Rigidbody rRopeRbody;
	// Use this for initialization
	void Start () {
        rbody = this.gameObject.GetComponent<Rigidbody>();
        rRopeRbody = rightRope.GetComponent<Rigidbody>();
        jumpable = true;
	}
	
	// Update is called once per frame
	void Update () {
        //Character control
        rbody.AddRelativeForce(1000 * Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime);
        this.transform.eulerAngles += new Vector3(0,100 * Input.GetAxis("Horizontal") * Time.deltaTime,0);
        //Jet
        if(Input.GetKey(KeyCode.Z)){
            rbody.AddRelativeForce(10000*Vector3.forward*Time.deltaTime);
            rbody.AddRelativeForce(5000 * Vector3.up * Time.deltaTime);
        }
        //shoot rope



        //Jump
        if(Input.GetAxis("Jump") != 0 && jumpable){

            rbody.AddRelativeForce(1000 * Vector3.up);
            jumpable = false;
        }

	}
	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.CompareTag("Ground")){
            jumpable = true;
        }
	}
	private void OnCollisionExit(Collision collision)
	{
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpable = false;
        }
	}
}
