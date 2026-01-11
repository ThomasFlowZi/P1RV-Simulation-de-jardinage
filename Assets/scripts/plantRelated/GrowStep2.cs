using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowStep2 : MonoBehaviour
{
    private DryToWetPot dryToWetPot1;
    private DryToWetPot dryToWetPot2;
    public float growthSpeed2 = 0.1f;
    public float finalScale = 1f;
    public GameObject AssociatedPot;
    public List<GameObject> vegs;
    public bool done;

    private void Start()
    {
        GameObject terre = transform.root.Find("DirtPile").gameObject;
        dryToWetPot1 = terre.GetComponent<DryToWetPot>();
        dryToWetPot2 = transform.root.Find("DiggedDirt").GetComponent<DryToWetPot>();
    }

 

    void Update()
    {
        if (!done)
        {
            float waterLevel = (dryToWetPot1.getWet() + dryToWetPot2.getWet()) / 2;

            if (waterLevel >= 0.7f)
            {
                StartCoroutine(Grow());
                done = true;
            }

        }
        else
        {
            if (transform.childCount <= 1) ResetState() ;
        }
        
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(3);
        while (vegs[0].transform.localScale.x < finalScale)
        {
            foreach (GameObject v in vegs) {
                v.transform.localScale += growthSpeed2 * Time.deltaTime * Vector3.one * 0.01f;
            }

            transform.localScale += growthSpeed2/2 * Time.deltaTime * Vector3.one * 0.01f;
            
            dryToWetPot1.setWet((finalScale - vegs[0].transform.localScale.x) / finalScale);
            yield return null;
        }

        AssociatedPot = gameObject.transform.root.gameObject;

        foreach (GameObject v in vegs)
        {
            v.transform.GetChild(0).tag = "Selectable";
        }

        yield break;

    }


    private void ResetState()
    {
        
        AssociatedPot.GetComponent<ResetPot>().ResetPotState();
        Destroy(gameObject);

    }

}
