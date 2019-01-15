using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckScript : MonoBehaviour {
    private EnemyHealth EH;
	private Animator anim;

	// Use this for initialization
	void Start () {
        EH = gameObject.GetComponentInParent<EnemyHealth>();
		anim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Weapon")){

            // shake VR controller
            other.transform.parent.gameObject.GetComponent<ControllerScript>().Shake(1000, 150);
            print(other.transform.parent);

            EH.getHurt(500);
        }
	}
}
