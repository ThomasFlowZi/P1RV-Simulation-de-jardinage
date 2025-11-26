using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class step1 : MonoBehaviour
{
    DryToWetPot dryToWetPot;
    public GameObject plantStep1;
    public float growthTime = 10f;

    private void Start()
    {
        StartCoroutine(Grow());
    }

    void Update()
    {
       /* if (dryToWetPot.estWet == true)
        {
            

        }*/
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growthTime);
        Instantiate(plantStep1, transform.position,transform.rotation);

        Destroy(gameObject);
    }
}
