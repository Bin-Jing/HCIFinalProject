using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    float MaxHaelth = 200;
    float currentHealth = 0;
    private Animator anim;
    public GameManager GM;
	// Use this for initialization
	void Start () {
        currentHealth = MaxHaelth;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void getHurt(float damage){
        currentHealth -= damage;
        print(currentHealth);
        if (currentHealth <= 0)
        {

            //do something
            GM.AddScore(100);
            this.gameObject.tag = "Untagged";
            anim.SetInteger("state", 4);
            GetComponent<TitanAI>().enabled = false;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(this.gameObject, 30f);
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
