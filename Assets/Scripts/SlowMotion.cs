using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

    private GameObject[] enemies;
    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
        float scale = 1;
        foreach (GameObject target in enemies)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 100)
            {
                // perform attack on target  
                scale = 0.5f;
            }
        }
        Time.timeScale = scale;
    }
}
