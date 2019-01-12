using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Steam_VR_InteractionSystem;

public class RopeScript : MonoBehaviour {

    Rigidbody rbody;
    //ConfigurableJoint conJoint;
    public GameObject startPoint;
    public Rigidbody playerRbody;
    public GameObject player;
    public NeckDetector ND;
    Vector3 relaPos;
    public Vector3 NeckPos;
    public GameObject controller;
    // Use this for initialization
    // 1
    public SteamVR_TrackedObject trackedObj;
    //// 2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    // void Start () {
        // rbody = this.gameObject.GetComponent<Rigidbody>();
        // //conJoint = this.gameObject.GetComponent<ConfigurableJoint>();
	// }
	
	void Awake() 
	{
		rbody = this.gameObject.GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.X) && conJoint.connectedBody == null)
        //{
        //    rbody.WakeUp();
        //    rbody.AddRelativeForce(1000 * Vector3.forward);
        //}
        if (Input.GetKey(KeyCode.X)
            || Controller.GetHairTrigger())
        {
            rbody.WakeUp();
            if(ND.findEnemy){
                rbody.AddForce(10000 * (NeckPos - startPoint.transform.position));
            }else{

                rbody.AddRelativeForce(10000 * (Quaternion.Inverse(player.transform.rotation) * controller.transform.forward));
            }

        }else{
            this.transform.position = startPoint.transform.position;
            this.transform.rotation = controller.transform.rotation;
        }
        //if (conJoint.connectedBody == null && !Input.GetKey(KeyCode.X)){
        //    this.transform.position = startPoint.transform.position;
        //}
        if (Input.GetKeyUp(KeyCode.X)
            || Controller.GetHairTriggerUp()
           )
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
            playerRbody.AddForce((relaPos+offset) * 100 * Time.deltaTime);
        }
        if(this.transform.parent != null){
            if (this.transform.parent.CompareTag("Enemy"))
            {
                if (this.transform.parent.GetComponent<EnemyHealth>().getHealth() < 0)
                {
                    this.transform.parent = null;
                    rbody.Sleep();
                    transform.parent = player.transform;
                }
            }
        }


	}
	private void OnCollisionEnter(Collision collision)
	{
        if((collision.gameObject.tag == "Untagged" 
            || collision.gameObject.tag == "Enemy" 
            || collision.gameObject.tag == "Neck") && (Input.GetKey(KeyCode.X) || Controller.GetHairTrigger())){
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
	private void OnCollisionStay(Collision collision)
	{
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Neck")
        {
            if (collision.gameObject.GetComponent<EnemyHealth>().getHealth() > 0)
            {
                transform.parent = collision.transform;
            }
            else
            {
                transform.parent = null;
            }

        }
	}

}
