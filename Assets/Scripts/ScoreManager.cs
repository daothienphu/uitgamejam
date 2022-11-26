using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    int score = 0;
    public int minScore = -20;
    public int maxScore = 20;
    void Start()
    {
        if (Instance != null && Instance != this){
            DestroyImmediate(this);
        }    
        else{
            Instance = this;
        }
    }

    public void IncreaseScore(int amount = 1){
        score = Mathf.Min(score + amount, maxScore);    
    }
    
    public void DecreaseScore(int amount = 1){
        score = Mathf.Max(score - amount, minScore);
    }
}
