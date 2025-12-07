using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Android;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Timeline;

public class CamPosInteraction : MonoBehaviour
{

    private Transform cameraStatic;
    private ModeManager modeManager;

    private void Start()
    {

        if (modeManager == null)
            modeManager = FindFirstObjectByType<ModeManager>();

        GameObject parent = transform.parent.transform.gameObject;
        cameraStatic = parent.transform.Find("PotView").transform;
    
        
    }

    private void OnMouseDown()
    {

        if (Input.GetKey(KeyCode.E))
        {
            modeManager.SetCamStatic(cameraStatic);
            modeManager.SwitchMode(true);
        }

    }

}
