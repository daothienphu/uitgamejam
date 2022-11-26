using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartUIController : MonoBehaviour
{
    EventSystem es;
    public Button playbutton; 
    public Image creditsPanel = null;
    void Start(){
        es = EventSystem.current;
        if (creditsPanel !!= null){
            creditsPanel.gameObject.SetActive(false);
        }
        es.SetSelectedGameObject(playbutton.gameObject);
        InputController.Instance.playerInput.SwitchCurrentActionMap("UI");
    }    
    public void OnPlayButtonClicked(){
        InputController.Instance.playerInput.SwitchCurrentActionMap("Player");
        SceneManager.LoadScene("SampleScene");
    }

    public void OnCreditsButtonClicked(){
        creditsPanel.gameObject.SetActive(true);
        es.SetSelectedGameObject(creditsPanel.transform.GetChild(0).gameObject);
    }

    public void OnClosedCreditsButtonClicked(){
        creditsPanel.gameObject.SetActive(false);
        es.SetSelectedGameObject(playbutton.gameObject);
    }

    public void OnQuitButtonClicked(){
        Application.Quit();
    }

}
