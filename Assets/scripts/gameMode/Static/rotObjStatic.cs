using UnityEngine;


public class RotObjStatic : MonoBehaviour
{


    private GameObject grabbedObject;
    private  GrabManagerStatic GrabManagerStatic;

    public float rayDistance = 100f;
    private Transform selection;
    private Quaternion initRot = Quaternion.Euler(0, 0, 0);
    public float speedRot = 0.2f;


    private void Start()
    {
        if (GrabManagerStatic == null)
        {
            GrabManagerStatic = FindAnyObjectByType<GrabManagerStatic>();
            if (GrabManagerStatic == null) return;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            

            grabbedObject = GrabManagerStatic.WhatGrab();
            if (grabbedObject == null) return;

            selection = grabbedObject.transform;


            if (initRot == Quaternion.Euler(0, 0, 0))
            {
                initRot = selection.rotation;
            }

            Debug.Log(Quaternion.Angle(selection.rotation, initRot));
            if (Quaternion.Angle(selection.rotation, initRot) < 160)
            {
                selection.rotation = selection.rotation * Quaternion.Euler(0, 0, speedRot);
            }




        }
        else
        {
            if (selection != null)
            {

                if (Quaternion.Angle(selection.rotation, initRot) > 1)
                {
                    selection.rotation = selection.rotation * Quaternion.Euler(0, 0, -2 * speedRot);
                }
                else { initRot = Quaternion.Euler(0, 0, 0); }

            }

        
        }


    }
}

