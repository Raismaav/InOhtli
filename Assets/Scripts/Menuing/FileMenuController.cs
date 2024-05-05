using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
public class FileMenuController : MonoBehaviour
{
    private String Archive;
    public void FileContinue(){
        SceneManager.LoadScene("Mictlan");
    }
    public void FileNew(){
        Archive = Application.dataPath+"/game_data.json";
        if(File.Exists(Archive)){
            File.Delete(Archive);
        }
        SceneManager.LoadScene("Mictlan");
    }
}
