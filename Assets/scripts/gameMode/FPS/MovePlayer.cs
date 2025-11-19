using UnityEngine;

public class Moveplayer : MonoBehaviour
{
    public float playerMoveSpeed;

    void Update() 
    {

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.W)) // C'est W car QWERTY
        {
            Vector3 movement = new Vector3(camera.forward.x, 0, camera.forward.z);
            transform.position += movement.normalized * playerMoveSpeed;
            camera.position += movement.normalized * playerMoveSpeed;
        }
        if (Input.GetKey(KeyCode.S)) 
        {
          
            Vector3 movement = new Vector3(camera.forward.x, 0, camera.forward.z);
            transform.position -= movement.normalized * playerMoveSpeed;
            camera.position -= movement.normalized * playerMoveSpeed;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            
            Vector3 movement = new Vector3(camera.right.x, 0, camera.right.z);
            transform.position += movement.normalized * playerMoveSpeed;
            camera.position += movement.normalized * playerMoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            
            Vector3 movement = new Vector3(camera.right.x, 0, camera.right.z);
            transform.position -= movement.normalized * playerMoveSpeed;
            camera.position -= movement.normalized * playerMoveSpeed;
        }




    }

}
