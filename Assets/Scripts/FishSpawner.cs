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

    public List<Transform> allFishes;
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize * 2;
        width = height * aspect;      
        for (int i = 0; i < transform.childCount; ++i){
            allFishes.Add(transform.GetChild(i));
        }
    }

    void Update()
    {
        numOfActiveFishes = GetComponentsInChildren<Transform>().GetLength(0);
        if (numOfActiveFishes < maxNumOfActiveFishes){
            foreach (Transform t in allFishes){
                if (t.gameObject.activeSelf == false){
                    bool spawnOnTheLeft = Random.Range(0, 100) > 49;
                    Vector3 spawnPoint;
                    if (spawnOnTheLeft){
                        spawnPoint = new Vector3(cam.transform.position.x - width / 2 - 10f, Random.Range(cam.transform.position.y - height / 2, cam.transform.position.y), 0f);
                    }
                    else{
                        spawnPoint = new Vector3(cam.transform.position.x + width / 2 + 10f, Random.Range(cam.transform.position.y - height / 2, cam.transform.position.y), 0f);
                    }
                    t.position = spawnPoint;
                    t.gameObject.SetActive(true);
                    t.GetComponent<Fish>().Respawn();
                }
            }
        }
    }
}
