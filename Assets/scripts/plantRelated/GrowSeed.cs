using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrowSeed : MonoBehaviour
{
    GameObject terre;
    public DryToWetPot dryToWetPot;
    public GameObject plantStep1;
    public float SeedDev = 3;

    private void Start()
    {

        terre = transform.root.Find("DirtPile").gameObject;
        dryToWetPot = terre.GetComponent<DryToWetPot>();
    }

    void Update()
    {
        if (dryToWetPot.getWet() >= 1f && terre.activeInHierarchy)
        {
            StartCoroutine(Grow());
            enabled = false;
        }
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(SeedDev);
        GameObject step1 = Instantiate(plantStep1, transform.localPosition+Vector3.up*0.3f, transform.localRotation);
        step1.transform.localScale = 0f * Vector3.one;
        step1.transform.SetParent(transform.parent, false);
        step1.GetComponent<GrowStep1>().OnActivate();
        Destroy(gameObject);
    }   
}
