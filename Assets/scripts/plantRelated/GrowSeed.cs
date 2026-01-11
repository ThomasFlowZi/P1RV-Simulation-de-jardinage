using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrowSeed : MonoBehaviour
{
    GameObject terre;
    private DryToWetPot dryToWetPot1;
    private DryToWetPot dryToWetPot2;
    public GameObject plantStep1;
    public float SeedDev = 3;

    private void Start()
    {

        terre = transform.root.Find("DirtPile").gameObject;
        dryToWetPot1 = terre.GetComponent<DryToWetPot>();
        dryToWetPot2 = transform.root.Find("DiggedDirt").GetComponent<DryToWetPot>();

    }

    void Update()
    {
        float waterLevel = (dryToWetPot1.getWet() + dryToWetPot2.getWet()) / 2;

        if (waterLevel >= 0.7f && terre.activeInHierarchy)
        {
            Debug.Log("debut pousse");
            StartCoroutine(Grow());
            
        }
    }

    IEnumerator Grow()
    {
        enabled = false;
        yield return new WaitForSeconds(SeedDev);
        GameObject step1 = Instantiate(plantStep1, transform.localPosition+Vector3.up*0.3f, transform.localRotation);
        step1.transform.localScale = 0f * Vector3.one;
        step1.transform.SetParent(transform.parent, false);
        step1.GetComponent<GrowStep1>().OnActivate();
        Destroy(gameObject);
    }   
}
