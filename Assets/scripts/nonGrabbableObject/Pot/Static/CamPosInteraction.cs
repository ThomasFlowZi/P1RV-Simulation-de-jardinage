using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Android;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Timeline;

public class CamPosInteraction : MonoBehaviour
{

    public ModeManager ModeManager;

    private void OnMouseDown()
    {
        GameObject parent = transform.parent.transform.gameObject;
        Transform cameraStatic = parent.transform.Find("PotView").transform;


        ModeManager.SetCamStatic(cameraStatic);
        ModeManager.SwitchMode(true);


    }

}
