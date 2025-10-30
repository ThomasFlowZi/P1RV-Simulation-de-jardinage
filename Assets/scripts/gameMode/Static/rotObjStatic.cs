using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotObjStatic : MonoBehaviour
{


    Ray _ray;
    RaycastHit _hit;
    public float rayDistance = 100f;
    private Transform selection;
    private Quaternion initRot = Quaternion.Euler(0, 0, 0);
    public float speedRot = 0.2f;


    void Update()
    {
        Vector2 mousePos = Input.mousePosition; // on crée un vecteur 2D qui prend la position de la souris sur l'écran
        Transform camera = Camera.main.transform;
        _ray = Camera.main.ScreenPointToRay(mousePos); // on envoie un rayon sur la position de la souris



        if (Input.GetKey(KeyCode.R))
        {


            if (Physics.Raycast(_ray, out _hit, rayDistance)) // si le rayon touche un objet à une distance max de 100 :
            {

                if (_hit.collider.gameObject.CompareTag("Selectable"))
                {

                    selection = _hit.collider.gameObject.transform;
                    if (initRot == Quaternion.Euler(0, 0, 0))
                    {
                        initRot = selection.rotation;
                    }

                    Debug.Log(Quaternion.Angle(selection.rotation, initRot));
                    if (Quaternion.Angle(selection.rotation, initRot) < 160)
                    {
                        selection.rotation = selection.rotation * Quaternion.Euler(0, 0, speedRot);
                    }

                }
            }

        ;



        }
        else
        {
            if (selection != null)
            {

                if (Quaternion.Angle(selection.rotation, initRot) > 1)
                {
                    selection.rotation = selection.rotation * Quaternion.Euler(0, 0, -2 * speedRot);
                }
                else { initRot = Quaternion.Euler(0, 0, 0); }

            }

        
        }


    }
}

