using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    float Score = 0;
    PlayerHealth PH;
    GameObject[] enemys;
    public GameObject Victory;

	// Use this for initialization
	void Start () {
        PH = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemys.Length == 0) {
            Victory.SetActive(true);
        }

	}
    public void AddScore(float score){
        Score += score;
    }
}
