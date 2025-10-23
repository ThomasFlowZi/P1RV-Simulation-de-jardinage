using System.Collections;
using UnityEngine;
using static UnityEditor.SceneView;

public class switchMode : MonoBehaviour
{
    public MonoBehaviour grabObj;      // Mode interactif (clics)
    public MonoBehaviour colorObj;
    public MonoBehaviour cameraMove;   // Mode FPS
    public GameObject crosshair;


    public bool isFPSMode = false;

    void Start()
    {
        // On démarre en mode "caméra fixe"
        SetFPSMode(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFPSMode = !isFPSMode;
            SetFPSMode(isFPSMode);
        }
    }

    void SetFPSMode(bool active)
    {
        // Active/désactive les scripts selon le mode
        cameraMove.enabled = active;
        grabObj.enabled = active;
        colorObj.enabled = !active;

   
        if (crosshair != null)
            crosshair.SetActive(active);

       
        Cursor.lockState = active ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !active;
    }
}
