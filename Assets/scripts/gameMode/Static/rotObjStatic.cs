using UnityEngine;

public class rotObjStatic : MonoBehaviour
{


    Ray _ray;
    RaycastHit _hit;
    public float rayDistance = 100f;
    private Transform selection;


    void Update()
    {
        Vector2 mousePos = Input.mousePosition; // on crée un vecteur 2D qui prend la position de la souris sur l'écran
        Transform camera = Camera.main.transform;
        _ray = Camera.main.ScreenPointToRay(mousePos); // on envoie un rayon sur la position de la souris



        if (Input.GetKeyDown(KeyCode.R)) // lorsque que le clic gauche est touchée :
        {
            

             if (Physics.Raycast(_ray, out _hit, rayDistance)) // si le rayon touche un objet à une distance max de 100 :
                {

                 if (_hit.collider.gameObject.CompareTag("Selectable"))
                    {

                        selection = _hit.collider.gameObject.transform;


                 }
             }
             

            

        };

    }
}