using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IGController : MonoBehaviour
{
    [SerializeField] GameObject ControlsMenu;
    public void BackMenu(){
        Time.timeScale=1f;
        SceneManager.LoadScene("StartMenu");
    }
    public void ControlMenu(){
        ControlsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void exitGame(){
        Application.Quit();
    }
    public void DeathContinue(){
        SceneManager.LoadScene("Mictlan");
    }
}
