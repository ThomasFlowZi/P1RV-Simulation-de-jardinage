using Unity.VisualScripting;
using UnityEngine;

public class GrabObjFPS : MonoBehaviour
{
    public float rayDistance = 10f;
    

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

        int mask = ~LayerMask.GetMask("Ignore Raycast");

        if (!useObject.getAnim())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out hit, rayDistance, mask))
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
                            if (selection.GetComponent<Collider>() != null) selection.GetComponent<Collider>().enabled = false;
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
                
                if (selection != null && selection.GetComponent<Rigidbody>() != null) selection.GetComponent<Rigidbody>().isKinematic = false;
                if (selection != null &&  selection.GetComponent<Collider>() != null) selection.GetComponent<Collider>().enabled = true;
                selection = null;
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