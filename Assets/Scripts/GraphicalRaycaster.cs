using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GraphicalRaycaster : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    GameObject CharacterObj;

    Button doorBtn;
    // Start is called before the first frame update
    void Start()
    {
        CharacterObj = GameObject.Find("Character");
         //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //  Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);
        // m_PointerEventData.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        if(results.Count > 0){
            
            foreach (RaycastResult result in results)
            {
                GameObject hitObject = result.gameObject;
                
                if(hitObject != null && hitObject.name.Contains("btn-")){
                    doorBtn = hitObject.GetComponent<Button>();
                    Debug.Log("Hit " + doorBtn);
                    doorBtn.OnPointerEnter(null);
                    if(Input.GetButton("js10")){
                        doorBtn.onClick.Invoke();
                    }
                    if(CharacterObj != null){
                        LineRenderer lr = CharacterObj.GetComponent<LineRenderer>();
                        if(lr != null){
                            lr.SetPosition(1, hitObject.transform.position);
                        }
                    }

                }
                else{
                    if(doorBtn!=null){
                        doorBtn.OnPointerExit(null);
                        doorBtn = null;
                    }
                }

                  
                
                
            }
        }
        else{
            if(doorBtn != null){
                doorBtn.OnPointerExit(null);
                doorBtn = null;
            }
        }
    }
}
