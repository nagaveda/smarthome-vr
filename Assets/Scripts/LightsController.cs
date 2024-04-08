using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public static GameObject selectedLight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void onLight(){
        Debug.Log("on light");
        Light light = selectedLight.GetComponent<Light>();
        if(light != null){
            light.enabled = true;
        }
        CharacterHandler.exitLightMenu();
    }
    public static void offLight(){
         Debug.Log("off light");
        Light light = selectedLight.GetComponent<Light>();
        if(light != null){
            light.enabled=false;
        }
        CharacterHandler.exitLightMenu();
    }
}
