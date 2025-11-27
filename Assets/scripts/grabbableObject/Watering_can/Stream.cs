using UnityEngine;

public class WaterStream : MonoBehaviour
{
    public Transform topPoint;    // position du bord du seau
    public Transform bottomPoint; // position où tombe l'eau
    public float width = 0.1f;    // largeur du filet d’eau
    public float uvSpeed = 2f;    // vitesse de défilement du shader

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[4];
        uvs = new Vector2[4];
        triangles = new int[6] { 0, 2, 1, 1, 2, 3 };
    }

    void LateUpdate()
    {
        UpdateMesh();
    }

    void UpdateMesh()
    {
        Vector3 right = transform.right * width / 2f;

        // Haut (près du seau)
        vertices[0] = transform.InverseTransformPoint(topPoint.position - right);
        vertices[1] = transform.InverseTransformPoint(topPoint.position + right);

        // Bas (point d'impact)
        vertices[2] = transform.InverseTransformPoint(bottomPoint.position - right);
        vertices[3] = transform.InverseTransformPoint(bottomPoint.position + right);

        mesh.vertices = vertices;

        float t = Time.time * uvSpeed;

        // UV qui défile vers le bas = effet d’écoulement
        uvs[0] = new Vector2(0, t);
        uvs[1] = new Vector2(1, t);
        uvs[2] = new Vector2(0, 1 + t);
        uvs[3] = new Vector2(1, 1 + t);

        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
