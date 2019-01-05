using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text ScoreText;
    public Text PlayerHealthText;
    float Score = 0;
    public PlayerHealth PH;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ScoreText.text = "Score: "+ Score;
        PlayerHealthText.text = "Health: " + PH.getHealth();
	}
    public void AddScore(float score){
        Score += score;
    }
}
