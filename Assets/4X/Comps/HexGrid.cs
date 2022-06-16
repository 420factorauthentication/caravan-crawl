using UnityEngine;
using HexEngine;


public class HexGrid : MonoBehaviour {
    // Note: Set before adding nodes; doesn't update on change //
    [HideInInspector] public float scale = 1f;
    [HideInInspector] public float rotDeg = 0f;
    [HideInInspector] public float rOffQ = 0f;  //Every rOffQ cols, shift rows back 1
    [HideInInspector] public float qOffR = 2f;  //Every qOffR rows, shift cols back 1

    void Start() {
        Test(); //TODO//
    }

    // Add node, using offsets //
    void AddNode(int col, int row) {
        int q = (qOffR==0)?col:  col - Mathf.FloorToInt(1f / qOffR * row);
        int r = (rOffQ==0)?row:  row - Mathf.FloorToInt(1f / rOffQ * col);
        AddNodeAbs(q, r);
    }

    // Add node, ignoring offsets //
    void AddNodeAbs(int col, int row) {
        GameObject node = new();
        NodeMesh nodeMesh = node.AddComponent<NodeMesh>();

        HexGeo geo = new(scale, rotDeg);
        AxHexVec2 coords = new(col, row, geo);

        node.name = "Node (" + col + "q," + row + "r)";
        node.transform.SetParent(transform);
        node.transform.localPosition = coords.ToWorld();

        nodeMesh.SetMesh(geo);
    }

    //TODO//
    void Test() {
        AddNode(-1, -2);
        AddNode(-2, -2);
        AddNode(0, -2);
        AddNode(1, -2);
        AddNode(2, -2);

        AddNode(-1, -1);
        AddNode(-2, -1);
        AddNode(0, -1);
        AddNode(1, -1);
        AddNode(2, -1);

        AddNode(-1, 0);
        AddNode(-2, 0);
        AddNode(0, 0);
        AddNode(1, 0);
        AddNode(2, 0);

        AddNode(-1, 1);
        AddNode(-2, 1);
        AddNode(0, 1);
        AddNode(1, 1);
        AddNode(2, 1);

        AddNode(-1, 2);
        AddNode(-2, 2);
        AddNode(0, 2);
        AddNode(1, 2);
        AddNode(2, 2);

        AddNode(-1, 3);
        AddNode(-2, 3);
        AddNode(0, 3);
        AddNode(1, 3);
        AddNode(2, 3);

        AddNode(-1, 4);
        AddNode(-2, 4);
        AddNode(0, 4);
        AddNode(1, 4);
        AddNode(2, 4);
    }
}
