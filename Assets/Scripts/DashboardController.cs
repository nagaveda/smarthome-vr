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
        bool isTvOn = TvController.isTvOn;
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
        GameObject hallTvStatus = GameObject.Find("hall-tv-status");

        // Tv Handling

        if(hallTvStatus != null){
            TextMeshProUGUI TvText = hallTvStatus.GetComponent<TextMeshProUGUI>();
            if(TvText != null){
                TvText.text = isTvOn ? "ON" : "OFF";
            }
        }
        else{
            Debug.Log("Issue with Tv Status");
        }
        
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

    public static void updateKitchenDashboard() {
        GameObject m_foodstatus = GameObject.Find("kitchen-microwave-food-status");
        GameObject m_activestatus = GameObject.Find("kitchen-microwave-status");
        
       
       if (m_foodstatus != null && m_activestatus != null) {
        Debug.Log("UPDATE started-1");

         TextMeshProUGUI m_foodstatusText = m_foodstatus.GetComponent<TextMeshProUGUI>();
         TextMeshProUGUI m_activestatusText = m_activestatus.GetComponent<TextMeshProUGUI>();

         if (m_foodstatusText != null && m_activestatusText != null) {

            if (MicrowaveController.food_present) {
                m_foodstatusText.text = "YES";
            }
            else {
                m_foodstatusText.text = "NO";
            }


             if (MicrowaveController.start_flag) {
                m_activestatusText.text = "ON";
            }
            else {
                m_activestatusText.text = "OFF";
            }

         }
       }


       GameObject r_icestatus = GameObject.Find("kitchen-refrigerator-ice-status");
       GameObject r_temperaturestatus = GameObject.Find("kitchen-refrigerator-temperature-status");
       GameObject r_modestatus = GameObject.Find("kitchen-refrigerator-mode-status");


       if (r_icestatus != null && r_temperaturestatus != null && r_modestatus != null) {
        Debug.Log("UPDATE started-2");

            TextMeshProUGUI r_icestatusText =r_icestatus.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI r_temperaturestatusText = r_temperaturestatus.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI r_modestatusText = r_modestatus.GetComponent<TextMeshProUGUI>();

             if (r_icestatusText != null && r_temperaturestatusText != null && r_modestatusText != null) {
                
                Debug.Log("UPDATE started-3");

                 if (RefrigiratorController.ice_present) {
                    r_icestatusText.text = "YES";
                }
                else {
                    r_icestatusText.text = "NO";
                }

            r_temperaturestatusText.text = Mathf.CeilToInt(RefrigiratorController.cur_temperature).ToString();

            if(RefrigiratorController.cur_temperature == 37f) {
                r_modestatusText.text = "NORMAL";
            }
            else if(RefrigiratorController.cur_temperature == 45f) {
                r_modestatusText.text = "HIGH";
            }
            else if(RefrigiratorController.cur_temperature == 10f) {
                r_modestatusText.text = "LOW";
            }

            
             }
        }
       
  
    }


    public static void closeHallDashboard(){
        CharacterHandler.toggleHallDashboard = false;
        CharacterHandler.ReleaseCharacter();
    }
    public static void closeBedroomDashboard(){
        CharacterHandler.toggleBedroomDashboard = false;
        CharacterHandler.ReleaseCharacter();
    }
    public static void closekitchenDashboard() {
        CharacterHandler.toggleKitchenDashboard = false;
        CharacterHandler.ReleaseCharacter();

    }
}
