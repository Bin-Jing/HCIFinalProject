using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    float MaxHaelth = 1000;
    float currentHealth = 0;
    private Animator anim;
    public GameManager GM;
	// Use this for initialization
	void Start () {
        currentHealth = MaxHaelth;
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void getHurt(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){

            //do something
            GM.AddScore(100);
            //this.gameObject.tag = "Untagged";
            anim.SetInteger("state", 4);
            Destroy(this.gameObject,3f);
        }
    }
    public float getHealth(){
        return currentHealth;
    }
}
