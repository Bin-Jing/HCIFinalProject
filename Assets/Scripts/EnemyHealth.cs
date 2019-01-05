using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    float MaxHaelth = 1000;
    float currentHealth = 0;
    public GameManager GM;
	// Use this for initialization
	void Start () {
        currentHealth = MaxHaelth;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void getHurt(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){

            //do something
            GM.AddScore(100);
            this.gameObject.tag = "Untagged";
            Destroy(this.gameObject,3.0f);
        }
    }
    public float getHealth(){
        return currentHealth;
    }
}
