using UnityEngine;
using UnityEngine.UIElements;

public class grabObjStatic : MonoBehaviour
{


    // On définit les variables utiles

    Ray _ray;
    RaycastHit _hit;
    public float rayDistance = 100f;
    public Color hitColor = Color.red;
    private Color originalColor;
    private Renderer lastHitRenderer;

    public AudioClip hitSound;
    public AudioSource audioSource;

    private bool grabActivate = false;
    private GameObject grabObject;
    private Plane grabPlane;
    private Vector3 ecartMouseObject ;
    private OutlineSelection OutlineSelection;

    private rotObjStatic rotObj;

    void Start()
    {
        rotObj = GetComponent<rotObjStatic>();
    }
    void Update()
    {
        Vector2 mousePos = Input.mousePosition; // on crée un vecteur 2D qui prend la position de la souris sur l'écran
        Transform camera = Camera.main.transform;
        _ray = Camera.main.ScreenPointToRay(mousePos); // on envoie un rayon sur la position de la souris



        if (Input.GetKeyDown(KeyCode.Mouse0)) // lorsque que le clic gauche est touchée :
        {

            if (Physics.Raycast(_ray, out _hit, rayDistance)) // si le rayon touche un objet à une distance max de 100 :
            {

                if (_hit.collider.gameObject.CompareTag("Selectable"))
                {
                    Renderer hitRenderer = _hit.collider.GetComponent<Renderer>();// On enregistre le renderer de l'objet en question

                    grabObject = _hit.collider.gameObject;
                    grabActivate = true;
                    grabPlane = new Plane(camera.forward, grabObject.transform.position);

                    grabObject.GetComponent < Outline>().enabled = false;

                    originalColor = hitRenderer.material.color; // on enregistre la couleur de l'objet touché 
                    hitRenderer.material.color = hitColor; // on applique la couleur souhaité à l'objet
                    lastHitRenderer = hitRenderer; // on update la valeur du hit renderer

                }
            }
            else ResetLastObjectColor(); // si le rayon ne touche pas d'objet toutes les couleurs sont intouchées.
        }
    
    



    if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grabObject = null;
            grabActivate = false;
            ecartMouseObject = new Vector3(0f, 0f, 0f);
            ResetLastObjectColor();



        

        }

    if (grabActivate == true)
        {
            grabFunc(mousePos, grabPlane);
        }


    }

    void ResetLastObjectColor() // pour réinitialiser les couleurs des objets qu'on ne touche plus actuellement
    {
        if (lastHitRenderer != null) // si on touché aupparavant
        {
            lastHitRenderer.material.color = originalColor; // on remet la couleur d'origine de l'objet
            lastHitRenderer = null; // on sort de la boucle
        }
    }




    void grabFunc(Vector2 mousePos,  Plane grabPlane)
    {


        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        if (grabPlane.Raycast(mouseRay, out float distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance);
            if (ecartMouseObject == new Vector3(0f,0f,0f) ) ecartMouseObject = grabObject.transform.position - hitPoint;


            grabObject.transform.position = hitPoint + ecartMouseObject ;
        }

    }
}


