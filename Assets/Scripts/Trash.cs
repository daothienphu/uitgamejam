using System.Collections;
using UnityEngine;

public class Trash : MonoBehaviour
{   
    public int coin = 1;
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

    public float minTimeToReachTarget = 10f;
    public float maxTimeToReachTarget = 20f; 

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
        if (endTarget.x > startPos.x){
            if (oriFacingLeft){
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }     
            else{
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else{
            if (oriFacingLeft)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else{
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
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
