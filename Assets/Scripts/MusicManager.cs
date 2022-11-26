using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource AudioSource;

    [SerializeField]
    private AudioClip[] BackgroundMusics;

    void Start()
    {
        if (Instance != null && Instance != this){
            DestroyImmediate(this);
        }
        else{
            Instance = this;
        }

        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = BackgroundMusics[0];
        AudioSource.Play();
    }

    public void TurnOffMusic(){
        AudioSource.volume = 0;
    }

    public void TurnOnMusic(){
        AudioSource.volume = 1;
    }
}
