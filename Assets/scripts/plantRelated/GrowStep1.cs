using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GrowStep1 : MonoBehaviour
{
    private DryToWetPot dryToWetPot;
    public GameObject plantStep2;
    public float growthSpeed1 = 0.01f;
    public float finalScale = 1f;
    public bool twoStepsGrowth;

    private void Awake()
    {
        enabled = false; 
    }

    public void OnActivate()
    {

        GameObject terre = transform.root.transform.Find("DiggedDirt").gameObject;
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
            transform.localScale += growthSpeed1 * Time.deltaTime * Vector3.one *0.01f;
            yield return null;
            dryToWetPot.setWet((finalScale -transform.localScale.x )/ finalScale);
        }

        if (twoStepsGrowth)
        {
            GameObject step2 = Instantiate(plantStep2, transform.position, transform.rotation);
            step2.transform.localScale = finalScale * Vector3.one;
            Destroy(gameObject);

        }
        else
        {
            gameObject.transform.SetParent(null, true);
            gameObject.transform.GetChild(0).tag = "Selectable";
        }
        
    }   
}
