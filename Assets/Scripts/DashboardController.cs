using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void updateHallDashboard(){
        bool isLightOn = false;
        bool isFanOn = false;
        GameObject hall_Fan = GameObject.Find("int-fan-hall");
        FanStatus fanStatus = hall_Fan.GetComponent<FanStatus>();
        if(fanStatus != null){
            isFanOn = fanStatus.enabled;
        }
        GameObject hall_light = GameObject.Find("int-light-hall");
        Light lightStatus = hall_light.GetComponent<Light>();
        if(lightStatus != null){
            isLightOn = lightStatus.enabled;
        }

        GameObject hallFanStatus = GameObject.Find("hall-fan-status");
        GameObject hallLightStatus = GameObject.Find("hall-light-status");

        
        if (hallFanStatus != null && hallLightStatus != null)
        {
            
            TextMeshProUGUI FanText = hallFanStatus.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI LightText = hallLightStatus.GetComponent<TextMeshProUGUI>();

            if (FanText != null && LightText != null){
                FanText.text = isFanOn ? "ON" : "OFF";
                LightText.text = isLightOn ? "ON" : "OFF";
            }
            else
            {
                Debug.LogWarning("TextMeshPro component not found on the GameObject named 'status'.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject named 'hall-fan-status'/'hall-light-status' not found in the scene.");
        }
        
    }
    public static void updateBedroomDashboard(){
        bool isLightOn = false;
        bool isFanOn = false;
        GameObject bedroom_Fan = GameObject.Find("int-fan-bed");
        FanStatus fanStatus = bedroom_Fan.GetComponent<FanStatus>();
        if(fanStatus != null){
            isFanOn = fanStatus.enabled;
        }
        GameObject bedroom_light = GameObject.Find("int-light-bed");
        Light lightStatus = bedroom_light.GetComponent<Light>();
        if(lightStatus != null){
            isLightOn = lightStatus.enabled;
        }

        GameObject bedFanStatus = GameObject.Find("bed-fan-status");
        GameObject bedLightStatus = GameObject.Find("bed-light-status");

        
        if (bedFanStatus != null && bedLightStatus != null)
        {
            
            TextMeshProUGUI FanText = bedFanStatus.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI LightText = bedLightStatus.GetComponent<TextMeshProUGUI>();

            if (FanText != null && LightText != null){
                FanText.text = isFanOn ? "ON" : "OFF";
                LightText.text = isLightOn ? "ON" : "OFF";
            }
            else
            {
                Debug.LogWarning("TextMeshPro component not found on the GameObject named 'status'.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject named 'hall-fan-status'/'hall-light-status' not found in the scene.");
        }
        
    }
}
