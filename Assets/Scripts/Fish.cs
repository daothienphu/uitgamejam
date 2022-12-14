using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{   
    float aspect = (float)Screen.width / Screen.height;
    float height;
    float width;
    Camera cam;
    public bool oriFacingLeft = true;

    float leftBound;
    float rightBound;

    float randomX;
    float randomY;
    float randomDuration;
    Coroutine co = null;

    public float minTimeToReachTarget = 2f;
    public float maxTimeToReachTarget = 5f;
    bool isCaught = false;
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize * 2;
        width = height * aspect;

        randomX = Random.Range(cam.transform.position.x - width / 2, cam.transform.position.x + width / 2);
        randomY = Random.Range(cam.transform.position.y - height / 2, cam.transform.position.y);
        randomDuration = Random.Range(minTimeToReachTarget, maxTimeToReachTarget);
    }

    void Update()
    {
        if (isCaught){
            StopAllCoroutines();
            co = null;
        }
        if (co == null && !isCaught){
            co = StartCoroutine(SwimToTarget());
        }
    }

    IEnumerator SwimToTarget(){
        randomX = Random.Range(cam.transform.position.x - width / 2, cam.transform.position.x + width / 2);
        randomY = Random.Range(cam.transform.position.y - height / 2, cam.transform.position.y);
        randomDuration = Random.Range(minTimeToReachTarget, maxTimeToReachTarget);
        float time = 0;
        Vector2 startPos = transform.position;
        Vector2 endTarget = new Vector2(randomX, randomY);
        Vector3 scale = transform.localScale;
        if (endTarget.x > startPos.x){
            if (oriFacingLeft){
                scale.x = -Mathf.Abs(scale.x);
            }     
            else{
                scale.x = Mathf.Abs(scale.x);
            }
        }
        else{
            if (oriFacingLeft)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else{
                scale.x = -Mathf.Abs(scale.x);
            }
        }
        transform.localScale = scale;
        while (time < randomDuration){
            transform.position = Vector2.Lerp(startPos, endTarget, time / randomDuration);
            time += Time.deltaTime;
            yield return null;    
        }
        transform.position = endTarget;
        co = null;
    }

    public void Respawn(){
        isCaught = false;
        transform.GetComponent<Collider2D>().enabled = true;
    }

    public void GetCaught(){
        gameObject.SetActive(false);
    }

    public void GetHooked(){
        isCaught = true;
        transform.GetComponent<Collider2D>().enabled = false;
    }
}
