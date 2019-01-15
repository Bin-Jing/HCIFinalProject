using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Steam_VR_InteractionSystem;

public class PlayerController : MonoBehaviour {


    public GameObject rightRope;
    public GameObject leftRope;
    public GameObject body;
    // 1
    public SteamVR_TrackedObject trackedObj;
    //// 2

    private Quaternion rotation;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    bool jumpable = true;
    Rigidbody rbody;
    Rigidbody rRopeRbody;
    //Use this for initialization

    void Start () {
        rbody = this.gameObject.GetComponent<Rigidbody>();
        rRopeRbody = rightRope.GetComponent<Rigidbody>();
        jumpable = true;
        rotation = transform.rotation;

    }


    void Awake() 
	{ 	
		rbody = this.gameObject.GetComponent<Rigidbody>();
        rRopeRbody = rightRope.GetComponent<Rigidbody>();
        jumpable = true;
		
	}

	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
        //Character control
        //rbody.AddRelativeForce(1000 * Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime);
        this.transform.eulerAngles += new Vector3(0,100 * Input.GetAxis("Horizontal") * Time.deltaTime,0);
        this.transform.eulerAngles += new Vector3(-50 * Input.GetAxis("Vertical") * Time.deltaTime,0, 0);
        //this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        //Jet
        if(Input.GetKey(KeyCode.Z) 
           || Controller.GetHairTrigger()
          )
        {
            rbody.AddForce(1500 * (body.transform.forward * Time.deltaTime));
            rbody.AddForce(4000 * Vector3.up * Time.deltaTime);
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
