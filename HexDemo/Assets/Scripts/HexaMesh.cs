using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexaMesh : MonoBehaviour
{
    Mesh hexaMesh;
    List<Vector3> vertices;
    List<int> triangles;
    MeshCollider meshCollider;
    List<Color> colors;
    public Color colorMs;
    public Color[] colorsArray;
    private void Awake()
    {
        hexaMesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh = hexaMesh;
        hexaMesh.name = "Hexa Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
        colors = new List<Color>();
        int randomIndex = Random.Range(0,colorsArray.Length);
        colorMs = colorsArray[randomIndex];

        GenerateMesh();
    }
    
    public void GenerateMesh()
    {
       
        hexaMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();
        Vector2 center = gameObject.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(center, (center + HexaInfo.corners[i]), (center + HexaInfo.corners[i + 1]));
            AddTrianglesColor(colorMs);

        }
        meshCollider.sharedMesh = hexaMesh;

        hexaMesh.vertices = vertices.ToArray();
        hexaMesh.colors = colors.ToArray();
        hexaMesh.triangles = triangles.ToArray();
        hexaMesh.RecalculateNormals();
    }



    public void AddTriangle(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    void AddTrianglesColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }
}
