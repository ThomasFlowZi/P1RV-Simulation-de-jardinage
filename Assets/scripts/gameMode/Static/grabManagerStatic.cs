using System.Collections.Generic;
using UnityEngine;

public class GrabManagerStatic : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float rayDistance = 10f;

    [Header("Grab Settings")]
    public List<MonoBehaviour> grabScripts = new List<MonoBehaviour>();

    private Ray _ray;
    private RaycastHit _hit;

    private bool grabActive = false;
    private GameObject grabbedObject;
    private Plane grabPlane;
    private Vector3 offsetMouseObject;

    private GrabVisualFeedback visualFeedback;
    public OutlineSelection outlineSelection;


    public GameObject Snap ;
    public Vector3 hitPoint;

    


    private void Start()
    {
        foreach (var script in grabScripts)
            script.enabled = false;

    }

    private void OnDisable()
    {
        if ( grabbedObject != null )
        {
            if (visualFeedback != null)
                visualFeedback.OnGrabEnd();

            //if (grabbedObject.GetComponent<Rigidbody>() != null) grabbedObject.GetComponent<Rigidbody>().isKinematic = false; temporaire 


            grabbedObject = null;
        }

        StopGrab();
          



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
                   

                    if (grabbedObject.GetComponent<Rigidbody>() != null) grabbedObject.GetComponent<Rigidbody>().isKinematic = true;


                    visualFeedback = grabbedObject.GetComponent<GrabVisualFeedback>();
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

        
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

    
        if (grabbedObject != null && grabPlane.Raycast(mouseRay, out float distance))
        {

            hitPoint = mouseRay.GetPoint(distance);

            int layerMask = LayerMask.GetMask("SnapZone");  // ne detecte que les snap zones

            

            if (Physics.Raycast(mouseRay, out hit, distance,layerMask) )
            {
                Debug.Log(hit);

                if ( IsPointInsideBoxCollider(hit.collider.gameObject.GetComponent<BoxCollider>(), hitPoint) && grabbedObject.layer == 9)  // si notre curseur est sur une snap zone et qu'on tient le seau 
                {
                    grabbedObject.transform.position = hit.collider.transform.position;
                    Snap = hit.collider.gameObject;
                }
                else
                {
                    grabbedObject.transform.position = hitPoint;
                    Snap = null;

                }
            }
            else
            {

                grabbedObject.transform.position = hitPoint;
                Snap = null;


            }

        }
 
    }

    void StartGrab()
    {

        ////////////////// ici on active tous les scripts généraux du mode grab ////////////////////////////////

        grabActive = true;

        if (outlineSelection != null)
            outlineSelection.SetActivate(false);

        foreach (var script in grabScripts)
            script.enabled = true;

        //Debug.Log("Grab activé");

        ///////////////////////// ici on active tous les scripts dédiés a l'objet grab ///////////////////////////////


        foreach (var g in grabbedObject.GetComponents<IGrabbable>())
      
            g.OnGrabStart();



    }

    void StopGrab()
    {

        ////////////////// ici on desactive tous les scripts généraux du mode grab ////////////////////////////////

        grabActive = false;
        offsetMouseObject = Vector3.zero;

        if (outlineSelection != null)
            outlineSelection.SetActivate(true);

        foreach (var script in grabScripts)
            script.enabled = false;

        ////////////////// ici on desactive tous les scripts dédiés a l'objet grab ////////////////////////////////

        if (grabbedObject != null)
        {
            foreach (var g in grabbedObject.GetComponents<IGrabbable>())
                g.OnGrabEnd();
        }

    }

    public static bool IsPointInsideBoxCollider(BoxCollider box, Vector3 point)
    {
        Vector3 localPoint = box.transform.InverseTransformPoint(point);

        Vector3 halfSize = box.size / 2f;

        return (localPoint.x >= box.center.x - halfSize.x && localPoint.y >= box.center.y - halfSize.y && localPoint.z >= box.center.z - halfSize.z) &&
            (localPoint.x <= box.center.x + halfSize.x && localPoint.y <= box.center.y + halfSize.y && localPoint.z <= box.center.z + halfSize.z);


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