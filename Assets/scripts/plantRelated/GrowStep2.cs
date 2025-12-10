using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrowStep2 : MonoBehaviour
{
    public DryToWetPot dryToWetPot;
    public float growthSpeed2 = 0.1f;
    public float finalScale = 1f;

    private void Awake()
    {
        enabled = false;
    }

    public void OnActivate()
    {

        GameObject terre = transform.root.transform.Find("terre").gameObject;
        dryToWetPot = terre.GetComponent<DryToWetPot>();
       
        enabled = true;
    }

    void Update()
    {
        if (dryToWetPot.getWet() >= 1f)
        {
            StartCoroutine(Grow());
            enabled = false;
        }
    }

    IEnumerator Grow()
    {
        while (transform.localScale.x < finalScale)
        {
            transform.localScale += growthSpeed2 * Time.deltaTime * Vector3.one * 0.01f;
            yield return null;
            dryToWetPot.setWet((finalScale - transform.localScale.x) / finalScale);
        }

        
        gameObject.transform.SetParent(null, true);
        gameObject.transform.GetChild(0).tag = "Selectable";
        

    }

}
