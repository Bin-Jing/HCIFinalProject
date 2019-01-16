using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySlashAudio : MonoBehaviour {
    public AudioClip neckSlash;
    public AudioClip bodySlash;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Neck"))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = neckSlash;
            audio.Play();
        }
        if (other.CompareTag("Enemy"))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = bodySlash;
            audio.Play();
        }
    }

}
