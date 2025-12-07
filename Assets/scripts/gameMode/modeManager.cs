
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ModeManager : MonoBehaviour
{


    private bool modeStatic = true;
    

    public GameObject playerPOV;


    private float speed = 3f;
    private bool travel;
    private Transform camPositionFPS;
    private Transform camPositionStatic;
    private float threshold = 0.01f;



    public UnityEvent triggerModeFPS;
    public UnityEvent triggerModeStatic;
    public UnityEvent triggerModeTravel;


    private void Start()
    {
        camPositionFPS = playerPOV.transform;
        camPositionStatic = camPositionFPS;
        Camera.main.transform.SetPositionAndRotation(camPositionFPS.position, camPositionFPS.rotation);
        Camera.main.transform.parent = playerPOV.transform;
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
        float distTravel = speed*Time.deltaTime;
        travel = true;
        triggerModeTravel.Invoke(); // on veut que pendant le travel, tout les scripts soient désactivés
        Transform destination = modeStat ? camPositionStatic : camPositionFPS;

        while ( (Camera.main.transform.position - destination.position ).magnitude > threshold)
        {
            Camera.main.transform.SetPositionAndRotation(Vector3.Lerp(Camera.main.transform.position, destination.position, distTravel), Quaternion.Slerp(Camera.main.transform.rotation, destination.rotation, distTravel));

            yield return null;
        };
        travel = false;


        GameMode();

    }

    void GameMode() 
    {
        if (!modeStatic)
        {
            triggerModeFPS.Invoke();
            Camera.main.transform.position = camPositionFPS.position; //en FPS, la camera suit la position de la tete du personnage
            Camera.main.transform.parent = playerPOV.transform;
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


