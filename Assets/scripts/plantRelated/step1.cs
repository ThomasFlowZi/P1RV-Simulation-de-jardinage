using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class step1 : MonoBehaviour
{
    DryToWetPot dryToWetPot;
    public GameObject plantStep1;
    public float growthTime = 10f;



    void Update()
    {
       /* if (dryToWetPot.estWet == true)
        {
            StartCoroutine(Grow());

        }*/
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growthTime);
        Instantiate(plantStep1, transform);
        Destroy(gameObject);
    }
}
