using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    float aspect = (float)Screen.width / Screen.height;
    Camera cam;
    float height;
    float width;
    
    public List<Sprite> prefabs;
    public int maxNumOfActiveFishes; 
    public int numOfActiveFishes;
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize * 2;
        width = height * aspect;

        
    }

    void Update()
    {
        numOfActiveFishes = GetComponentsInChildren<Transform>().GetLength(0);
        if (numOfActiveFishes < maxNumOfActiveFishes){
            for (int i = numOfActiveFishes; i < maxNumOfActiveFishes; ++i){
                bool spawnOnTheLeft = Random.Range(0, 100) > 49;
                if (spawnOnTheLeft){
                    Vector3 spawnPoint = new Vector3(cam.transform.position.x - width / 2 - 40f, Random.Range(cam.transform.position.y - height / 2, cam.transform.position.y), 0f);
                    
                }
            }
        }
    }
}
