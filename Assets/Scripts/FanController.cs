using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public static GameObject selectedFan;
    public static float fanSpeedHall = 100f;
    public static float fanSpeedBedroom = 100f;
    public static float fanSpeedStudyroom = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public static void onFan(){
        Debug.Log("on fan");
        if(selectedFan != null){
            
            FanStatus FanStatusScript = selectedFan.GetComponent<FanStatus>();

            // Enable the FanStatus script if it's not null
            if (FanStatusScript != null)
            {
                FanStatusScript.enabled = true;
            }
            else
            {
                Debug.LogError("FanStatus script not found on the specified GameObject.");
            }
        }
       
        CharacterHandler.exitFanMenu();
    }
    public static void offFan(){
        Debug.Log("off fan");
        if(selectedFan != null){
            FanStatus FanStatusScript = selectedFan.GetComponent<FanStatus>();

            // Enable the FanStatus script if it's not null
            if (FanStatusScript != null)
            {
                FanStatusScript.enabled = false;
            }
            else
            {
                Debug.LogError("FanStatus script not found on the specified GameObject.");
            }
        }
       
        CharacterHandler.exitFanMenu();
    }

    public static void increaseSpeed(){
        Debug.Log("Increasing fan speed");
        if(selectedFan != null){
            if(selectedFan.name.Contains("hall")){
                fanSpeedHall += 100f;
            }
            else if(selectedFan.name.Contains("bed")){
                fanSpeedBedroom += 100f;
            }
            else if(selectedFan.name.Contains("study")){
                fanSpeedStudyroom += 100f;
            }
        }
        
    }
    public static void decreaseSpeed(){
        Debug.Log("Decreasing fan speed");
        if(selectedFan != null){
            if(selectedFan.name.Contains("hall")){
                fanSpeedHall -= 100f;
            }
            else if(selectedFan.name.Contains("bed")){
                fanSpeedBedroom -= 100f;
            }
            else if(selectedFan.name.Contains("study")){
                fanSpeedStudyroom -= 100f;
            }
        }
    }

}
