using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckDetector : MonoBehaviour {
    RayPerception rayPer;
    float rayDistance = 500f;
    float[] rayAngles = new float[10];
    string[] detectableObjects = { "Untagged", "Neck" , "Enemy"};
    public RopeScript[] ropeS;
    public bool findEnemy = false;
	// Use this for initialization
	void Start () {
        rayPer = GetComponent<RayPerception>();
	}
	
	// Update is called once per frame
	void Update () {
        findEnemy = false;
        float rotationx = this.transform.eulerAngles.x;
        for (int i = 0; i < 10;i++){
            rayAngles[i] = i + 80;
        }
        if(rotationx > 180){
            rotationx -= 360;
        }

        //print(rotationx);
        rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, -rotationx);
        for (int i = 0; i < rayAngles.Length;i++){
            if (rayPer.GetHitObj()[i] != null&&rayPer.GetHitObj()[i].CompareTag("Enemy"))
            {
                findEnemy = true;
                GameObject hitItm = rayPer.GetHitObj()[i];
                Transform Neck = hitItm.transform.Find("Neck");
                Vector3 offset = Vector3.up * 20;
                for (int j = 0; j < ropeS.Length; j++)
                {
                    ropeS[j].NeckPos = Neck.position+offset;
                }
            }
        }

	}
}
