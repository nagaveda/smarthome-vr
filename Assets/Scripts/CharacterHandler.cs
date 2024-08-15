using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class CharacterHandler : MonoBehaviour
{
    [SerializeField] Transform rayCastSource;
    public static GameObject currentHit;
    GameObject hitObject;
    public float raycastLength = 10f;
    public LineRenderer lineRenderer;
    bool isHighlighted = false;

    GameObject doorMenu;

    public static GameObject lightMenu;
    public static GameObject fanMenu;
    public static GameObject tvMenu;
    GameObject objectInfo;
    GameObject dashboardInfo;

    GameObject hallDashboard;
    GameObject bedroomDashboard;
    GameObject kitchenDashboard;

     // Added microwave and refrigirator RXA220017   
    public static GameObject microwaveMenu;
    public static bool toggleMicrowaveMenu = false;
    public static GameObject chicken_object;
    static bool togglechickenCookMenu = false;
    public static GameObject chicken_menu;
    public static bool change_food_pos = true;
    public static Vector3 initial_chicken_position;
    public static Vector3 microwave_pos;
    public static GameObject microwavedoor;

    public static GameObject refrigeratorMenu;
    public static bool toggleRefrigeratorMenu = false;
    public static Vector3 refrigerator_pos;

    // END
    bool toggleDoorMenu = false;
    static bool toggleLightMenu = false;
    static bool toggleFanMenu = false;
    static bool toggleTvListener = false;
    bool toggleObjectInfo = false;
    bool toggleDashboardInfo = false;
    bool doorTrigger = false;

    bool tvPlayTrigger = false;


    public static bool toggleHallDashboard = true;
    public static bool toggleBedroomDashboard = true;

    public static bool toggleKitchenDashboard = true;
    public static GameObject GlobalMenu;
    bool PressedLastFrame = false;
    bool speedChanged = false;
    bool lengthChanged = false;
    int optionCount = 1;
    bool counterFlag = false;


    //sxb-start
    public static GameObject BlindMenu;

    static bool toggleblindMenu = false;

    //sxb-end


    // Start is called before the first frame update
    void Start()
    {
        doorMenu = GameObject.Find("door-menu");   
        BlindMenu = GameObject.Find("blind-menu");   
        objectInfo = GameObject.Find("obj-info");   
        dashboardInfo = GameObject.Find("dash-info");   
        lightMenu = GameObject.Find("light-menu");   
        fanMenu = GameObject.Find("fan-menu");   
        tvMenu = GameObject.Find("tv-menu");   
        hallDashboard = GameObject.Find("hall-dashboard");
        bedroomDashboard = GameObject.Find("bed-dashboard");
        kitchenDashboard = GameObject.Find("kitchen-dashboard");
        GlobalMenu = GameObject.Find("settings-menu");
        GlobalMenu.SetActive(false);

        // Added microwave and refrigirator RXA220017   
        microwaveMenu = GameObject.Find("microwave-menu");
        microwavedoor = GameObject.Find("int-microwave_C2_02");
        chicken_object = GameObject.Find("int-Chicken_01");
        chicken_menu = GameObject.Find("chicken-menu");
        initial_chicken_position = chicken_object.transform.position;
        microwave_pos = GameObject.Find("int-microwave-kitchen").transform.position;

        refrigeratorMenu = GameObject.Find("refrigirator-menu");
        refrigerator_pos = GameObject.Find("int-Refrigerator3_C2").transform.position;
        refrigeratorMenu.SetActive(false);

        
        // END

    }

    // Update is called once per frame
    void Update()
    {
        performRaycast();
        handleAllFans();
        handleSettings();
        DashboardController.updateKitchenDashboard();
        if(isHighlighted && currentHit != null){
            if(currentHit.name.Contains("int-door")){
                Vector3 hitPosition = currentHit.transform.position;
                Vector3 cameraPosition = Camera.main.transform.position;
                float distance = Vector3.Distance(hitPosition, cameraPosition);
                Debug.Log("DISTANCE"+distance);
                if(distance <= 2.4f){
                    DoorsHandler.selectedDoor = currentHit;
                    toggleDoorMenu = true;
                }
                
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
            else if(currentHit.name.Contains("int-tv") || currentHit.name.Contains("int-screen")){
                toggleTvListener = true;
            }// Added microwave and refrigirator RXA220017   
            else if(currentHit.name.Contains("int-microwave") && MicrowaveController.food_present) {
                MicrowaveController.selectedMicrowave = currentHit;
                if(!microwaveMenu.activeSelf) toggleObjectInfo = true;
            }
            else if (currentHit.name.Contains("int-Chicken_01")) {
                togglechickenCookMenu = true;
            }
            else if (currentHit.name.Contains("int-Refrige")) {
                if(toggleRefrigeratorMenu == false) toggleObjectInfo = true;
            }

            // END

            //sxb-start
            else if (currentHit.name.Contains("int-blind"))
            {
                blinds.selectedBlind = currentHit;
                toggleObjectInfo = true;
            }
            //sxb-end
        }

        // Door Handling START
        if(toggleDoorMenu){
            doorMenu.SetActive(true);
            if(currentHit!=null){
                
                doorMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                doorMenu.transform.LookAt(gameObject.transform);

                if (Input.GetButton("js2") && !doorTrigger)
                {
                    doorTrigger = true;
                    //Delay for waiting for had animation while opening door
                     float timeElapsed = 5.0f;
                    while(timeElapsed > 0){
                        timeElapsed -= Time.deltaTime;
                    }
                    DoorsHandler.openCloseDoor();
                }
                else if (!Input.GetButton("js2") && doorTrigger)
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
              
                objectInfo.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                objectInfo.transform.LookAt(gameObject.transform);

                if (Input.GetButton("js2"))
                {
                    if(currentHit.name.Contains("light")){
                        toggleLightMenu = true;
                    }
                    else if(currentHit.name.Contains("fan")){
                        toggleFanMenu = true;
                    }// Added microwave and refrigirator RXA220017   
                    else if(currentHit.name.Contains("microwave")) {
                        toggleMicrowaveMenu = true;
                    }
                    else if(currentHit.name.Contains("Refrigerator")) {
                        toggleRefrigeratorMenu = true;
                        toggleObjectInfo = false;
                    }
                    // END
                    //sxb-start
                    else if (currentHit.name.Contains("blind"))
                    {
                        toggleblindMenu = true;
                    }
                    //sxb-end
                }
            }
           
            
            
        }
        else{
            objectInfo.SetActive(false);
        }
        //Object Handling ENDS

        //TV START
        if(toggleTvListener){
            if(currentHit!=null){
                if(currentHit.name.Contains("tv-hall") || currentHit.name.Contains("screen-hall")){
                    if (Input.GetButton("js2") && !tvPlayTrigger)
                    {
                        tvPlayTrigger = true;
                        TvController.HandleTvPower();
                    }
                    else if (!Input.GetButton("js2") && tvPlayTrigger)
                    {
                        tvPlayTrigger = false;
                    }  
                }
                 
            }
        }
       
        //TV END

        // Chicken Menu Handling Starts 
        if(togglechickenCookMenu) {
            chicken_menu.SetActive(true);
            if (currentHit != null){
                chicken_menu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                chicken_menu.transform.LookAt(gameObject.transform);

                if (Input.GetButton("js2") && change_food_pos == true) {
                    microwavedoor.transform.Rotate(0 , 90 , 0);
                    MicrowaveController.microdoorflag = true;
                    currentHit.transform.position = currentHit.transform.position + new Vector3(0.5f, 0.53f, 0.3f);
                    MicrowaveController.food_present = true;
                    change_food_pos = false;
                }
            }
                
        }
        else {
            chicken_menu.SetActive(false);
            
        }


        //LIGHT START
        if(toggleLightMenu){
            lightMenu.SetActive(true);
            if(currentHit!=null){
                
               
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
                
               

                Vector3 menuPosition = currentHit.transform.position + new Vector3(0.3f,-0.6f, 0);
                fanMenu.transform.position = menuPosition;
                fanMenu.transform.LookAt(gameObject.transform);
                
            }
        }
        else{
            fanMenu.SetActive(false);
        }
        //FAN END


        //sxb-start

        //BLIND START
        if (toggleblindMenu)
        {
            BlindMenu.SetActive(true);
            if (currentHit != null)
            {



                Vector3 menuPosition = currentHit.transform.position + new Vector3(-0.2f, 2.0f, 1f);
                BlindMenu.transform.position = menuPosition;
                BlindMenu.transform.LookAt(gameObject.transform);

            }
        }
        else
        {
            BlindMenu.SetActive(false);
        }
        //BLIND END

        //sxb-end



        // Added microwave and refrigirator RXA220017   

        if (toggleMicrowaveMenu) {
            microwaveMenu.SetActive(true);
            

            if(currentHit != null) {
                Vector3 menuPosition = microwave_pos + new Vector3(0.75f, -0f , -0.25f);
                microwaveMenu.transform.position = menuPosition;
                microwaveMenu.transform.LookAt(gameObject.transform);
                // microwaveMenu.transform.LookAt(gameObject.transform);
            }
        }
        else {
            microwaveMenu.SetActive(false);
        }
        // Microwave End


        //Added Refrigerator
        if (toggleRefrigeratorMenu) {
            refrigeratorMenu.SetActive(true);
            toggleRefrigeratorMenu = true;
            

            Debug.Log("Activated the refrigerator menu");

            // if (currentHit != null) {
            //     Vector3 menuPosition = refrigerator_pos + new Vector3(-1f , 0.5f , -0.3f);
            //     refrigeratorMenu.transform.position = menuPosition;
            //     refrigeratorMenu.transform.Rotate(Vector3.up, 90f);

            // }
        }
        else {
            toggleRefrigeratorMenu = false;
        }
        //Refrigerator End
        

        //Dashborad START
        if(toggleDashboardInfo){
            dashboardInfo.SetActive(true);
            if(currentHit!=null){

                dashboardInfo.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f + Vector3.up * 0.2f;
                dashboardInfo.transform.LookAt(gameObject.transform);

                if (Input.GetButton("js2"))
                {
                    if(currentHit.name.Contains("int-dash-hall")){
                        toggleHallDashboard = true;
                    }
                    else if(currentHit.name.Contains("int-dash-bed")){
                        toggleBedroomDashboard = true;
                    }
                    else if(currentHit.name.Contains("int-dash-kitchen")) {
                        toggleKitchenDashboard = true;
                        // RestrictCharacter();
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
            // if(currentHit!=null){
               
            //     Vector3 dashboardPosition = currentHit.transform.position + new Vector3(0,-0.6f, -1f);
            //     hallDashboard.transform.position = dashboardPosition;
            //     hallDashboard.transform.LookAt(gameObject.transform);

            // }
        }
        // else{
        //     hallDashboard.SetActive(false);
        // }
        //Hall DASH END

        //Bedroom DASH START
        if(toggleBedroomDashboard){
            DashboardController.updateBedroomDashboard();
            bedroomDashboard.SetActive(true);
            // if(currentHit!=null){
                
            //     Vector3 dashboardPosition = currentHit.transform.position + new Vector3(0.3f,-0.8f, 0.3f);
            //     bedroomDashboard.transform.position = dashboardPosition;
            //     bedroomDashboard.transform.LookAt(gameObject.transform);
                
                
            // }
        }
        // else{
        //     bedroomDashboard.SetActive(false);
        // }
        //Bedroom DASH END

        // Kitchen DASH START
        if (toggleKitchenDashboard) {
            DashboardController.updateKitchenDashboard();
            kitchenDashboard.SetActive(true);
            // if(currentHit != null) {
            //     Vector3 dashboardPosition = currentHit.transform.position + new Vector3(0.3f,-0.8f, 0.3f);
            //         kitchenDashboard.transform.position = dashboardPosition;
            //         kitchenDashboard.transform.LookAt(gameObject.transform);
            // }

        }
        // else {
        //     kitchenDashboard.SetActive(false);
        // }

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
         if(GlobalMenu.activeSelf && Input.GetButton("js10") && !PressedLastFrame){
           counterFlag = true;
           PressedLastFrame = true;
        }
        else if (!Input.GetButton("js10"))
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
                if(Input.GetButton("js3")){
                    Debug.Log("Resume");
                    Resume();
                }
                
            } else if(selectedOption == 2){
                //speed
                UnSelectOption("Resume");
                SelectOption("Speed");
                Debug.Log("SPEED: "+CharacterMovement.speed);
                if(Input.GetButton("js3") && !speedChanged){
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
                else if(!Input.GetButton("js3")){
                    speedChanged = false;         
                }
            }
            else if(selectedOption == 3){
                //raycast length
                UnSelectOption("Speed");
                SelectOption("Length");
                if(Input.GetButton("js3") && !lengthChanged){
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
                else if(!Input.GetButton("js3")){
                    lengthChanged = false;         
                }
            }
            else if(selectedOption == 0){
                //quit
                UnSelectOption("Length");
                SelectOption("Quit");
                if(Input.GetButton("js3")){
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
        // toggleBedroomDashboard = false;
        toggleDashboardInfo = false;
        toggleDoorMenu = false;
        toggleblindMenu = false;
        toggleFanMenu = false;
        // toggleHallDashboard = false;
        toggleLightMenu = false;
        toggleObjectInfo = false;
        togglechickenCookMenu = false;
        toggleMicrowaveMenu = false;
        toggleRefrigeratorMenu = false;
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
    public static void exitBlindMenu(){
        if(BlindMenu.activeSelf){
            BlindMenu.SetActive(false);
            toggleblindMenu = false;
            
        }
    }

    // Added microwave and refrigirator RXA220017   

    public static void exitMicrowaveMenu() {
        if(microwaveMenu.activeSelf) {
            microwaveMenu.SetActive(false);
            toggleMicrowaveMenu = false;
        }
    }

    // END
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
            if(togglechickenCookMenu) {
                togglechickenCookMenu = false;
            }
            
            if(toggleTvListener){
                toggleTvListener = false;
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
                        float speed = 100f; // default handling
                        if(fanObject.name.Contains("hall")){
                            speed = FanController.fanSpeedHall;
                        }
                        else if(fanObject.name.Contains("bed")){
                            speed = FanController.fanSpeedBedroom;
                        }
                        else if(fanObject.name.Contains("study")){
                            speed = FanController.fanSpeedStudyroom;
                        }
                        
                        fanObject.transform.Rotate(Vector3.up * Time.deltaTime * speed);
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
    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Trigger Enter: " + other.gameObject.name);

    //     if (other.gameObject.name.Contains("door"))
    //     {
    //         if(currentHit != null && currentHit.name.Contains("int-door")){
    //             DoorsHandler.selectedDoor = currentHit;
    //             DoorsHandler.openCloseDoor();
    //             Debug.Log("Collided with door!");
    //         }
            
    //     }
    // }

}
