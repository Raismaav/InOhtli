using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    private AudioSource audioSource;
    [SerializeField] AudioClip GeneralHurt;
    private void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
        audioSource=GetComponent<AudioSource>();
    }
    public void SoundPlay(AudioClip sound){
        audioSource.PlayOneShot(sound);
    }
    public void SoundHurtPlay(){
        audioSource.PlayOneShot(GeneralHurt);
    }
}
