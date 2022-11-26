using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    void Start()
    {
        if (Instance != null && Instance != this){
            DestroyImmediate(this);
        }
        else{
            Instance = this;
        }
    }

    public void TurnOffMusic(){
        
    }

    public void TurnOnMusic(){

    }
}
