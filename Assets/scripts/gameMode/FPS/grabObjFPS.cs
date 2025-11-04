using Unity.VisualScripting;
using UnityEngine;

public class GrabObjFPS : MonoBehaviour
{
    public float rayDistance = 100f;
    public float objRight = 2;
    public float objUp = -1;
    public float objClose = 2;

    private Transform selection;
    private Vector3 lastPos;
    private Quaternion lastRot;

    private UseObjFPS useObject;
    private GameObject player;

    void Start()
    {
        useObject = GetComponent<UseObjFPS>();
        player = GameObject.Find("player");

    }
        void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;

        if (!useObject.Animation)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    Transform hitTransform = hit.collider.GetComponent<Transform>();
                    Debug.Log(hit.collider.gameObject);

                    if (hitTransform.CompareTag("Selectable"))
                    {
                        if (hitTransform != selection)
                        {
                            ResetLastObjectPosition();
                            selection = hitTransform;
                            lastPos = selection.position;
                            lastRot = selection.rotation;
                        }
                    }
                    else if (selection != null)
                    {
                        useObject.StartInteraction(selection, hitTransform);
                    }
                }
                else if (selection != null)
                {
                    useObject.StartAnimation();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ResetLastObjectPosition();
            selection = null;
            useObject.StopAnimation();
        }

        if (selection != null)
        {
            selection.position = camera.position + (objClose * camera.forward + objRight * camera.right + objUp * camera.up) * player.transform.localScale.y;
            selection.rotation = camera.rotation;

            if (useObject.Animation)
            {
                useObject.PlayAnimation(selection, camera);
            }
        }
    }

    void ResetLastObjectPosition()
    {
        if (selection != null && selection.CompareTag("Selectable"))
        {
            selection.position = lastPos;
            selection.rotation = lastRot;
        }
    }
}