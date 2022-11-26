using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance;
    public Image pauseMenu;
    public Image upgradeMenu;
    public bool isMusicOn = true;
    public Image losePanel;
    public Image winPanel;

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
}
