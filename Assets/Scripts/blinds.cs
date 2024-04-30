using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinds : MonoBehaviour
{
    public static float X;
    public static float target_X = 0f;
    public const float rot_speed = 1f;
    public static GameObject selectedBlind;
    public static GameObject selectedBlind_copy;
    //public bool is_blinds_open =false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void BlindsOpen()
    {
        X = 85f;

        Debug.Log("open clicked ");

        if (selectedBlind != null)
        {
            selectedBlind_copy = selectedBlind;
            blinds blinds_script = selectedBlind_copy.GetComponent<blinds>();

            if (blinds_script != null)
            {
                Debug.Log("enabled blind script for " + selectedBlind_copy.name);
                blinds_script.enabled = true;
            }

            if (blinds_script.enabled)
            {
                Debug.Log("Entered math open function and x  value is"+X);

                target_X = 0f;
                X = Mathf.MoveTowards(X, target_X, rot_speed * Time.deltaTime);
                foreach (Transform child in selectedBlind.transform)
                {
                    if (child.name.Contains("Cube"))
                    {
                        child.localRotation = Quaternion.Euler(X, 0f, 0f);
                    }
                }
            }

            // DashboardController.isblindon = true;
        }

        CharacterHandler.exitBlindMenu();

    }

    public static void BlindsClose()
    {
        X = 0f;

        Debug.Log("close clicked ");

        if (selectedBlind != null)
        {
            selectedBlind_copy = selectedBlind;
            blinds blinds_script = selectedBlind_copy.GetComponent<blinds>();

            if (blinds_script != null)
            {
                Debug.Log("enabled blind script for " + selectedBlind_copy.name);
                blinds_script.enabled = true;
            }

            if (blinds_script.enabled)
            {
                Debug.Log("Entered math open function and x  value is" + X);

                target_X = 85f;
                X = Mathf.MoveTowards(X, target_X, rot_speed * Time.deltaTime);
                foreach (Transform child in selectedBlind.transform)
                {
                    if (child.name.Contains("Cube"))
                    {
                        child.localRotation = Quaternion.Euler(X, 0f, 0f);
                    }
                }
            }

            // DashboardController.isblindon = true;
        }

        CharacterHandler.exitBlindMenu();

        // DashboardController.isblindon = false;
    }

    //public static void BlindRotate()
    //{
      //  X = Mathf.MoveTowards(X, target_X, rot_speed * Time.deltaTime);
        //foreach (Transform child in selectedBlind.transform)
        //{
          //  if (child.name.Contains("Cube"))
            //{
              //  child.localRotation = Quaternion.Euler(X, 0f, 0f);
            //}
        //}

    //}
}
