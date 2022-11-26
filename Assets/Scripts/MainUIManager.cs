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

    public TextMeshProUGUI coinTextUpgrade;
    public TextMeshProUGUI coinText;
    TextMeshProUGUI harpoonSpeedText;
    TextMeshProUGUI harpoonRangeText;
    TextMeshProUGUI shipSpeedText;
    Button buyHarpoonSpeed;
    Button buyHarpoonRange;
    Button buyShipSpeed;

    [SerializeField]
    private Image audioButtonImage;

    [SerializeField]
    private Sprite audioButtonOnSprite;

    [SerializeField]
    private Sprite audioButtonOffSprite;

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
        
        harpoonSpeedText = upgradeMenu.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        harpoonRangeText = upgradeMenu.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        shipSpeedText = upgradeMenu.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        
        buyHarpoonSpeed = upgradeMenu.transform.GetChild(0).GetComponent<Button>();
        buyHarpoonRange = upgradeMenu.transform.GetChild(1).GetComponent<Button>();
        buyShipSpeed = upgradeMenu.transform.GetChild(2).GetComponent<Button>();
        upgradeMenu.gameObject.SetActive(false);
    }

    public void OnUpgradePanelOpen(){
        UpdateUpgradePanelInfo();
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

            audioButtonImage.sprite = audioButtonOffSprite;
        }
        else{
            MusicManager.Instance.TurnOnMusic();

            audioButtonImage.sprite = audioButtonOnSprite;
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
        UpgradeManager.Instance.priceHarpoonSpeed += 5;
        UpgradeManager.Instance.harpoonSpeed += 1;
        UpdateUpgradePanelInfo();
    }
    public void OnBuyHarpoonRange(){
        CoinManager.Instance.coin -= UpgradeManager.Instance.priceHarpoonRange;
        UpgradeManager.Instance.priceHarpoonRange += 5;
        UpgradeManager.Instance.harpoonRange += 1;
        UpdateUpgradePanelInfo();
    }
    public void OnBuyShipSpeed(){
        CoinManager.Instance.coin -= UpgradeManager.Instance.priceShipSpeed;
        UpgradeManager.Instance.priceShipSpeed += 5;
        UpgradeManager.Instance.shipSpeed += 1;
        UpdateUpgradePanelInfo();
    }

    void UpdateUpgradePanelInfo(){
        upgradeMenu.gameObject.SetActive(true);
        harpoonSpeedText.text = $"Harpoon speed: {UpgradeManager.Instance.harpoonSpeed} -> {UpgradeManager.Instance.harpoonSpeed + 1}";
        harpoonRangeText.text = $"Harpoon range: {UpgradeManager.Instance.harpoonRange} -> {UpgradeManager.Instance.harpoonRange + 1}";
        shipSpeedText.text = $"Ship speed: {UpgradeManager.Instance.shipSpeed} -> {UpgradeManager.Instance.shipSpeed + 1}";

        buyHarpoonRange.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Buy (${UpgradeManager.Instance.priceHarpoonRange})";
        buyHarpoonSpeed.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Buy (${UpgradeManager.Instance.priceHarpoonSpeed})";
        buyShipSpeed.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Buy (${UpgradeManager.Instance.priceShipSpeed})";

        int coin = CoinManager.Instance.coin;
        if (coin < UpgradeManager.Instance.priceHarpoonRange){
            buyHarpoonRange.interactable = false;
        }
        else{
            buyHarpoonRange.interactable = true;
        }
        
        if (coin < UpgradeManager.Instance.priceHarpoonSpeed){
            buyHarpoonSpeed.interactable = false;
        }
        else{
            buyHarpoonSpeed.interactable = true;
        }

        if (coin < UpgradeManager.Instance.priceShipSpeed){
            buyShipSpeed.interactable = false;
        }
        else{
            buyShipSpeed.interactable = true;
        }

        coinTextUpgrade.text = $"$ {CoinManager.Instance.coin}";
    }
}
