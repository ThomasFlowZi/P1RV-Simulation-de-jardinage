using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Android;

public class ModeManager : MonoBehaviour
{
    [Header("Scripts à activer pendant Static")]
    public List<MonoBehaviour> staticScripts = new List<MonoBehaviour>();
    [Header("Scripts à activer pendant FPS")]
    public List<MonoBehaviour> fpsScripts = new List<MonoBehaviour>();

    private bool modeStatic = true;
    public GameObject crosshair;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            modeStatic = !modeStatic;   
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