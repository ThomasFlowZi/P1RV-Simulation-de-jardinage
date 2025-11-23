using UnityEngine;

public class Moveplayer : MonoBehaviour
{
    public float playerMoveSpeed;

    void Update() 
    {

        Transform camera = Camera.main.transform;
        Vector3 forward = new Vector3(camera.forward.x, 0, camera.forward.z);
        Vector3 right = new Vector3(camera.right.x, 0, camera.right.z);
        Vector3 result = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) // C'est W car QWERTY
        {

            result += forward.normalized;
            
        }
        if (Input.GetKey(KeyCode.S)) 
        {


            result -= forward.normalized;
            
        }
        if (Input.GetKey(KeyCode.D)) 
        {


            result += right.normalized;
            
        }
        if (Input.GetKey(KeyCode.A))
        {

            result -= right.normalized;
            
        }

        transform.position += result.normalized * playerMoveSpeed;



    }

}
