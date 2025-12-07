using UnityEngine;

public class ModeVisualFeedback : MonoBehaviour
{
    public GameObject body;
    public GameObject crosshair;
    private bool modeStatic;

    public void VisualFeedbackStatic()
    {
        
        modeStatic = true;
        enabled = true;
    }

    public void VisualFeedbackFPS()
    {
        
        modeStatic = false;
        enabled = true;
    }

    public void VisualFeedbackTravel()
    {
        body.SetActive(false);

        Cursor.lockState =  CursorLockMode.Locked;
        Cursor.visible = false;


        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }
    }


    private void OnEnable()
    {
        

        body.SetActive(!modeStatic);

        Cursor.lockState = modeStatic ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = modeStatic;


        if (crosshair != null)
        {
            crosshair.SetActive(!modeStatic);
        }

        enabled = false;
    }
}
