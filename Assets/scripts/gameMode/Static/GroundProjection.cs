using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProjection : MonoBehaviour
{


    public GameObject objet;
    LineRenderer lineRenderer;
    private void Awake()
    {

        lineRenderer = objet.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, objet.transform.position);

        RaycastHit hit;
        Ray ray = new Ray(objet.transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 10f);

        lineRenderer.SetPosition(1, hit.point);
        lineRenderer.startWidth = 0.001f; 
        lineRenderer.startWidth = 0.01f;

    }


    public void SetLR(LineRenderer lr) { lineRenderer = lr; }
}