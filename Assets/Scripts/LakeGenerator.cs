using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PolygonCollider2D))]
public class LakeGenerator : MonoBehaviour
{
    public int pointsCount = 32;
    public float radius = 3f;
    public float noiseStrength = 1f;
    public float noiseScale = 1f;

    void Start()
    {
        GenerateLakeShape();
    }

    void GenerateLakeShape()
    {
        List<Vector2> points = new List<Vector2>();

        for (int i = 0; i < pointsCount; i++)
        {
            float angle = 2 * Mathf.PI * i / pointsCount;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            float noise = Mathf.PerlinNoise(dir.x * noiseScale + transform.position.x, dir.y * noiseScale + transform.position.y);
            float r = radius + noise * noiseStrength;

            points.Add(dir * r);
        }

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[pointsCount + 1];
        int[] triangles = new int[pointsCount * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < pointsCount; i++)
        {
            vertices[i + 1] = points[i];

            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = (i + 1);
            triangles[i * 3 + 2] = (i + 2 > pointsCount ? 1 : i + 2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;

        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.SetPath(0, points.ToArray());

        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        GetComponent<MeshRenderer>().material.color = new Color(0.2f, 0.5f, 1f, 1f);
    }
}
