using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    float MaxHaelth = 1000;
    public float currentHealth = 0;
    private Animator anim;
    public GameManager GM;
    public GameObject deathSteam;
    public AudioClip groaning;
    public AudioClip air;

    private Color alphaColor;
    private float timeToFade = 300.0f;
    private int deathCount;

    // Use this for initialization
    void Start () {
        currentHealth = MaxHaelth;
        anim = GetComponent<Animator>();
        alphaColor = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        alphaColor.a = 0;
        deathCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (deathCount > 0) {
            if (deathCount < 270){
                transform.position -= Vector3.up * 0.1f;
            }
            deathCount -= 1;
        }
		
	}
    public void getHurt(float damage){
        currentHealth -= damage;
        print(currentHealth);
        if (currentHealth <= 0)
        {

            //do something
            AudioSource audio = GetComponent<AudioSource>();
            audio.loop = false;
            audio.clip = air;
            audio.Play();
            GM.AddScore(100);
            this.gameObject.tag = "Untagged";
            anim.SetInteger("state", 4);
            GetComponent<TitanAI>().enabled = false;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            deathSteam.SetActive(true);
            deathCount = 450;
            Destroy(this.gameObject, 15f);
        }
        else {
            anim.SetInteger("state", 2);
            StartCoroutine(WaitAttackStop());
        }
    }
    public float getHealth(){
        return currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = groaning;
            audio.Play();
            getHurt(20);
        }
    }
    IEnumerator WaitAttackStop()
    {
        yield return null;
        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.IsTag("beAttacked") && (stateinfo.normalizedTime > 1.0f))
        {
            anim.SetInteger("state", 1);
        }
        else
        {
            StartCoroutine(WaitAttackStop());
        }
    }
}
