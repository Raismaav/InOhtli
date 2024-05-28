using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField] private TMP_Text LogText;
    [SerializeField, TextArea(4,6)] private string[] LogLines;
    private int LineIndex=0;
    [SerializeField] private GameObject Container;
    private bool firstPlay;
    void Update(){
        if(Input.GetButtonDown("Fire1")){

            if(LogText.text==LogLines[LineIndex]){
                NextLogLine();
            }
            else{
                StopAllCoroutines();
                LogText.text=LogLines[LineIndex];
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            if(firstPlay){
                Time.timeScale=0;
                Container.SetActive(true);
                LogText.text="Finalmente he llegado al Tepectli Monamictlán, gracias a la ayuda de mi leal Xoloitzcuintle. El, me permitió cruzar el temido Chicnahuapan, río de los nueve cielos.";
            }else{
                LineIndex=0;
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator ShowLine(){
        LogText.text=string.Empty;
        foreach (char ch in LogLines[LineIndex])
        {
            LogText.text += ch;
            yield return new WaitForSecondsRealtime(0.03f);
        }
    }
    private void NextLogLine(){
        LineIndex++;
        if(LineIndex<LogLines.Length){
            StartCoroutine(ShowLine());
        }else{
            Container.SetActive(false);
            Time.timeScale=1;
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            LogText.text="";
            Destroy(gameObject);
        }
    }
    public void CheckFirst(bool first){
        firstPlay=first;
    }
}
