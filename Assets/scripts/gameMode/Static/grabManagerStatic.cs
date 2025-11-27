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


    public Vector3 hitPoint;

    [Header("Grab Settings")]
    public List<int> SnapableObject = new List<int>();




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
        if (grabbedObject == null) return;

        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        if (!grabPlane.Raycast(mouseRay, out float distance)) return;

        hitPoint = mouseRay.GetPoint(distance);

        //  Si l'objet n'est PAS snapable mouvement normal
        if (!SnapableObject.Contains(grabbedObject.layer))
        {
            
            grabbedObject.transform.position = hitPoint;
            return;
        }

        // L'objet est snapable  on cherche une snap zone
        int snapMask = LayerMask.GetMask("SnapZone");
        if (!Physics.Raycast(mouseRay, out RaycastHit hit, distance, snapMask))
        {
            grabbedObject.transform.position = hitPoint;

            if (grabbedObject.GetComponent<State>().GetSnap() != false) grabbedObject.GetComponent<State>().SetSnap(false);
            return;
        }

        // On vérifie si le point est bien DANS le box collider
        BoxCollider box = hit.collider.GetComponent<BoxCollider>();
        bool inside = IsPointInsideBoxCollider(box, hitPoint);

        if (inside)
        {

           
            grabbedObject.transform.position = hit.collider.transform.position;
            if(grabbedObject.GetComponent<State>().GetSnap() != true) grabbedObject.GetComponent<State>().SetSnap(true) ;
        }
        else
        {

            
            grabbedObject.transform.position = hitPoint;
            if (grabbedObject.GetComponent<State>().GetSnap() != false) grabbedObject.GetComponent<State>().SetSnap(false);
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