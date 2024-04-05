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

    bool toggleDoorMenu = false;


    // Start is called before the first frame update
    void Start()
    {
        doorMenu = GameObject.Find("door-menu");   
    }

    // Update is called once per frame
    void Update()
    {
        performRaycast();

        if(isHighlighted && (currentHit != null && currentHit.name.Contains("int-door"))){
            toggleDoorMenu = true;
        }

        if(toggleDoorMenu){
            doorMenu.SetActive(true);
            if(currentHit!=null){
                
                doorMenu.transform.position = new Vector3(currentHit.transform.position.x+0.5f, currentHit.transform.position.y + 1f, currentHit.transform.position.z+1f);
                doorMenu.transform.LookAt(gameObject.transform);
                
            }
        }
        else{
            doorMenu.SetActive(false);
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
        }
        
    }

}
