using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


    bool toggleHallDashboard = false;
    bool toggleBedroomDashboard = false;


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
    }

    // Update is called once per frame
    void Update()
    {
        performRaycast();
        handleAllFans();

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
                doorMenu.transform.position = gameObject.transform.position + Camera.main.transform.forward * 1f;
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
                objectInfo.transform.position = gameObject.transform.position + Camera.main.transform.forward * 1f;
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
                lightMenu.transform.position = gameObject.transform.position + gameObject.transform.forward * 1f;
                lightMenu.transform.LookAt(gameObject.transform);
                
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
                fanMenu.transform.position = gameObject.transform.position + gameObject.transform.forward * 1f;
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
                dashboardInfo.transform.position = gameObject.transform.position + Camera.main.transform.forward * 1f;
                dashboardInfo.transform.LookAt(gameObject.transform);
                if (Input.GetButton("js10"))
                {
                    if(currentHit.name.Contains("int-dash-hall")){
                        toggleHallDashboard = true;
                    }
                    else if(currentHit.name.Contains("int-dash-bed")){
                        toggleBedroomDashboard = true;
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
                
                // lightMenu.transform.position = new Vector3(currentHit.transform.position.x+0.3f, currentHit.transform.position.y +0.6f, currentHit.transform.position.z);
                hallDashboard.transform.position = gameObject.transform.position + gameObject.transform.forward * 1f;
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
                
                // lightMenu.transform.position = new Vector3(currentHit.transform.position.x+0.3f, currentHit.transform.position.y +0.6f, currentHit.transform.position.z);
                bedroomDashboard.transform.position = gameObject.transform.position + gameObject.transform.forward * 1f;
                bedroomDashboard.transform.LookAt(gameObject.transform);
                
            }
        }
        else{
            bedroomDashboard.SetActive(false);
        }
        //Bedroom DASH END

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
                    
                    // currentHit = null;
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
                
                // currentHit = null;
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

    //Entire Fans Handling
    public void handleAllFans(){
        List<string> fanNames = new List<string>{"int-fan-hall", "int-fan-bed", "int-fan-study"};
        foreach(string name in fanNames)
        {
            // Find the GameObject with the corresponding name
            GameObject fanObject = GameObject.Find(name);

            // Check if the GameObject is found
            if (fanObject != null)
            {
                // Get the FanStatus component attached to the fanObject
                FanStatus fanStatus = fanObject.GetComponent<FanStatus>();

                // Check if the FanStatus component is found
                if (fanStatus != null)
                {
                    // Check if the FanStatus component is enabled
                    if (fanStatus.enabled)
                    {
                        // Rotate the GameObject if the component is enabled
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
