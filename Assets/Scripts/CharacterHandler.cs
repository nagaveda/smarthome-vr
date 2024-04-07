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

    bool toggleDoorMenu = false;
    static bool toggleLightMenu = false;
    static bool toggleFanMenu = false;
    bool toggleObjectInfo = false;
    bool doorTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        doorMenu = GameObject.Find("door-menu");   
        objectInfo = GameObject.Find("obj-info");   
        lightMenu = GameObject.Find("light-menu");   
        fanMenu = GameObject.Find("fan-menu");   
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
        }

        // Door Handling START
        if(toggleDoorMenu){
            doorMenu.SetActive(true);
            if(currentHit!=null){
                // doorMenu.transform.position = new Vector3(gameObject.transform.position.x+0.2f, gameObject.transform.position.y+0.4f, gameObject.transform.position.z+0.3f);
                doorMenu.transform.position = gameObject.transform.position + Camera.main.transform.forward * 1f;
                doorMenu.transform.LookAt(gameObject.transform);
            }
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
            }
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
