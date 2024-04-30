using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinds : MonoBehaviour
{
    private float rotationX = 85f;
    private float targetRotationX = 0f;
    private const float rotationSpeed = 100f;
    public static GameObject selectedBlind;
    public static GameObject selectedBlind_copy;
    public static bool is_blinds_open =false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (is_blinds_open)
        {
            if (Mathf.Approximately(rotationX, 0f))
            {
                targetRotationX = 85f;
            }
        }
        else 
        {
            if (Mathf.Approximately(rotationX, 85f))
            {
                targetRotationX = 0f;
            }
        }

        rotationX = Mathf.MoveTowards(rotationX, targetRotationX, rotationSpeed * Time.deltaTime);
        foreach (Transform child in transform)
        {
            // Operate on the local rotation of each child object
            child.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }

    public static void BlindsOpen()
    {
        is_blinds_open = true;
        CharacterHandler.exitBlindMenu();

    }

    public static void BlindsClose()
    {
        is_blinds_open = false;
        CharacterHandler.exitBlindMenu();

        // DashboardController.isblindon = false;
    }

    
}
