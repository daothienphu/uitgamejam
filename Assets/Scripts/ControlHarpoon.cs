using UnityEngine;
using System.Collections.Generic;

public class ControlHarpoon : MonoBehaviour
{
    List<Transform> caughtFishes;
    List<Transform> caughtTrashes;
    public Transform fishes;
    public Transform trashes;

    void Start(){
        caughtFishes = new List<Transform>();
        caughtTrashes = new List<Transform>();
    }
    public void CollectAllCaughtFishes(){
        foreach (var fish in caughtFishes){
            fish.GetComponent<Fish>().GetCaught();
            ScoreManager.Instance.DecreaseScore();
            CoinManager.Instance.IncreaseCoin(fish.GetComponent<Fish>().coin);
        }   
        caughtFishes.Clear();
        for (int i = transform.childCount - 1;  i >= 0; --i){
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Fish")){
                child.parent = fishes;
            }
        }
        transform.GetComponent<Collider2D>().enabled = true;
    }

    public void CollectAllCaughtTrashes(){
        foreach (var trash in caughtTrashes){
            trash.GetComponent<Trash>().GetCaught();
            ScoreManager.Instance.IncreaseScore();
            CoinManager.Instance.IncreaseCoin(trash.GetComponent<Trash>().coin);
        } 
        caughtTrashes.Clear();
        for (int i = transform.childCount - 1;  i >= 0; --i){
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Trash")){
                child.parent = trashes;
            }
        }
        transform.GetComponent<Collider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.transform.CompareTag("Fish")){
            col.transform.parent = transform;
            col.transform.GetComponent<Fish>().GetHooked();
            caughtFishes.Add(col.transform);
            transform.GetComponent<Collider2D>().enabled = false;
        }
        else if (col.transform.CompareTag("Trash")){
            col.transform.parent = transform;
            col.transform.GetComponent<Trash>().GetHooked();
            caughtTrashes.Add(col.transform);
            transform.GetComponent<Collider2D>().enabled = false;
        }
    }
}
