﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckScript : MonoBehaviour {
    private EnemyHealth EH;
	private Animator anim;

	// Use this for initialization
	void Start () {
        EH = gameObject.GetComponentInParent<EnemyHealth>();
		anim = gameObject.GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Weapon")){
            EH.getHurt(500);
			anim.SetInteger("state", 2);
        }
	}
}
