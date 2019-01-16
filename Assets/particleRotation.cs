using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleRotation : MonoBehaviour {
    public GameObject weapon;
    private ParticleSystem particle;
	// Use this for initialization
	void Start () {
        particle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        var main = particle.main;
        main.startRotationX = weapon.transform.rotation.x;
        main.startRotationY = weapon.transform.rotation.y;
        main.startRotationZ = (weapon.transform.rotation.z + (0.5f * Mathf.PI));
    }
}
