using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;

public class Animation : MonoBehaviour
{
    GameObject parent;

  

    float time;
    Vector3 currentPos;
    public void Play()
    {

        currentPos = transform.position;
        parent = transform.parent.gameObject;
        StartCoroutine(Anim(parent));



    }

    private IEnumerator Anim(GameObject parent)
    {

        
            float durationRot = 0.25f;
            float durationDown = 0.3f;
            float durationReturn = 0.25f;

            Vector3 startPos = parent.transform.localPosition;
            Quaternion startRot = parent.transform.localRotation;

            Quaternion targetRot = Quaternion.Euler(180f,startRot.eulerAngles.y,startRot.eulerAngles.z);
            Vector3 downPos = new Vector3(startPos.x,0f,startPos.z);

            float t;

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / durationRot;
                parent.transform.localRotation = Quaternion.Slerp(startRot, targetRot, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / durationDown;
                parent.transform.localPosition = Vector3.Lerp(startPos, downPos, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / durationReturn;
                parent.transform.localPosition = Vector3.Lerp(downPos, startPos, t);
                parent.transform.localRotation = Quaternion.Slerp(targetRot, startRot, t);
                yield return null;
            }

            parent.transform.localPosition = startPos;  
            parent.transform.localRotation = startRot;
            FindAnyObjectByType<InteractFPS>().isGrabbing = true;
        
    }
}
