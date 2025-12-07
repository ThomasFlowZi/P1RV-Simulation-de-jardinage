using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;

    private RaycastHit raycastHit;
    public float rayDistance = 100f;
    public AudioSource audioSource;
    public AudioClip hitSound;
    private bool Sound = false;
    private bool activate=true;

    public void SetRayDistance(float distance)
        { this.rayDistance = distance; }    

    public void OnActivate()
    {
        enabled = true;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition; // on crée un vecteur 2D qui prend la position de la souris sur l'écran
        Transform camera = Camera.main.transform;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out raycastHit, rayDistance))
        {
            if (audioSource != null && hitSound != null && Sound) // joue le son de sélection 
            {
                audioSource.PlayOneShot(hitSound);
                Sound = false;
            }

            if (highlight != raycastHit.transform)
            {
                if (highlight != null)
                { highlight.gameObject.GetComponent<Outline>().enabled = false; }

                highlight = raycastHit.transform;
                if (highlight.CompareTag("Selectable"))
                {
                    Sound = true;
                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {


                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                        highlight.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;

                    }
                    else
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                        highlight.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;
                    }
                }
                else
                {


                    highlight = null;
                    Sound = false;

                }
                ;
            }


        }
        else
        {
            Sound = false;
            if (highlight != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = null;
            }
        }

    }

    


    

    public void OnDeactivate()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        enabled = false;
    }

}



