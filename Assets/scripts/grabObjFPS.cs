using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

public class grabObjectFPS : MonoBehaviour
{


    // On définit les variables utiles

    Ray _ray;
    RaycastHit _hit;
    public float rayDistance = 100f;
    private Transform selection;
    private Vector3 lastPos;
    private Quaternion lastRot;


    public AudioClip hitSound;
    public AudioSource audioSource;
    public float objRight = 2;
    public float objUp = -1;
    public float objClose = 2;
    private bool Animation = false;
 
    public float dureeAnim = 60f;
    private float frameAnim = 1f;

    void Update()
    { 
        Transform camera = Camera.main.transform;
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (!Animation)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(_ray, out _hit, rayDistance)) // si le rayon touche un objet à une distance max de 100 :
                {
                    if (_hit.collider.GetComponent<Transform>().CompareTag("Selectable"))
                    {
                        if (_hit.collider.GetComponent<Transform>() != selection)
                        {

                            Debug.Log("nouveau objet");
                            ResetLastObjectPosition();
                           
                            selection = _hit.collider.GetComponent<Transform>();

                            lastPos = selection.position;
                            lastRot = selection.rotation;
      
                        }
                         
                    }
                    else {
                        if (selection != null)
                        {
                            Animation = true;
                            Interract(selection, _hit.collider.GetComponent<Transform>());
                        }
                    }



                }
              

                else
                {
                    if (selection != null)
                    {
                    Animation = true;
               
                    }
                }
            }


        }
   
    
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
          
            ResetLastObjectPosition();
            selection = null;
            Animation = false;
        }

            if (selection != null)
        {

            selection.position = camera.position + objClose * camera.forward + objRight * camera.right + objUp * camera.up;
            selection.rotation = camera.rotation;
            if (Animation)
            {
                PlayAnimation(camera);


            }
        }

    }

    private void Interract(Transform selection, Transform objetAModifier )
    {

        Debug.Log("interract");
        LayerMask action  = selection.gameObject.layer;

        if (action  == LayerMask.NameToLayer("pinceau rouge"))
        {
            Renderer hitRenderer = objetAModifier.GetComponent<Renderer>();// On enregistre le renderer de l'objet en question

            Debug.Log("rouge");
            if (hitRenderer != null) // si le renderer n'est pas nul
            {
               
                    
                    hitRenderer.material.color = Color.red; // on applique la couleur souhaité à l'objet
                    
            }

        }
        else if (action == LayerMask.NameToLayer("pinceau bleu"))
        {
            Renderer hitRenderer = objetAModifier.GetComponent<Renderer>();// On enregistre le renderer de l'objet en question


            if (hitRenderer != null) // si le renderer n'est pas nul
            {

                 
                hitRenderer.material.color = Color.blue; // on applique la couleur souhaité à l'objet

            }

        }
    }

    void ResetLastObjectPosition() 
    {
        if (selection != null)
        {
            if (lastPos != selection.position && selection.CompareTag("Selectable")) 
            {
                selection.position = lastPos;
                selection.rotation = lastRot;
                Debug.Log(" objet remit a sa place");

            }
        }
    }


    void PlayAnimation(Transform camera)
    {

        selection.RotateAround(selection.position - camera.up, camera.right, Mathf.Abs(frameAnim) * (90 / dureeAnim));
        frameAnim += 1f;
        
        if (frameAnim > dureeAnim)
        {
            frameAnim = -dureeAnim;


        }
        if (frameAnim == 0)
        {
            Animation = false;
        }
    }

}



