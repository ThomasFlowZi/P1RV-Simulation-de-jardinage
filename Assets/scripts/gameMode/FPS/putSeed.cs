using UnityEngine;

public class putSeed : MonoBehaviour
{

    float rayDistance = 100f; 
    
    void Start()
    {
        
    }


    void Update()
    {
        Transform camera = Camera.main.transform;
        RaycastHit hit;
        
        Ray ray = new Ray(camera.position, camera.forward);
        if (Physics.Raycast(ray, out hit, rayDistance) && )
        {
            if (Input.GetMouseButton(0)) { 
            
            
            
            }




        }
    }
}
