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

        cameraStatic = transform.Find("StaticView").transform;
    
        
    }

    public void OnActivate()
    {

        
            modeManager.SetCamStatic(cameraStatic);
            modeManager.SwitchMode(true);
        

    }

}
