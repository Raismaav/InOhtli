using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IGController : MonoBehaviour
{
    public void BackMenu(){
        Time.timeScale=1f;
        SceneManager.LoadScene("StartMenu");
    }
    public void exitGame(){
        Application.Quit();
    }
    public void DeathContinue(){
        SceneManager.LoadScene("Mictlan");
    }
}
