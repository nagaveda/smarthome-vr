using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class CharacterHandler : MonoBehaviour
{
    [SerializeField] Transform rayCastSource;
    GameObject currentHit;
    GameObject hitObject;
    public float raycastLength = 10f;
    public LineRenderer lineRenderer;
    bool isHighlighted = false;

    GameObject doorMenu;

    public static GameObject lightMenu;
    public static GameObject fanMenu;
    GameObject objectInfo;
    GameObject dashboardInfo;

    GameObject hallDashboard;
    GameObject bedroomDashboard;

    bool toggleDoorMenu = false;
    static bool toggleLightMenu = false;
    static bool toggleFanMenu = false;
    bool toggleObjectInfo = false;
    bool toggleDashboardInfo = false;
    bool doorTrigger = false;


    public static bool toggleHallDashboard = false;
    public static bool toggleBedroomDashboard = false;

    public static GameObject GlobalMenu;
    bool PressedLastFrame = false;
    bool speedChanged = false;
    bool lengthChanged = false;
    int optionCount = 1;
    bool counterFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        doorMenu = GameObject.Find("door-menu");   
        objectInfo = GameObject.Find("obj-info");   
        dashboardInfo = GameObject.Find("dash-info");   
        lightMenu = GameObject.Find("light-menu");   
        fanMenu = GameObject.Find("fan-menu");   
        hallDashboard = GameObject.Find("hall-dashboard");
        bedroomDashboard = GameObject.Find("bed-dashboard");
        GlobalMenu = GameObject.Find("settings-menu");
        GlobalMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        performRaycast();
        handleAllFans();
        handleSettings();

        if(isHighlighted && currentHit != null){
            if(currentHit.name.Contains("int-door")){
                DoorsHandler.selectedDoor = currentHit;
                toggleDoorMenu = true;
            }
            else if(currentHit.name.Contains("int-light")){
                LightsController.selectedLight = currentHit;
                toggleObjectInfo = true;
            }
            else if(currentHit.name.Contains("int-fan")){
                FanController.selectedFan = currentHit;
                toggleObjectInfo = true;
            }
            else if(currentHit.name.Contains("int-dash")){
                toggleDashboardInfo = true;
            }
        }

        // Door Handling START
        if(toggleDoorMenu){
            doorMenu.SetActive(true);
            if(currentHit!=null){
                // doorMenu.transform.position = new Vector3(gameObject.transform.position.x+0.2f, gameObject.transform.position.y+0.4f, gameObject.transform.position.z+0.3f);
                // doorMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                // doorMenu.transform.LookAt(gameObject.transform);
                doorMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                doorMenu.transform.LookAt(gameObject.transform);

                if (Input.GetButton("js10") && !doorTrigger)
                {
                    doorTrigger = true;
                    DoorsHandler.openCloseDoor();
                }
                else if (!Input.GetButton("js10") && doorTrigger)
                {
                    doorTrigger = false;
                }
            }
            
        }
        else{
            doorMenu.SetActive(false);
        }
        // Door Handling END

        
        //Object Handling starts
        if(toggleObjectInfo){
            objectInfo.SetActive(true);
            if(currentHit!=null){
                // doorMenu.transform.position = new Vector3(gameObject.transform.position.x+0.2f, gameObject.transform.position.y+0.4f, gameObject.transform.position.z+0.3f);
                // objectInfo.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                // objectInfo.transform.LookAt(gameObject.transform);
                objectInfo.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                objectInfo.transform.LookAt(gameObject.transform);

                if (Input.GetButton("js10"))
                {
                    if(currentHit.name.Contains("light")){
                        toggleLightMenu = true;
                    }
                    else if(currentHit.name.Contains("fan")){
                        toggleFanMenu = true;
                    }
                }
            }
           
            
            
        }
        else{
            objectInfo.SetActive(false);
        }
        //Object Handling ENDS

        //LIGHT START
        if(toggleLightMenu){
            lightMenu.SetActive(true);
            if(currentHit!=null){
                
                // lightMenu.transform.position = new Vector3(currentHit.transform.position.x+0.3f, currentHit.transform.position.y +0.6f, currentHit.transform.position.z);
                // lightMenu.transform.position = gameObject.transform.position + gameObject.transform.forward * 1.5f + Vector3.up * 0.4f;
                // lightMenu.transform.LookAt(gameObject.transform);
                if(currentHit.name.Contains("hall") || currentHit.name.Contains("bed")){
                    Vector3 menuPosition = currentHit.transform.position + new Vector3(0.3f, 0.6f, 0);
                    lightMenu.transform.position = menuPosition;
                    lightMenu.transform.LookAt(gameObject.transform);
                }
                else{
                    Vector3 menuPosition = currentHit.transform.position + new Vector3(0.3f,-0.6f, 0);
                    lightMenu.transform.position = menuPosition;
                    lightMenu.transform.LookAt(gameObject.transform);
                }   
            }
        }
        else{
            lightMenu.SetActive(false);
        }
        //LIGHT END
        //FAN START
        if(toggleFanMenu){
            fanMenu.SetActive(true);
            if(currentHit!=null){
                
                // lightMenu.transform.position = new Vector3(currentHit.transform.position.x+0.3f, currentHit.transform.position.y +0.6f, currentHit.transform.position.z);
                // fanMenu.transform.position = gameObject.transform.position + gameObject.transform.forward * 1.5f + Vector3.up * 0.4f;
                // fanMenu.transform.LookAt(gameObject.transform);

                Vector3 menuPosition = currentHit.transform.position + new Vector3(0.3f,-0.6f, 0);

                // Set the position of the lightMenu
                fanMenu.transform.position = menuPosition;
                fanMenu.transform.LookAt(gameObject.transform);
                
            }
        }
        else{
            fanMenu.SetActive(false);
        }
        //FAN END

        //Dashborad START
        if(toggleDashboardInfo){
            dashboardInfo.SetActive(true);
            if(currentHit!=null){
                
                // lightMenu.transform.position = new Vector3(currentHit.transform.position.x+0.3f, currentHit.transform.position.y +0.6f, currentHit.transform.position.z);
                // dashboardInfo.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                // dashboardInfo.transform.LookAt(gameObject.transform);

                dashboardInfo.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                dashboardInfo.transform.LookAt(gameObject.transform);

                if (Input.GetButton("js10"))
                {
                    if(currentHit.name.Contains("int-dash-hall")){
                        toggleHallDashboard = true;
                        RestrictCharacter();
                    }
                    else if(currentHit.name.Contains("int-dash-bed")){
                        toggleBedroomDashboard = true;
                        RestrictCharacter();
                    }
                    
                }
            }
        }
        else{
            dashboardInfo.SetActive(false);
        }
        //Dashborad END

        //Hall DASH START
        if(toggleHallDashboard){
            DashboardController.updateHallDashboard();
            hallDashboard.SetActive(true);
            if(currentHit!=null){
                
                // hallDashboard.transform.position = new Vector3(gameObject.transform.position.x+0.3f, gameObject.transform.position.y +0.6f, gameObject.transform.position.z);
                // hallDashboard.transform.position = gameObject.transform.position + gameObject.transform.forward * 1.5f + Vector3.up * 0.4f;
                // hallDashboard.transform.LookAt(gameObject.transform);

                Vector3 dashboardPosition = currentHit.transform.position + new Vector3(0,-0.6f, -1f);
                hallDashboard.transform.position = dashboardPosition;
                hallDashboard.transform.LookAt(gameObject.transform);

               
                
            }
        }
        else{
            hallDashboard.SetActive(false);
        }
        //Hall DASH END

        //Bedroom DASH START
        if(toggleBedroomDashboard){
            DashboardController.updateBedroomDashboard();
            bedroomDashboard.SetActive(true);
            if(currentHit!=null){
                // bedroomDashboard.transform.position = new Vector3(currentHit.transform.position.x+0.3f, currentHit.transform.position.y +0.6f, currentHit.transform.position.z);
                // bedroomDashboard.transform.position = gameObject.transform.position + gameObject.transform.forward * 1.5f + Vector3.up * 0.4f;
                // bedroomDashboard.transform.LookAt(gameObject.transform);

                Vector3 dashboardPosition = currentHit.transform.position + new Vector3(0.3f,-0.8f, 0.3f);
                bedroomDashboard.transform.position = dashboardPosition;
                bedroomDashboard.transform.LookAt(gameObject.transform);
                
                
            }
        }
        else{
            bedroomDashboard.SetActive(false);
        }
        //Bedroom DASH END

    }

    public void handleSettings(){
        if(Input.GetButton("js7")){  
            clearAllAndInitializeGlobalMenu();      
            updateSpeed();
        }
         if(counterFlag){
                optionCount++;
                counterFlag = false;
        }
         if(GlobalMenu.activeSelf && Input.GetButton("js11") && !PressedLastFrame){
           counterFlag = true;
           PressedLastFrame = true;
        }
        else if (!Input.GetButton("js11"))
        {
            PressedLastFrame = false;
        }
        if(GlobalMenu.activeSelf){
            int selectedOption = optionCount % 4;
            updateSpeed();
            updateRaycastLength();
            
            if(selectedOption == 1){
                //resume
                UnSelectOption("Quit");
                SelectOption("Resume");
                if(Input.GetButton("js5")){
                    Debug.Log("Resume");
                    Resume();
                }
                
            } else if(selectedOption == 2){
                //speed
                UnSelectOption("Resume");
                SelectOption("Speed");
                Debug.Log("SPEEEED: "+CharacterMovement.speed);
                if(Input.GetButton("js5") && !speedChanged){
                    if(CharacterMovement.speed == 5f){
                        CharacterMovement.speed = 10f;
                    }
                    else if(CharacterMovement.speed == 10f){
                        CharacterMovement.speed = 20f;
                    }
                    else if(CharacterMovement.speed == 20f){
                        CharacterMovement.speed = 5f;
                    }
                    speedChanged = true;
                    
                }
                else if(!Input.GetButton("js5")){
                    speedChanged = false;         
                }
            }
            else if(selectedOption == 3){
                //raycast length
                UnSelectOption("Speed");
                SelectOption("Length");
                if(Input.GetButton("js5") && !lengthChanged){
                    if(raycastLength == 1f){
                        raycastLength = 10f;
                    }
                    else if(raycastLength == 10f){
                        raycastLength = 50f;
                    }
                    else if(raycastLength == 50f){
                        raycastLength = 1f;
                    }
                    lengthChanged = true;
                }
                else if(!Input.GetButton("js5")){
                    lengthChanged = false;         
                }
            }
            else if(selectedOption == 0){
                //quit
                UnSelectOption("Length");
                SelectOption("Quit");
                if(Input.GetButton("js5")){
                    Debug.Log("Quit");
                    quit();
                }
            }
        }
    }
     public void updateSpeed(){

        if(GameObject.Find("Speed") != null){
            GameObject speedobj = GameObject.Find("Speed");
            Button speedButton = speedobj.GetComponent<Button>();
                
            TextMeshProUGUI speedText = speedButton.GetComponentInChildren<TextMeshProUGUI>();
            if(speedText!=null){
                if(CharacterMovement.speed == 5f){
                    speedText.text = "Speed: Slow";
                }
                else if(CharacterMovement.speed == 10f){
                    speedText.text = "Speed: Medium";
                } 
                else if(CharacterMovement.speed == 20f){
                    speedText.text = "Speed: High";
                }
            }
        }
        
    }

    public void clearAllAndInitializeGlobalMenu(){
        toggleBedroomDashboard = false;
        toggleDashboardInfo = false;
        toggleDoorMenu = false;
        toggleFanMenu = false;
        toggleHallDashboard = false;
        toggleLightMenu = false;
        toggleObjectInfo = false;
        isHighlighted = false;
        currentHit = null;
        RestrictCharacter();
        GlobalMenu.SetActive(true);
        optionCount = 1;
        
        
    }

     public void updateRaycastLength(){
        if(GameObject.Find("Length") != null){
            GameObject lengthObj = GameObject.Find("Length");
            Button lengthButton = lengthObj.GetComponent<Button>();
                
            TextMeshProUGUI lengthText = lengthButton.GetComponentInChildren<TextMeshProUGUI>();
            if(lengthText!=null){
                if(raycastLength == 1f){
                    lengthText.text = "Raycast Length:1m";
                }
                else if(raycastLength == 10f){
                    lengthText.text = "Raycast Length:10m";
                } 
                else if(raycastLength == 50f){
                    lengthText.text = "Raycast Length:50m";
                }
            }
        }
    }

    public void SelectOption(string btnName){
        GameObject optionObj = GameObject.Find(btnName);
        if(optionObj != null){
            Button optionButton = optionObj.GetComponent<Button>();
            if(optionButton !=null){
                optionButton.OnPointerEnter(null);
            }
        }
    }
    public void UnSelectOption(string btnName){
        GameObject optionObj = GameObject.Find(btnName);
        if(optionObj != null){
            Button optionButton = optionObj.GetComponent<Button>();
            if(optionButton !=null){
                optionButton.OnPointerExit(null);
            }
        }
    }

    public void performRaycast(){
        
        RaycastHit hit;

        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, raycastLength))
        {
            lineRenderer.SetPosition(0, rayCastSource.position);
            lineRenderer.SetPosition(1, hit.point);
            hitObject = hit.collider.gameObject;
            
            if(hitObject.name.Contains("int-")){
                enableOutline(hitObject);
                currentHit = hitObject;
            }
            else{
                if(currentHit != hitObject){
                    if(currentHit != null){
                        disableOutline(currentHit);
                    }
                }
                
            }

        }
        else{
            Vector3 rayCastEnd = rayCastSource.position + Camera.main.transform.forward * raycastLength;
            lineRenderer.SetPosition(0, rayCastSource.position);
            lineRenderer.SetPosition(1, rayCastEnd);
            
            if(currentHit != null){
                if(currentHit != null){
                    disableOutline(currentHit);
                }
            }
 
        }
    }

    public void enableOutline(GameObject hitObject)
    {
        if(hitObject != null){
            Outline outLineComponent = hitObject.GetComponent<Outline>();
            if(outLineComponent != null){
                outLineComponent.enabled = true;
            } 
            isHighlighted = true;
        }
        
    }

    public static void exitLightMenu(){
        if(lightMenu.activeSelf){
            lightMenu.SetActive(false);
            toggleLightMenu = false;
            
        }
    }
    public static void exitFanMenu(){
        if(fanMenu.activeSelf){
            fanMenu.SetActive(false);
            toggleFanMenu = false;
            
        }
    }

    public void disableOutline(GameObject hitObject)
    {   
        if(hitObject != null){
            Outline outLineComponent = hitObject.GetComponent<Outline>();
            if(outLineComponent != null){
                outLineComponent.enabled = false;
            }
            isHighlighted = false;
            if(toggleDoorMenu){
                toggleDoorMenu = false;
            }
            if(toggleObjectInfo){
                toggleObjectInfo = false;
            }
            
            if(toggleDashboardInfo){
                toggleDashboardInfo = false;
            }
        }
        
    }


     public static void RestrictCharacter(){
        GameObject character = GameObject.Find("Character");
        CharacterController cc = character.GetComponent<CharacterController>();
        cc.enabled = false;
    }

    public static void ReleaseCharacter(){
        GameObject character = GameObject.Find("Character");
        CharacterController cc = character.GetComponent<CharacterController>();
        cc.enabled = true;
    }

    public void Resume(){
        GlobalMenu.SetActive(false);
        ReleaseCharacter();
    }
    
    public void quit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Unity Editor
        #elif UNITY_ANDROID
            Application.Quit(); // Quit application on Android
        #elif UNITY_IOS
            Application.Quit(); // Quit application on iOS
        #endif
    }

    //Entire Fans Handling
    public void handleAllFans(){
        List<string> fanNames = new List<string>{"int-fan-hall", "int-fan-bed", "int-fan-study"};
        foreach(string name in fanNames)
        {
            GameObject fanObject = GameObject.Find(name);

            if (fanObject != null)
            {
                FanStatus fanStatus = fanObject.GetComponent<FanStatus>();

                if (fanStatus != null)
                {
                    if (fanStatus.enabled)
                    {
                        fanObject.transform.Rotate(Vector3.up * Time.deltaTime * 100f);
                    }
                }
                else
                {
                    Debug.LogError("FanStatus script not found on the GameObject: " + name);
                }
            }
            else
            {
                Debug.LogError("GameObject not found with the name: " + name);
            }
        }
    }

}
