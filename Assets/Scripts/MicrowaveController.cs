using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.EventSystems;


public class MicrowaveController : MonoBehaviour
{
    public static GameObject selectedMicrowave;
    public static float cur_time = 3f;
    public static bool start_flag = false;
    public static bool food_present = false;
    public static GameObject food_item;
    public static float rotationSpeed = 2f;
    public static float time_control = 1f;
    public static bool microdoorflag = false;
    // public static Button increase_button;
    public static bool increase_flag = true;
    private float rotationDuration = 10f;
    public static GameObject micro_light;
    public static Light micro_light_control;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonGameObject = GameObject.Find("btn-increase");
        micro_light = GameObject.Find("light_m_i_c_r_o");
        micro_light_control = micro_light.GetComponent<Light>();
        micro_light_control.enabled = false;
        // increase_button = buttonGameObject.GetComponent<Button>();
        // increase_button.onClick.AddListener(increase);        
    }
  
    // Update is called once per frame
    void Update()
    {   
       
        
    }

    public void increase() {
        
    }


    IEnumerator RotateObject()
    {   //microdoorflag = true;
        float elapsedTime = 1f;
        micro_light_control.enabled = true;
        while (elapsedTime <= rotationDuration)
        {
            CharacterHandler.chicken_object.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            // Update the elapsed time
            elapsedTime += Time.deltaTime;
            update_text();
            cur_time = rotationDuration - elapsedTime;
            // Wait for the next frame
            yield return null;
        }

        // Ensure the object reaches its final rotation exactly
        CharacterHandler.chicken_object.transform.rotation = Quaternion.Euler(CharacterHandler.chicken_object.transform.rotation.eulerAngles.x, Mathf.Round(CharacterHandler.chicken_object.transform.rotation.eulerAngles.y), CharacterHandler.chicken_object.transform.rotation.eulerAngles.z);
        micro_light_control.enabled = false;
        elapsedTime = 1f;
        while(elapsedTime > 0 ) {
            elapsedTime -= Time.deltaTime;
            cur_time = 0f;
            update_text();
            yield return null;
        }
        start_flag = false;
        CharacterHandler.chicken_object.transform.position = CharacterHandler.initial_chicken_position;
        CharacterHandler.change_food_pos = true;
    }

    public void btn_15sec(){
        Debug.Log("set timer");

        if(start_flag == false && food_present == true) {
            rotationDuration = 15f;
            cur_time = rotationDuration;
            update_text();
        }  
    }

    public void btn_30seconds(){
        Debug.Log("set timer");

        if(start_flag == false && food_present == true) {
            rotationDuration = 30f;
            cur_time = rotationDuration;
            update_text();
        }  
    }

    public void btn_45seconds(){
        Debug.Log("set timer");

        if(start_flag == false && food_present == true) {
            rotationDuration = 45f;
            cur_time = rotationDuration;
            update_text();
        }  
    }

    public void btn_60seconds(){
        Debug.Log("set timer");

        if(start_flag == false && food_present == true) {
            rotationDuration = 60f;
            cur_time = rotationDuration;
            update_text();
        }  
    }

    public void btn_75seconds(){
        Debug.Log("set timer");

        if(start_flag == false && food_present == true) {
            rotationDuration = 75f;
            cur_time = rotationDuration;
            update_text();
        }  
    }

    public void btn_90seconds(){

        Debug.Log("set timer");

        if(start_flag == false && food_present == true) {
            rotationDuration = 90f;
            cur_time = rotationDuration;
            update_text();
        }  
    }


    


    public void start() {
        Debug.Log("cooking started");
        if (microdoorflag) {
            CharacterHandler.microwavedoor.transform.Rotate(0 , -90 , 0);
            microdoorflag = false;
        }
        if(food_present == true) {
            start_flag = true;

            StartCoroutine(RotateObject());
            
            
           

        }

        
        
    }

    public void update_text() {
        GameObject microwavetimer = GameObject.Find("timer_value");
        if (microwavetimer != null) {
            Debug.Log("counter started to detect the timer_value");
            TextMeshProUGUI TimerText = microwavetimer.GetComponent<TextMeshProUGUI>();
            if (TimerText != null) {
                TimerText.text = Mathf.CeilToInt(cur_time).ToString();
                Debug.Log("counter is working" + cur_time);
            }
        }
    }

    public void take_food_out() {
        food_item = null;
    }

    public static void close_microwave_menu() {
        CharacterHandler.toggleMicrowaveMenu = false;
        CharacterHandler.microwaveMenu.SetActive(false);
    }
}
