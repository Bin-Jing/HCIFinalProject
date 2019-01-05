using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckScript : MonoBehaviour {
    EnemyHealth EH;
	// Use this for initialization
	void Start () {
        EH = gameObject.GetComponentInParent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Weapon")){
            EH.getHurt(500);
        }
	}
}
