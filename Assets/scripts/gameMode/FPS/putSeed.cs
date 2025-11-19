using UnityEngine;

public class PutSeed : MonoBehaviour
{
    GameObject seedPrefab;
    bool possedeGraine = false;

    void OnMouseDown()
    {

        if (possedeGraine)
        
    }


    void Update()
    {
        Transform camera = Camera.main.transform;
        RaycastHit hit;
        
        Ray ray = new Ray(camera.position, camera.forward);
        if (Physics.Raycast(ray, out hit, rayDistance)  )

        {
            Instantiate(seedPrefab, gameObject.transform.position, gameObject.transform.rotation);

        }


    }

}  