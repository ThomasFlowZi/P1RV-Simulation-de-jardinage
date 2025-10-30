using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Android;

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
    public GameObject player;

    private void Start()
    {
        Camera.main.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            modeStatic = !modeStatic;
            if (modeStatic)
            {
                Camera.main.transform.position = staticView.transform.position;
                Camera.main.transform.rotation = staticView.transform.rotation;
            }
            else
            {
                Camera.main.transform.position = player.transform.position + new Vector3(0,1.4f,0);
                Camera.main.transform.rotation = player.transform.rotation;


            }
        }

        if (crosshair != null)
            crosshair.SetActive(!modeStatic);

        Cursor.lockState = modeStatic ?  CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = modeStatic;


        GameMode();
    }

    void GameMode()
    {
      
            foreach (var script in staticScripts)
                script.enabled = modeStatic;

        

        foreach (var script in fpsScripts)
            script.enabled = !modeStatic;

        /*if (modeStatic) Debug.Log("mode static activé !");
        else Debug.Log("mode FPS activé !");*/
    }

   


    public bool IsStatic() => modeStatic;
}