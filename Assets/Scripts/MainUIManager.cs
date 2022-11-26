using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance;
    public Image pauseMenu;
    public Image upgradeMenu;
    public bool isMusicOn = true;

    void Start()
    {
        if (Instance != null && Instance != this){
            DestroyImmediate(this);
        }
        else{
            Instance = this;
        }
        //pauseMenu.gameObject.SetActive(false);
        //upgradeMenu.gameObject.SetActive(false);
    }

    public void OnUpgradePanelOpen(){
        upgradeMenu.gameObject.SetActive(true);
    }

    public void OnUpgradeMenuCloseButtonClicked(){
        upgradeMenu.gameObject.SetActive(false);
    }

    public void OnPauseButtonClicked(){
        pauseMenu.gameObject.SetActive(true);
    }

    public void OnPausePanelCloseButtonClicked(){
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
}
