using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{   
    float aspect = (float)Screen.width / Screen.height;
    float height;
    float width;
    Camera cam;


    float leftBound;
    float rightBound;

    float randomX;
    float randomY;
    float randomDuration;
    Coroutine co = null;
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize * 2;
        width = height * aspect;

        randomX = Random.Range(cam.transform.position.x - width / 2, cam.transform.position.x + width / 2);
        randomY = Random.Range(cam.transform.position.y - height / 2, cam.transform.position.y);
        randomDuration = Random.Range(2f, 5f);
    }

    void Update()
    {
        if (co == null){
            co = StartCoroutine(SwimToTarget());
        }
    }

    IEnumerator SwimToTarget(){
        randomX = Random.Range(cam.transform.position.x - width / 2, cam.transform.position.x + width / 2);
        randomY = Random.Range(cam.transform.position.y - height / 2, cam.transform.position.y);
        randomDuration = Random.Range(2f, 5f);
        float time = 0;
        Vector2 startPos = transform.position;
        Vector2 endTarget = new Vector2(randomX, randomY);
        while (time < randomDuration){
            transform.position = Vector2.Lerp(startPos, endTarget, time / randomDuration);
            time += Time.deltaTime;
            yield return null;    
        }
        transform.position = endTarget;
        co = null;
    }

    public void GetCaught(){
        
    }
}
