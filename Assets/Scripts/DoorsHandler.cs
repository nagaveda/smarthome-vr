using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsHandler : MonoBehaviour
{
    public static GameObject selectedDoor;
    
    public static bool isDoorOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void openDoor(){
        Debug.Log("open");
        isDoorOpen = true;
        selectedDoor.transform.Rotate(0, 90, 0);
    }
    public static void closeDoor(){
        Debug.Log("close");
        selectedDoor.transform.Rotate(0, -90, 0);
        isDoorOpen = false;
    }

    public static void openCloseDoor(){
        
        if(selectedDoor != null){
            if(isDoorOpen){
                closeDoor();
            }
            else{
                openDoor();
            }
        }
    }

    


}
