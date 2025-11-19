using UnityEngine;

public class Moveplayer : MonoBehaviour
{

    void Update()
    {
        Transform camera = Camera.main.transform;
        Vector3 movement = new Vector3(camera.forward.x, 0, camera.forward.z); 

    }
}
