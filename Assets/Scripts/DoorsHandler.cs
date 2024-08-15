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
        if(selectedDoor != null){
            AudioSource[] sounds = selectedDoor.GetComponents<AudioSource>();
            if(sounds.Length >= 1){
                AudioSource openSound = sounds[0];
                openSound.enabled = true;
                AudioSource closeSound = sounds[1];
                closeSound.enabled = false;
            }
            
        }
    }
    public static void closeDoor(){
        Debug.Log("close");
        selectedDoor.transform.Rotate(0, -90, 0);
        isDoorOpen = false;
        if(selectedDoor != null){
            AudioSource[] sounds = selectedDoor.GetComponents<AudioSource>();
            if(sounds.Length >= 1){
                AudioSource openSound = sounds[0];
                openSound.enabled = false;
                AudioSource closeSound = sounds[1];
                closeSound.enabled = true;
            }
            
        }
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
