using System.Collections.Generic;
using UnityEngine;

public class GrabManagerStatic : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float rayDistance = 100f;

    [Header("Grab Settings")]
    public List<MonoBehaviour> grabScripts = new List<MonoBehaviour>();

    private Ray _ray;
    private RaycastHit _hit;

    private bool grabActive = false;
    public GameObject grabbedObject;
    private Plane grabPlane;
    private Vector3 offsetMouseObject;

    private GrabVisualFeedback visualFeedback;
    public OutlineSelection outlineSelection;


    public bool Snap = false;
    public Vector3 hitPoint;

    private void Start()
    {
        foreach (var script in grabScripts)
            script.enabled = false;

    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Transform camera = Camera.main.transform;
        _ray = Camera.main.ScreenPointToRay(mousePos);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_ray, out _hit, rayDistance))
            {
                GameObject hitObj = _hit.collider.gameObject;
                if (hitObj.CompareTag("Selectable"))
                {
                    grabbedObject = hitObj;
                    visualFeedback = grabbedObject.GetComponent<GrabVisualFeedback>();

                    //if (grabbedObject.GetComponent<Rigidbody>() != null) grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

                    if (visualFeedback != null)
                        visualFeedback.OnGrabStart();

                    grabPlane = new Plane(new Vector3(camera.forward.x,0,camera.forward.z), grabbedObject.transform.position);
                    StartGrab();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (visualFeedback != null)
                visualFeedback.OnGrabEnd();

            StopGrab();
            //if (grabbedObject.GetComponent<Rigidbody>() != null) grabbedObject.GetComponent<Rigidbody>().isKinematic = false; désactiver pour l'instant


            grabbedObject = null;
            
        }

        if (grabActive && grabbedObject != null)
        {
            MoveGrabbedObject(mousePos);
        }
    }

    void MoveGrabbedObject(Vector2 mousePos)
    {

        
        RaycastHit hit;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        Physics.Raycast(mouseRay, out hit, rayDistance);
        if (!(hit.collider.gameObject.layer == 10))
        {
            if (grabPlane.Raycast(mouseRay, out float distance))
            {
                hitPoint = mouseRay.GetPoint(distance);


                grabbedObject.transform.position = hitPoint;
            }
        }
        else
        {
            grabbedObject.transform.position = hit.collider.transform.position;
        }
 
    }

    void StartGrab()
    {


        grabActive = true;

        if (outlineSelection != null)
            outlineSelection.SetActivate(false);

        foreach (var script in grabScripts)
            script.enabled = true;

        //Debug.Log("Grab activé");
    }

    void StopGrab()
    {
        grabActive = false;
        offsetMouseObject = Vector3.zero;

        if (outlineSelection != null)
            outlineSelection.SetActivate(true);

        foreach (var script in grabScripts)
            script.enabled = false;

        //Debug.Log("Grab désactivé");
    }

    public bool IsGrabbing() => grabActive;

    public GameObject WhatGrab()
    {
        return grabbedObject;
    }

    public void SetPlane(Plane p)
        { grabPlane = p; }


    public void setGrabbedObject(GameObject objet)
    {

        grabbedObject = objet; 


    }
}