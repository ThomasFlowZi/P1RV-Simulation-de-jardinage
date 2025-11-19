using UnityEngine;


public class RotObjStatic : MonoBehaviour
{


    private GameObject grabbedObject;
    private GrabManagerStatic GrabManagerStatic;

    public float rayDistance = 100f;
    private Transform selection;
    private Quaternion initRot = Quaternion.Euler(0, 0, 0);


    public float speedRot = 0.002f;


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

            if (grabbedObject.layer == 9) //watering can 
            {

                selection = grabbedObject.transform;
                Debug.Log(selection);

                if (initRot == Quaternion.Euler(0, 0, 0))
                {
                    Debug.Log("coucou"); initRot = selection.localRotation;
                }



                if (Quaternion.Angle(selection.localRotation, initRot) < 90)
                {

                    selection.localRotation = selection.localRotation * Quaternion.Euler(0, 0, -speedRot);

                    if (GrabManagerStatic.Snap != null)
                    {
                            GameObject parent = GrabManagerStatic.Snap.transform.parent.gameObject;
                            GameObject terre = parent.transform.Find("terre").gameObject;

                            terre.GetComponent<Renderer>().material.color = terre.GetComponent<Renderer>().material.color*0.99f;

                    }
                }
            }



        }
        else
        {
            if (selection != null)
            {

                if (Quaternion.Angle(selection.localRotation, initRot) > 10)
                {
                    Debug.Log(Quaternion.Angle(selection.localRotation, initRot));
                    selection.localRotation = selection.localRotation * Quaternion.Euler(0, 0, 2 * speedRot);
                }
                else
                {
                    selection.localRotation = initRot;
                }

            }


        }


    }
}