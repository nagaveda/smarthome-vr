
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class RefrigiratorController : MonoBehaviour
{
    public static GameObject selectedRefrigirator;
    public static float cur_temperature = 37f;
    public static GameObject fridge_door;
    public static GameObject ice;
    public static bool door_fridge_flag = false;
    public static bool ice_present = false;
    public static GameObject ice_sound;

    
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        fridge_door = GameObject.Find("int-Refrigerator3_C2_07");
        ice = GameObject.Find("All_ice");
        ice.SetActive(false);
        ice_sound = GameObject.Find("refridgerator-audio");

    }

    // Update is called once per frame
    void Update()
    {   
       

        
    }

    public void normal_mode(){
        Debug.Log("normal mode");
        cur_temperature = 37f;
        update_temperature();
    }

    public void low_mode(){
        Debug.Log("low mode");
        cur_temperature = 10f;
        update_temperature();
    }

    public void high_mode(){
        Debug.Log("normal mode");
        cur_temperature = 45f;
        update_temperature();
    }


    // public void freeze_mode(){
    //     Debug.Log("freeze mode");
    //     cur_temperature = 0f;
    //     update_temperature();
    // }

    public void open_fridge_door(){
        Debug.Log("door opened");
        if(!door_fridge_flag){
            fridge_door.transform.Rotate(0 , -90 , 0);
            door_fridge_flag = true;
        }
        
        
    }

    public void close_fridge_door(){
        Debug.Log("door closed");
        if(door_fridge_flag){
            fridge_door.transform.Rotate(0 , 90 , 0);
            door_fridge_flag = false;
        }
    }


    public void make_ice(){
        
        Debug.Log("ice made");
        if(!door_fridge_flag){
            ice.SetActive(true);
            ice_present = true;
            if(ice_sound != null){
            AudioSource audio = ice_sound.GetComponent<AudioSource>();
            if(audio != null){
                audio.enabled = true;
            }
        }
        }
        
    }

     public void remove_ice(){
        if(ice_sound != null){
            AudioSource audio = ice_sound.GetComponent<AudioSource>();
            if(audio != null){
                audio.enabled = false;
            }
        }
        Debug.Log("ice removed");
        ice.SetActive(false);
        ice_present = false;
        
    }


    

    public static void update_temperature() {
        GameObject refrigeratortemperature = GameObject.Find("temperature-value");
        TextMeshProUGUI TemperatureText = refrigeratortemperature.GetComponent<TextMeshProUGUI>();
        TemperatureText.text = Mathf.CeilToInt(cur_temperature).ToString();
    }


    public static void close_refrigerator_menu() {
        CharacterHandler.toggleRefrigeratorMenu = false;
        CharacterHandler.refrigeratorMenu.SetActive(false);
    }

    
}
