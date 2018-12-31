using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanAI : MonoBehaviour{
    
    public float wanderRadius;
    public float traceMaxTimeScale;
    private GameObject player;
    private float traceTime;
    private NavMeshAgent agent;
    private Vector3 wanderPoint;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.Find("player");
        agent = GetComponent<NavMeshAgent> ();
        traceTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        Vector3 titanToPlayer = (player.transform.position - this.transform.position);
        Vector3 faceTo = this.transform.forward;
        Vector3 titanToPlayerVertical = new Vector3(titanToPlayer.x, 0, titanToPlayer.z);
        //print(titanToPlayerVertical);
        float verticalAngle = Mathf.Acos(Vector3.Dot(titanToPlayerVertical.normalized, faceTo.normalized)) * Mathf.Rad2Deg;
        //print("dist" + dist);
        //print("verticalAngle" + verticalAngle);
        if(dist < wanderRadius && verticalAngle < 60f){
            traceTime = traceMaxTimeScale * Time.deltaTime;
        }
        if(traceTime > 0){
            agent.SetDestination(player.transform.position);
            traceTime -= Time.deltaTime;
        }
        else{
            //print(wanderPoint);
            if(wanderPoint == Vector3.zero || Vector3.Distance(transform.position, wanderPoint) < 2f){
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
}
