using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public float harpoonRange = 5f;
    public float harpoonSpeed = 9f;
    public float shipSpeed = 3f;

    public int priceHarpoonRange;
    public int priceHarpoonSpeed;
    public int priceShipSpeed;
    bool isUpgradeMenuOpen = false;
    void Start()
    {
        if (Instance != null && Instance != this){
            DestroyImmediate(this);
        }
        else{
            Instance = this;
        }
    }

    public void OnUpgradeKeyPressed(){
        isUpgradeMenuOpen = !isUpgradeMenuOpen;
        if (isUpgradeMenuOpen){
            MainUIManager.Instance.OnUpgradePanelOpen();
        }
        else{
            MainUIManager.Instance.OnUpgradeMenuCloseButtonClicked();
        }
    }

    public void OnShipSpeedIncrease(){
        shipSpeed += 1;
    }

    public void OnHarpoonSpeedIncrease(){
        harpoonSpeed += 1;
    }

    public void OnHarpoonRangeIncrease(){
        harpoonRange += 1;
    }
}
