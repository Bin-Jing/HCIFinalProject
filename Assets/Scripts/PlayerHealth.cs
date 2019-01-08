using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    float MaxHaelth = 1000;
    float currentHealth = 0;
    public GameManager GM;
    // Use this for initialization
    void Start()
    {
        currentHealth = MaxHaelth;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void getHurt(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            
        }
    }
    public float getHealth(){
        return currentHealth;
    }
}
