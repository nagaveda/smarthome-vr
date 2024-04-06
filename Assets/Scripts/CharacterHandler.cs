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
    GameObject lightInfo;

    bool toggleDoorMenu = false;
    static bool toggleLightMenu = false;
    bool toggleLightInfo = false;
    bool doorTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        doorMenu = GameObject.Find("door-menu");   
        lightInfo = GameObject.Find("light-info");   
        lightMenu = GameObject.Find("light-menu");   
    }

    // Update is called once per frame
    void Update()
    {
        performRaycast();

        if(isHighlighted && currentHit != null){
            if(currentHit.name.Contains("int-door")){
                DoorsHandler.selectedDoor = currentHit;
                toggleDoorMenu = true;
            }
            else if(currentHit.name.Contains("int-light")){
                LightsController.selectedLight = currentHit;
                toggleLightInfo = true;
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

        // Light Handling START
        if(toggleLightInfo){
            lightInfo.SetActive(true);
            if(currentHit!=null){
                // doorMenu.transform.position = new Vector3(gameObject.transform.position.x+0.2f, gameObject.transform.position.y+0.4f, gameObject.transform.position.z+0.3f);
                lightInfo.transform.position = gameObject.transform.position + Camera.main.transform.forward * 1f;
                lightInfo.transform.LookAt(gameObject.transform);
            }
            if (Input.GetButton("js10"))
            {
                toggleLightMenu = true;
            }
            
            
        }
        else{
            lightInfo.SetActive(false);
        }

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

        // Light Handling END
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
            if(toggleLightInfo){
                toggleLightInfo = false;
            }
        }
        
    }

}
