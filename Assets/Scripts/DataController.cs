using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    public GameObject Player;
    private String Archive;
    public ClassGameData gameData = new ClassGameData();
    private string keyWord = "Password";
    private void Awake(){
        Archive = Application.dataPath+"/game_data.json";
        Player = GameObject.FindGameObjectWithTag("Player");
        DataLoad();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            DataSave();
        }
        if(Input.GetKeyDown(KeyCode.G)){
            DataLoad();
        }
    }

    public void DataLoad(){
        if(File.Exists(Archive)){
            string RawData = File.ReadAllText(Archive);
            gameData = JsonUtility.FromJson<ClassGameData>(EncryptDecrypt(RawData));
            Debug.Log("Position "+gameData.position+ "Max hp: "+gameData.MaxHP+ "Current hp: "+gameData.HP);
            Player.transform.position = gameData.position;
            Player.GetComponent<Player>().setCurrentHP(gameData.HP);
            Player.GetComponent<Player>().setMaxHP(gameData.MaxHP);
            Player.GetComponent<Player>().setstartingValue(gameData.HP);
        }else{
            Debug.Log("El archivo no existe");
        }
    }

    public void DataSave(){
        ClassGameData newData= new ClassGameData(){
            position = Player.transform.position,
            HP = Player.GetComponent<HP>().getCurrentHP(),
            MaxHP = Player.GetComponent<HP>().getMaxHP(),
            Door1=false
        };
        string StringJson = JsonUtility.ToJson(newData);
        File.WriteAllText(Archive,EncryptDecrypt(StringJson));
        Debug.Log("Guardado");
    }
    private string EncryptDecrypt( string Data )
    {
        string result = "";

        for (int i = 0; i < Data.Length; i++)
            result += (char) ( Data[i] ^ keyWord[i % keyWord.Length] );
        
        return(result);
    }
}
