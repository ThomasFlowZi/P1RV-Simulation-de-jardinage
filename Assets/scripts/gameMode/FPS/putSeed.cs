using UnityEngine;

public class PutSeed : MonoBehaviour
{
    GameObject seedPrefab;
    bool possedeGraine = false;

    void OnMouseDown()
    {

        if (possedeGraine)

        {

            Instantiate(seedPrefab, gameObject.transform.position, gameObject.transform.rotation);

        }
        
    }
}  