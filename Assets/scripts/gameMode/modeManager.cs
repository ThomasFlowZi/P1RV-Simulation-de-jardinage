using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Timeline;

public class ModeManager : MonoBehaviour
{
    [Header("Scripts à activer pendant Static")]
    public List<MonoBehaviour> staticScripts = new List<MonoBehaviour>();
    [Header("Scripts à activer pendant FPS")]
    public List<MonoBehaviour> fpsScripts = new List<MonoBehaviour>();

    private bool modeStatic = true;
    public GameObject crosshair;

    [Header("différents points de vue")]
    public GameObject staticView;
    public GameObject playerPOV;

    private float distTravel = 0f;
    private float speed = 2f;
    private Transform camPositionFPS;
    private Transform camPositionStatic;
    private bool travel = false;

    public GrabManagerStatic refGrabManager;



    private void Start()
    {
        camPositionFPS = playerPOV.transform;
        camPositionStatic = camPositionFPS;
        Camera.main.transform.SetPositionAndRotation(camPositionFPS.position, camPositionFPS.rotation);
        Camera.main.transform.parent = playerPOV.transform;
        SetMode(false);
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            SwitchMode(!modeStatic);
        }




        if (travel) { Travel(); }
        ;


        



    }

    private void Travel()
    {
        if (distTravel < 1f)
        {
            distTravel += Time.deltaTime * speed;


            if (modeStatic)
            {
                Camera.main.transform.SetPositionAndRotation(Vector3.Lerp(camPositionFPS.position, camPositionStatic.position, distTravel), Quaternion.Slerp(camPositionFPS.rotation, camPositionStatic.rotation, distTravel));

            }
            else
            {
                Camera.main.transform.SetPositionAndRotation(Vector3.Lerp(camPositionStatic.position, camPositionFPS.position, distTravel), Quaternion.Slerp(camPositionStatic.rotation, camPositionFPS.rotation, distTravel));
            }
        }


        else { 
            travel = false;
            GameMode();
        }

    }

    void GameMode() // on veut que pendant le travel, tout les scripts soient désactivés
    {

        if (travel)
        {
            foreach (var script in staticScripts)
                script.enabled = false;



            foreach (var script in fpsScripts)
                script.enabled = false;

        }
        else
        {
            foreach (var script in staticScripts)
                script.enabled = modeStatic;



            foreach (var script in fpsScripts)
                script.enabled = !modeStatic;

            if (!modeStatic)
            {
                Camera.main.transform.position = camPositionFPS.position; //en FPS, la camera suit la position de la tete du personnage
                Camera.main.transform.parent = playerPOV.transform;

            }
            else
            {
                Camera.main.transform.position = camPositionStatic.position; 
                Camera.main.transform.parent = camPositionStatic;
            }

        }


        /*if (modeStatic) Debug.Log("mode static activé !");
        else Debug.Log("mode FPS activé !");*/
    }


    public void SwitchMode(bool modeStat)
    {

        SetMode(modeStat);

        travel = true;
    


    }

    public void SetMode(bool modeStat)
    {

        modeStatic = modeStat;
        distTravel = 0f;
        

        Cursor.lockState = modeStatic ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = modeStatic;

      
        GameMode();


        if (crosshair != null)
            crosshair.SetActive(!modeStatic);
    }




    public void SetCamStatic(Transform campos) { camPositionStatic = campos;}




    public bool IsStatic() => modeStatic;
}


