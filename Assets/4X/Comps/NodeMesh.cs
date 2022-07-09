using UnityEngine;
using HexEngine;


// =================================================================== //
// When Node position is set, recalculates vertices and recreates mesh //
// =================================================================== //
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]

public class NodeMesh : MonoBehaviour {

////////////////////
// Unity Messages //
////////////////////

    void Awake() {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

/////////////
// Methods //
/////////////

    // Create a new mesh, with vertices calculated from a HexGeo //
    public void SetMesh(HexGeo hexGeo) {
        int[] triangles = new[] {
            0, 1, 6,    0, 6, 5,    0, 5, 4,
            0, 4, 3,    0, 3, 2,    0, 2, 1,
        };
        Vector2[] UVs = new Vector2[] {
            new(0.5f, 0.50f),
            new(1.0f, 0.25f),
            new(0.5f, 0.00f),
            new(0.0f, 0.25f),
            new(0.0f, 0.75f),
            new(0.5f, 1.00f),
            new(1.0f, 0.75f),
        };
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = hexGeo.GetVerts();
        mesh.triangles = triangles;
        mesh.SetUVs(0, UVs);
        mesh.RecalculateNormals();
    }
}
