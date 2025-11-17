using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Android;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Timeline;

public class ModeManager : MonoBehaviour
{
    [Header("Scripts à activer pendant Static")]
    public List<MonoBehaviour> staticScripts = new List<MonoBehaviour>();
    [Header("Scripts à activer pendant FPS")]
    public List<MonoBehaviour> fpsScripts = new List<MonoBehaviour>();

    private bool modeStatic = false;
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
        camPositionStatic = staticView.transform;
        Camera.main.transform.SetPositionAndRotation(camPositionFPS.position,camPositionFPS.rotation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            modeStatic = !modeStatic; 
            distTravel = 0f;
            if (modeStatic) // si on passe en mode static on stocke la position de la cam en FPS
            {
                camPositionFPS.SetPositionAndRotation(Camera.main.transform.position, Camera.main.transform.rotation);
            }
            
            travel = true;
            
        }

        if (travel) { Travel(); };

        if (crosshair != null)
            crosshair.SetActive(!modeStatic);

        Cursor.lockState = modeStatic ?  CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = modeStatic;

        GameMode();
    }

    private void Travel()
    {
        if (distTravel < 1f) { 
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
  

        else { travel = false; }

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
        }
            

        /*if (modeStatic) Debug.Log("mode static activé !");
        else Debug.Log("mode FPS activé !");*/
    }

   


    public bool IsStatic() => modeStatic;
}