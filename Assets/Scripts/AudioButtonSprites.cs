using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButtonSprites : MonoBehaviour
{
    [SerializeField]
    private static Sprite OnState;
    
    [SerializeField]
    private static Sprite OffState;

    public static AudioButtonSprites Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this){
            DestroyImmediate(this);
        }
        else{
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Sprite GetOffState() {
        return OffState;
    }
    
    public static Sprite GetOnState() {
        return OnState;
    }
}
