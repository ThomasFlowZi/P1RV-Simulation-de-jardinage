using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProjection : MonoBehaviour
{


    public GrabManagerStatic grabManager;
    public Material linecolor;
    private LineRenderer lineRenderer;
    private GameObject objet;
    private GameObject gpp;


    private void OnEnable()
    {
        objet = grabManager.WhatGrab();
        
        gpp = objet.transform.Find("GroundProjectionPoint").gameObject;
        if (gpp != null)
        {

            lineRenderer = gpp.AddComponent<LineRenderer>();

            lineRenderer.SetPosition(0, gpp.transform.position);
            lineRenderer.SetPosition(1, gpp.transform.position);
        }
    }

    private void OnDisable()
    {
        if (lineRenderer != null)
        {
            Destroy(lineRenderer);
            lineRenderer = null;
        }
    }


    private void Update()
    {
        if (objet == null || lineRenderer == null) return;


        lineRenderer.positionCount = 2;
        lineRenderer.material = linecolor;
        lineRenderer.startWidth = 0.001f;
        lineRenderer.startWidth = 0.01f;
        
        lineRenderer.SetPosition(0, gpp.transform.position);

        RaycastHit hit;
        Ray ray = new Ray(gpp.transform.position, Vector3.down);

        LayerMask layermask = ~LayerMask.GetMask("SnapZone");

        Physics.Raycast(ray, out hit, 10f, layermask);

        
        lineRenderer.SetPosition(1, hit.point);
        


    }


    public void SetLR(LineRenderer lr) { lineRenderer = lr; }
}