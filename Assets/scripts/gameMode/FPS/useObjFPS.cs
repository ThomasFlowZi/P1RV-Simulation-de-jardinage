using UnityEngine;

public class UseObjFPS : MonoBehaviour
{
    public float dureeAnim = 60f;
    private float frameAnim = 1f;
    public bool Animation { get; private set; } = false;

    public void StartInteraction(Transform selection, Transform target)
    {
        Animation = true;
        Interact(selection, target);
    }

    public void StartAnimation()
    {
        Animation = true;
    }

    public void StopAnimation()
    {
        Animation = false;
        frameAnim = 1f;
    }

    public void PlayAnimation(Transform selection, Transform camera)
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
}