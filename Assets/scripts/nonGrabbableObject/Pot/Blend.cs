using System.Collections;
using TMPro;
using TreeEditor;
using UnityEngine;

public class Blend : MonoBehaviour
{
    GameObject diggedDirt;
    GameObject dirtPile;
    DryToWetPot dpWet;
    DryToWetPot ddWet;
    public float speed = 2f; 
    private void Start()
    {
        diggedDirt = transform.Find("DiggedDirt").gameObject;
        dirtPile = transform.Find("DirtPile").gameObject;

        dpWet = dirtPile.GetComponent<DryToWetPot>();
        ddWet = diggedDirt.GetComponent<DryToWetPot>();
    }
    // Update is called once per frame
    void Update()
    {
        if(dirtPile.activeInHierarchy && diggedDirt.activeInHierarchy && !dpWet.GetIsWatered() && !ddWet.GetIsWatered())
        {
            Debug.Log("dmlgkjsdgmlksrjgmlsrdkmdsrjlgk");
            StartCoroutine(BlendColor());
            enabled = false;
        }


    }

    public IEnumerator BlendColor()
    {

        if (ddWet.getWet() == dpWet.getWet()) { enabled = true ; yield break; }

        float targetWet = ddWet.getWet() + dpWet.getWet() / 2f;

        float t = 0f;


        while (t < 1f)
        {
            t += Time.deltaTime * speed;

            ddWet.setWet(Mathf.Lerp(ddWet.getWet(),targetWet,t));
            dpWet.setWet(Mathf.Lerp(dpWet.getWet(), targetWet, t));




            yield return null;
        }

        ddWet.setWet(targetWet);
        dpWet.setWet(targetWet);
        enabled = true;
        yield break;
    }
}
