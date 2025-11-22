using UnityEditor.Rendering;
using UnityEngine;
using System.Collections;

public class UseObjFPS : MonoBehaviour
{
    public float dureeAnim = 60f;
    private float frameAnim = 0f;
    private bool Animation  = false;

    Transform  main;

    void Start()
    {
        
        main = GameObject.Find("player").transform.Find("positionTete/main");

    }

    public void StartInteraction(Transform selection, Transform target,Transform camera)
    {
        Animation = true;
        StartCoroutine(PlayAnimation(selection, camera));
        Interact(selection, target);
    }


    public IEnumerator PlayAnimation(Transform selection, Transform camera)
    {
        Animation = true;

        while (true)
        {

            frameAnim += 1f;


            // Si on a fini l'aller on repart dans l'autre sens
            if (frameAnim > dureeAnim)
                frameAnim = -dureeAnim;

            // Quand on revient pile à 0  fin de l’animation
            if (frameAnim == 0)
            {
                Animation = false;
                yield break;
            }


            Debug.Log(frameAnim);

            selection.RotateAround(
                main.position,
                camera.right,
                Mathf.Sign(frameAnim) * (90f / dureeAnim)
            );

            

            yield return null;
        }
    }

    private void Interact(Transform selection, Transform target)
    {
        int layer = selection.gameObject.layer;

        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer == null) return;

        if (layer == LayerMask.NameToLayer("pinceau rouge"))
        {
            renderer.material.color = Color.red;
        }
        else if (layer == LayerMask.NameToLayer("pinceau bleu"))
        {
            renderer.material.color = Color.blue;
        }
    }


    public bool getAnim() { return Animation; }
}