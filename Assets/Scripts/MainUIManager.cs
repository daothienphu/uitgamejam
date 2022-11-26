using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance;
    public Image pauseMenu;
    public Image upgradeMenu;
    public bool isMusicOn = true;
    public Image losePanel;
    public Image winPanel;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI upgradePanelCoinText;
    void Start()
    {
        if (Instance != null && Instance != this){
            DestroyImmediate(this);
        }
        else{
            Instance = this;
        }
        pauseMenu.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        //upgradeMenu.gameObject.SetActive(false);
    }

    public void OnUpgradePanelOpen(){
        upgradeMenu.gameObject.SetActive(true);
        upgradePanelCoinText.text = $"$ {CoinManager.Instance.coin}";

        TextMeshProUGUI harpoonRangeText = upgradeMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI harpoonSpeedText = upgradeMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI shipSpeedText = upgradeMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        
        Button buyHarpoonRange = upgradeMenu.transform.GetChild(3).GetComponent<Button>();
        Button buyHarpoonSpeed = upgradeMenu.transform.GetChild(4).GetComponent<Button>();
        Button buyShipSpeed = upgradeMenu.transform.GetChild(5).GetComponent<Button>();
        
        harpoonRangeText.text = $"Harpoon range: {UpgradeManager.Instance.harpoonRange} -> {UpgradeManager.Instance.harpoonRange + 1}";
        harpoonSpeedText.text = $"Harpoon speed: {UpgradeManager.Instance.harpoonSpeed} -> {UpgradeManager.Instance.harpoonSpeed + 1}";
        shipSpeedText.text = $"Ship Speed: {UpgradeManager.Instance.shipSpeed} -> {UpgradeManager.Instance.shipSpeed + 1}";
        
        buyHarpoonRange.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Buy (${UpgradeManager.Instance.priceHarpoonRange})";
        buyHarpoonSpeed.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Buy (${UpgradeManager.Instance.priceHarpoonSpeed})";
        buyShipSpeed.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Buy (${UpgradeManager.Instance.priceShipSpeed})";

        int coin = CoinManager.Instance.coin;
        // if (coin < UpgradeManager.Instance.priceHarpoonRange){
        //     buyHarpoonRange.interactable = false;
        // }
        // else{
        //     buyHarpoonRange.interactable = true;
        // }

        // if (coin < UpgradeManager.Instance.priceHarpoonSpeed){
        //     buyHarpoonSpeed.interactable = false;
        // }
        // else{
        //     buyHarpoonSpeed.interactable = true;
        // }

        // if (coin < UpgradeManager.Instance.priceShipSpeed){
        //     buyShipSpeed.interactable = false;
        // }
        // else{
        //     buyShipSpeed.interactable = true;
        // }

    }

    public void OnUpgradeMenuCloseButtonClicked(){
        upgradeMenu.gameObject.SetActive(false);
    }

    public void OnPauseButtonClicked(){
        Time.timeScale = 0.1f;
        pauseMenu.gameObject.SetActive(true);
    }

    public void OnPausePanelCloseButtonClicked(){
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
    }

    public void OnMusicButtonClicked(){
        isMusicOn = !isMusicOn;
        if (!isMusicOn){
            MusicManager.Instance.TurnOffMusic();
        }
        else{
            MusicManager.Instance.TurnOnMusic();
        }
    }

    public void OnBackToMenuButtonClicked(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    public void OnLose(){
        Time.timeScale = 0.1f;
        losePanel.gameObject.SetActive(true);
    }

    public void OnWin(){
        Time.timeScale = 0.1f;
        winPanel.gameObject.SetActive(true);
    }

    void Update(){
        coinText.text = $"$ {CoinManager.Instance.coin}";
    }

    public void OnBuyHarpoonSpeed(){
        CoinManager.Instance.coin -= UpgradeManager.Instance.priceHarpoonSpeed;
        UpgradeManager.Instance.harpoonSpeed += 1;
        UpgradeManager.Instance.priceHarpoonSpeed += 5;
    }

    public void OnBuyHarpoonRange(){
        CoinManager.Instance.coin -= UpgradeManager.Instance.priceHarpoonRange;
        UpgradeManager.Instance.harpoonRange += 1;
        UpgradeManager.Instance.priceHarpoonRange += 10;
    }

    public void OnBuyShipSpeed(){
        CoinManager.Instance.coin -= UpgradeManager.Instance.priceShipSpeed;
        UpgradeManager.Instance.shipSpeed += 1;
        UpgradeManager.Instance.priceShipSpeed += 3;
    }
}
