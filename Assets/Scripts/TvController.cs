using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class TvController : MonoBehaviour
{
    public static GameObject selectedTv;

    public static bool isTvOn = false;
    public VideoClip[] videoClips; // Array of video clips to play
    private VideoPlayer videoPlayer;
    private int currentChannelIndex = 0;

    bool prevFlag = false;

    bool nextFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        selectedTv = GameObject.Find("int-screen-hall");
        if(selectedTv != null){
            videoPlayer = selectedTv.GetComponent<VideoPlayer>();
        }
        selectedTv.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CharacterHandler.currentHit != null && CharacterHandler.currentHit.name.Contains("screen-hall")){
            if (Input.GetButton("js11"))
            {
                if(!prevFlag){
                    SwitchToPreviousChannel();
                    prevFlag = true;
                }
            }
            else{
                prevFlag = false;
            }
            
            if (Input.GetButton("js5")){
                if(!nextFlag){
                    SwitchToNextChannel();
                    nextFlag = true;
                }
            }
            else{
                nextFlag = false;
            }
        }
        
        
    }
    void SwitchToPreviousChannel()
    {
        Debug.Log("prev");
        currentChannelIndex = (currentChannelIndex - 1 + videoClips.Length) % videoClips.Length;
        PlayCurrentChannel();
    }

    void SwitchToNextChannel()
    {
        Debug.Log("next");
        currentChannelIndex = (currentChannelIndex + 1) % videoClips.Length;
        PlayCurrentChannel();
    }
    void PlayCurrentChannel()
    {
        if (videoPlayer != null && videoClips.Length > 0)
        {
            videoPlayer.Stop(); // Stop current video (if any)
            videoPlayer.clip = videoClips[currentChannelIndex];
            videoPlayer.Play();
        }
    }

    public static void onTv(){
        Debug.Log("on TV");
        if(selectedTv != null){
            selectedTv.SetActive(true);
            isTvOn = true;
        }
    }
    public static void offTv(){
        Debug.Log("off TV");
        if(selectedTv != null){
            selectedTv.SetActive(false);
            isTvOn = false;
        }
    }

    public static void HandleTvPower(){
        if(isTvOn){
            offTv();
        }
        else{
            onTv();
        }
    }

    public static void increaseVolume(){
        Debug.Log("Increase the volume");
        if(selectedTv != null && isTvOn){
             AudioSource audioSource = selectedTv.GetComponent<AudioSource>();
             if(audioSource != null){
                audioSource.volume += 0.2f;
             }
        }
    }
    public static void decreaseVolume(){
        Debug.Log("Decrease the volume");
        if(selectedTv != null && isTvOn){
            AudioSource audioSource = selectedTv.GetComponent<AudioSource>();
            if(audioSource != null){
                Debug.Log("wworkking");

                audioSource.volume -= 0.2f;
            }
        }
    }

    public static void muteTv(){
        Debug.Log("Mute the volume");
        if(selectedTv != null && isTvOn){
            GameObject hallScreen = GameObject.Find("int-screen-hall");
            if(hallScreen != null){
                AudioSource audioSource = hallScreen.GetComponent<AudioSource>();
                if(audioSource != null){
                    audioSource.volume = 0f;
                }
            }
             
        }
    }
}
