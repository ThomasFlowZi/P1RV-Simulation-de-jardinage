using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrowSteps : MonoBehaviour
{
    public DryToWetPot dryToWetPot;
    public GameObject plantStep1;
    public GameObject plantStep2;
    public float growthTime1 = 10f;
    public float growthTime2 = 5f;

    private void Start()
    {

        GameObject terre = transform.root.transform.Find("terre").gameObject;
        dryToWetPot = terre.GetComponent<DryToWetPot>();
    }

    void Update()
    {
        Debug.Log(dryToWetPot.getWet());
        if (dryToWetPot.getWet() >= 1f)
        {
            StartCoroutine(Grow());

        }
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growthTime1);
        GameObject step1 = Instantiate(plantStep1, transform.position,transform.rotation);
        step1.transform.localScale = 0.2f*Vector3.one;
        Destroy(gameObject);
        yield return new WaitForSeconds(growthTime2);
        GameObject step2 = Instantiate(plantStep2, transform.position, transform.rotation);
        step2.transform.localScale = 0.2f * Vector3.one;
        Destroy(step1);
    }
}
