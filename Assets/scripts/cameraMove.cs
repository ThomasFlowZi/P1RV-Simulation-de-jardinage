using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    public float sensitivityX = 3f;  // sensibilité horizontale
    public float sensitivityY = 3f;  // sensibilité verticale

    private float pitch = 0f; // rotation sur X (haut/bas)
    private float yaw = 0f;   // rotation sur Y (gauche/droite)
    private float roll = 0f;  // rotation sur Z (penchement, optionnel)

    void Start()
    {
        // Cache le curseur et le verrouille au centre de l’écran
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        // Récupère le mouvement de la souris
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Ajuste les angles
        yaw += mouseX;       // gauche/droite  axe Y
        pitch -= mouseY;     // haut/bas  axe X (souvent inversé pour un effet naturel)
        roll = 0f;           // optionnel : tu peux lier à une autre touche si tu veux pencher

        // Empêche la caméra de faire un tour complet sur X
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Applique la rotation à la caméra
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);


    }
}


