using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public int countPos = 0;
    public int countNeg = 0;
    public Image positive;
    public Image negative;
    public Image pointer;
    public int minScore = -20;
    public int maxScore = 20;
    public Image scoreBar;

    public Transform plusOne;
    public Transform minusOne;

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

        countPos = Mathf.Min(countPos + amount, maxScore);    
    }
    
    public void DecreaseScore(int amount = 1){
        countNeg = Mathf.Max(countNeg - amount, minScore);
    }

    void Update(){       
        Vector3 scale = positive.rectTransform.localScale;
        scale.x = countPos;
        positive.rectTransform.localScale = scale;
       
        scale = negative.rectTransform.localScale;
        scale.x = -countNeg;
        negative.rectTransform.localScale = scale;
        if (countPos >= maxScore){
            MainUIManager.Instance.OnWin();
        }
        if (countNeg <= minScore){
            MainUIManager.Instance.OnLose();
        }
    }

    IEnumerator FloatingScore(Transform img) {
        float duration = 0.5f;
        float time = 0;
        Transform imgInstance = Instantiate(img, transform.position + new Vector3(Random.Range(-3f, 4f), Random.Range(-3f, 4f), 0f), Quaternion.identity);
        while (time < duration) {
            imgInstance.position += Vector3.up * Time.deltaTime * 2f;
            time += Time.deltaTime;
            yield return null;    
        }
        DestroyImmediate(imgInstance);
    }
}
