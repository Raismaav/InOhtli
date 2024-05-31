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
    public DoorController dc;
    public GameObject semiBoss;
    public StartDialog StartDialog;
    private string keyWord = "Password";
    public GameObject bosstrigger1;
    private void Start(){
        Archive = Application.persistentDataPath+"/game_data.json";
        Player = GameObject.FindGameObjectWithTag("Player");
        DataLoad();
    }

    public void DataLoad(){
        if(File.Exists(Archive)){
            string RawData = File.ReadAllText(Archive);
            gameData = JsonUtility.FromJson<ClassGameData>(EncryptDecrypt(RawData));
            Debug.Log("Position "+gameData.position+ "Max hp: "+gameData.MaxHP+ "Current hp: "+gameData.HP);
            Player.transform.position = gameData.position;
            Player.GetComponent<Player>().setCurrentHP(gameData.HP);
            Player.GetComponent<Player>().setMaxHP(gameData.MaxHP);
            if(gameData.Door1){
                Player.GetComponent<Player>().unlockdash();
                dc.OpenDoor();
                Destroy(semiBoss);
                Destroy(bosstrigger1);
            }
            StartDialog.CheckFirst(gameData.firstPlay);
        }else{
            Debug.Log("El archivo no existe");
            StartDialog.CheckFirst(true);
        }
    }

    public void DataSave(){
        ClassGameData newData= new ClassGameData(){
            position = Player.transform.position,
            HP = Player.GetComponent<HP>().getCurrentHP(),
            MaxHP = Player.GetComponent<HP>().getMaxHP(),
            Door1=Player.GetComponent<Player>().DashCheck(),
            firstPlay=false
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
