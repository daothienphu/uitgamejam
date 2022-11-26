using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public Image positive;
    public Image negative;
    public Image pointer;
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

    void Update(){
        Vector3 pointerPos = pointer.rectTransform.localPosition;
        pointerPos.x = score * 390 / maxScore;
        pointer.rectTransform.localPosition = pointerPos;

        if (score > 0){
            Vector3 scale = positive.rectTransform.localScale;
            scale.x = score;
            positive.rectTransform.localScale = scale;
        }
        else if (score < 0){
            Vector3 scale = negative.rectTransform.localScale;
            scale.x = score;
            negative.rectTransform.localScale = scale;
        }
        else{
            negative.rectTransform.localScale = new Vector3(0f, 1f, 1f);
            positive.rectTransform.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}
