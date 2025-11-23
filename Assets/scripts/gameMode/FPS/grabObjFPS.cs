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
    private Transform main;

    void Start()
    {
        useObject = GetComponent<UseObjFPS>();
        main = GameObject.Find("player").transform.Find("positionTete/main");

    }
    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;

        if (!useObject.getAnim())
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

                            selection.position = main.position + camera.up*0.2f;
                            selection.rotation = camera.rotation;
                            if (selection.GetComponent<Rigidbody>() != null) selection.GetComponent<Rigidbody>().isKinematic = true;

                            selection.SetParent(main);
                        }
                    }
                    else if (selection != null)
                    {
                        useObject.StartInteraction(selection,hitTransform,camera);
                    }
                }
                else if (selection != null)
                {
                    Debug.Log("commence anim");
                    StartCoroutine(useObject.PlayAnimation(selection,camera));
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ResetLastObjectPosition();
                selection = null;
                if (selection.GetComponent<Rigidbody>() != null) selection.GetComponent<Rigidbody>().isKinematic = false;
            }

        }


    }

    void ResetLastObjectPosition()
    {
        if (selection != null && selection.CompareTag("Selectable"))
        {
            selection.position = lastPos;
            selection.rotation = lastRot;
            selection.SetParent(null);
        }
    }

}