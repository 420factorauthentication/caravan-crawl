using UnityEngine;
using HexEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class NodeMesh : MonoBehaviour {
    Mesh mesh;
    static int[] triangles = new[] {
        0, 1, 6,    0, 6, 5,    0, 5, 4,
        0, 4, 3,    0, 3, 2,    0, 2, 1,    };

    void Awake() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void SetMesh(HexGeo hexGeo) {
        mesh.Clear();
        mesh.vertices = hexGeo.GetVerts();
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
