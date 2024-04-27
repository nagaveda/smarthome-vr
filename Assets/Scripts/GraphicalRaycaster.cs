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
    bool flag = false;

    Button doorBtn;
    // Start is called before the first frame update
    void Start()
    {
        CharacterObj = GameObject.Find("Character");
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);

        if(results.Count > 0){
            
            foreach (RaycastResult result in results)
            {
                GameObject hitObject = result.gameObject;
                
                if(hitObject != null && hitObject.name.Contains("btn-")){
                    doorBtn = hitObject.GetComponent<Button>();
                    // Debug.Log("Hit " + doorBtn);
                    doorBtn.OnPointerEnter(null);
                    if(Input.GetButton("js10")){
                        if(!flag){
                            doorBtn.onClick.Invoke();
                            flag = true;
                        }

                    }
                    else{
                        flag = false;
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
            flag = false;
            
        }
    }
}
