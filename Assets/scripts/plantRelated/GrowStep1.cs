using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

public class GrowStep1 : MonoBehaviour
{
    private DryToWetPot dryToWetPot1;
    private DryToWetPot dryToWetPot2;
    public GameObject plantStep2;
    public GameObject AssociatedPot;
    public float growthSpeed1 = 0.01f;
    public float finalScale = 1f;
    public bool twoStepsGrowth=false;

    private void Awake()
    {
        enabled = false; 
    }

    public void OnActivate()
    {

        GameObject terre = transform.root.Find("DirtPile").gameObject;
        dryToWetPot1 = terre.GetComponent<DryToWetPot>();
        dryToWetPot2 = transform.root.Find("DiggedDirt").GetComponent<DryToWetPot>();


        enabled = true;
    }

    


        void Update()
    {
        float waterLevel = (dryToWetPot1.getWet() + dryToWetPot2.getWet()) / 2;

        if (waterLevel >= 0.7f )
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
            dryToWetPot1.setWet((finalScale - transform.localScale.x) / finalScale);
            yield return null;
        }

        AssociatedPot = gameObject.transform.root.gameObject;

        if (twoStepsGrowth)
        {
            GameObject step2 = Instantiate(plantStep2, transform.position, transform.rotation,AssociatedPot.transform.Find("positionSeed"));
            step2.transform.localScale = finalScale * Vector3.one;
            Destroy(gameObject);

        }
        else
        { 
            gameObject.transform.SetParent(null, true);
            gameObject.transform.GetChild(0).tag = "Selectable";
        }

        yield break;

    }   
}
