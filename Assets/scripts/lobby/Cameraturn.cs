using UnityEngine;

public class Cameraturn : MonoBehaviour
{

    public GameObject centerOfRotation;

    // Update is called once per frame
    void Update()
    {
        centerOfRotation.transform.Rotate(Vector3.up,0.01f);
    }
}
