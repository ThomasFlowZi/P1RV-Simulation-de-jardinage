using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GrabVisualFeedback : MonoBehaviour
{
    public Color grabColor = Color.red;
    private Color originalColor;

    public AudioClip grabSound;
    private AudioSource audioSource;

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        originalColor = _renderer.material.color;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnGrabStart()
    {
        if (_renderer != null)
            _renderer.material.color = grabColor;

        Outline outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;

        if (audioSource != null && grabSound != null)
            audioSource.PlayOneShot(grabSound);
    }

    public void OnGrabEnd()
    {
        if (_renderer != null)
            _renderer.material.color = originalColor;
    }
}