using UnityEngine;
using System.Collections.Generic;

public class ControlHarpoon : MonoBehaviour
{
    List<Transform> caughtFishes;
    List<Transform> caughtTrashes;

    void Start(){
        caughtFishes = new List<Transform>();
        caughtTrashes = new List<Transform>();
    }
    public void CollectAllCaughtFishes(){
        foreach (var fish in caughtFishes){
            Debug.Log("Kill fish");
        }   
    }

    public void CollectAllCaughtTrashes(){
        foreach (var fish in caughtFishes){
            Debug.Log("Kill trash");
        } 
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.transform.CompareTag("Fish")){
            Debug.Log("Shot fish");
            col.transform.parent = transform;
            caughtFishes.Add(col.transform);
        }
        else if (col.transform.CompareTag("Trash")){
            Debug.Log("Shot trash");
            col.transform.parent = transform;
            caughtTrashes.Add(col.transform);
        }
    }
}
