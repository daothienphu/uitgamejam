using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int coin = 0;
    void Start() {
        if (Instance != null && Instance != this) {
            DestroyImmediate(this);
        }
        else {
            Instance = this;
        }
    }

    public void IncreaseCoin(int amount = 1){
        coin += amount;
    }

    public void DecreaseCoin(int amount = 1){
        coin = Mathf.Max(coin - amount, 0);
    }
}
