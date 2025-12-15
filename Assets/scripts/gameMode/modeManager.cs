
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ModeManager : MonoBehaviour
{


    private bool modeStatic = true;
    

    public GameObject playerPOV;


    private bool travel;
    private Transform camPositionFPS;
    private Transform camPositionStatic;
    private float threshold = 0.01f;
    private Vector3 camVelocity = Vector3.zero;



    public UnityEvent triggerModeFPS;
    public UnityEvent triggerModeStatic;
    public UnityEvent triggerModeTravel;


    private void Start()
    {
        camPositionFPS = playerPOV.transform;
        camPositionStatic = camPositionFPS;
        Camera.main.transform.SetPositionAndRotation(camPositionFPS.position, camPositionFPS.rotation);
        triggerModeTravel.Invoke(); //juste pour desactiver tous les scripts
        SetMode(false);


    }

    void Update()
    {
        // utile seulement pour faire des tests
        if (Input.GetKeyUp(KeyCode.E)&& !travel)
        {
            SwitchMode(!modeStatic);
        }


    }

    private IEnumerator Travel(bool modeStat)
    {
        travel = true;
        triggerModeTravel.Invoke(); // désactive les scripts pendant le travel

        Transform destination = modeStat ? camPositionStatic : camPositionFPS;

        while (
            Vector3.Distance(Camera.main.transform.position, destination.position) > threshold ||
            Quaternion.Angle(Camera.main.transform.rotation, destination.rotation) > 0.5f
        )
        {
            Camera.main.transform.position = Vector3.SmoothDamp(
                Camera.main.transform.position,
                destination.position,
                ref camVelocity,
                0.3f 
            );

            Camera.main.transform.rotation = Quaternion.Slerp(
                Camera.main.transform.rotation,
                destination.rotation,
                Time.deltaTime * 4f
            );

            yield return null;
        }

        Camera.main.transform.SetPositionAndRotation(
            destination.position,
            destination.rotation
        );

        travel = false;
        GameMode();
    }

    void GameMode() 
    {
        if (!modeStatic)
        {
            Camera.main.transform.parent = null;
            triggerModeFPS.Invoke();
            Camera.main.transform.position = camPositionFPS.position; //en FPS, la camera suit la position de la tete du personnage

        }
        else
        {
            triggerModeStatic.Invoke();
            Camera.main.transform.parent = null;
            Camera.main.transform.position = camPositionStatic.position;
            Camera.main.transform.parent = camPositionStatic;
        }



        if (modeStatic) Debug.Log("mode static activé !");
        else Debug.Log("mode FPS activé !");
    }


    public void SwitchMode(bool modeStat)
    {
        modeStatic = modeStat;
        camVelocity = Vector3.zero;
        StartCoroutine(Travel(modeStat));

    }

    public void SetMode(bool modeStat)
    {

        modeStatic = modeStat;
        GameMode();
   
    }



    public void SetCamStatic(Transform campos) { camPositionStatic = campos;}


    public bool IsStatic() => modeStatic;
}


