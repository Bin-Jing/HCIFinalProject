﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanAI : MonoBehaviour{
    
    public float traceMaxTimeScale;
    public AudioClip walkingAudio;
    public AudioClip attackAudio;


    private GameObject player;
    private float traceTime;
    private NavMeshAgent agent;
    private Vector3 wanderPoint;
    private float wanderRadius;
    private Animator anim;
    bool attacking = false;
    private int attackCD;
    PlayerHealth PH;
    Rigidbody playerRbody;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("Player");
        PH = player.GetComponent<PlayerHealth>();
        playerRbody = player.GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent> ();
        traceTime = 0;
        wanderRadius = this.transform.localScale.x * 10;
        anim = GetComponent<Animator>();
        attackCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        attacking = false;
        if (attackCD != 0) {
            attackCD -= 1;
        }
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        Vector3 titanToPlayer = (player.transform.position - this.transform.position);
        Vector3 faceTo = this.transform.forward;
        Vector3 titanToPlayerVertical = new Vector3(titanToPlayer.x, 0, titanToPlayer.z);
        //print(titanToPlayerVertical);
        float verticalAngle = Mathf.Acos(Vector3.Dot(titanToPlayerVertical.normalized, faceTo.normalized)) * Mathf.Rad2Deg;
        //print("dist" + dist);
        //print("verticalAngle" + verticalAngle);
        if(dist < wanderRadius && verticalAngle < 120f){
            traceTime = traceMaxTimeScale * Time.deltaTime;
        }
        
        if (traceTime > 0){
            Vector3 dir = (player.transform.position - transform.position).normalized * wanderRadius * 0.1f;
            if(Vector3.Distance(transform.position, player.transform.position) > wanderRadius * 0.2f){
                agent.SetDestination(player.transform.position - dir);
                StartCoroutine(WaitAttackStop());
            }
            else{
                anim.SetInteger("state", 3);
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = attackAudio;
                audio.Play();
                attacking = true;
            }
            traceTime -= Time.deltaTime;
        }
        else{
            //print(wanderPoint);
            //print("dist   " + Vector3.Distance(transform.position, wanderPoint));
            if(wanderPoint == Vector3.zero || Vector3.Distance(transform.position, wanderPoint) < (wanderRadius * 0.1)){
                wanderPoint = RandomWanderPoint();
            }
            agent.SetDestination(wanderPoint);
        }
    }

    Vector3 RandomWanderPoint(){
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player") && attacking) {
            Vector3 relaPos = this.gameObject.transform.localPosition - player.transform.position;
            playerRbody.AddForce(relaPos * -50000 * Time.deltaTime);
        }
        if (other.CompareTag("Weapon") && attackCD == 0) {
            other.transform.parent.gameObject.GetComponent<ControllerScript>().Shake(500, 60);
            PH.getHurt(200);
            attackCD = 300;
        }
	}

    IEnumerator WaitAttackStop()
    {
        yield return null;
        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.IsTag("attack") && (stateinfo.normalizedTime > 1.0f))
        {
            anim.SetInteger("state", 1);
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = walkingAudio;
            audio.Play();
        }
        else
        {
            StartCoroutine(WaitAttackStop());
        }
    }
}
