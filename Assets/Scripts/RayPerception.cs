using System.Collections.Generic;
using UnityEngine;

public class RayPerception : MonoBehaviour{
    List<float> perceptionBuffer = new List<float>();
    Vector3 endPosition;
    RaycastHit hit;
    GameObject[] hitObj;
    string[] hitTag;
    public List<float> Perceive(float rayDistance,
        float[] rayAngles, string[] detectableObjects,
        float startOffset, float endOffset){
        hitTag = new string[rayAngles.Length];
        hitObj = new GameObject[rayAngles.Length];
        perceptionBuffer.Clear();
        foreach (float angle in rayAngles){
            endPosition = transform.TransformDirection(PolarToCartesian(rayDistance, angle));
            endPosition.y = endOffset;
            if (Application.isEditor){
                Debug.DrawRay(transform.position + new Vector3(0f, startOffset, 0f),
                        endPosition, Color.black, 0.01f, true);
            }

            float[] subList = new float[detectableObjects.Length + 2];
            if (Physics.SphereCast(transform.position +
                                       new Vector3(0f, startOffset, 0f), 0.5f,
                    endPosition, out hit, rayDistance)){
                for (int i = 0; i < detectableObjects.Length; i++){
                    if (hit.collider.gameObject.CompareTag(detectableObjects[i])){
                        hitObj[i] = hit.collider.gameObject;
                        hitTag[i] = hitObj[i].tag;
                        subList[i] = 1;
                        subList[detectableObjects.Length + 1] = hit.distance / rayDistance;
                        break;
                    }
                }
            }
            else{
                subList[detectableObjects.Length] = 1f;
            }

                perceptionBuffer.AddRange(subList);
        }

        return perceptionBuffer;
    }
    public string[] GetTag(){
        return hitTag;
    }
    public GameObject[] GetHitObj()
    {
        return hitObj;
    }
    public static Vector3 PolarToCartesian(float radius, float angle){
        float x = radius * Mathf.Cos(DegreeToRadian(angle));
        float z = radius * Mathf.Sin(DegreeToRadian(angle));
        return new Vector3(x, 0f, z);
    }
    public static float DegreeToRadian(float degree){
        return degree * Mathf.PI / 180f;
    }
}

