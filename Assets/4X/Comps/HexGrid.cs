using UnityEngine;


//Singleton that manages creating a map of hexagonal tiles //
public class HexGrid : MonoBehaviour {

    // Set before adding nodes; doesn't update on change //
    // Offsets render at different world coords, //
    //  but row/column axial coords are the same //
    [HideInInspector] public float scale = 1f;
    [HideInInspector] public float rotDeg = 0f;
    [HideInInspector] public float rOffQ = 0f;  //Every rOffQ cols, shift rows back 1
    [HideInInspector] public float qOffR = 2f;  //Every qOffR rows, shift cols back 1

    void Awake() {
        gameObject.name = "HexGrid";
    }

    void Start() {
        Test(); //TODO//
    }

    public Node AddNode(int col, int row) {
        GameObject obj = new();
        obj.transform.SetParent(transform);
        Node node = obj.AddComponent<Node>();
        node.SetAxialPos(col, row);
        return node;
    }

    public void RemoveNodeAt(int col, int row) {
        GameObject nodeObj = GetNodeObjAt(col, row);
        if (nodeObj != null) Destroy(nodeObj);
    }

    public GameObject GetNodeObjAt(int col, int row) {
        return GameObject.Find("HexGrid/Node " + col + "q " + row + "r");
    }

    //TODO//
    void Test() {
        AddNode(-2, -2);
        AddNode(-1, -2);
        AddNode(0, -2);
        AddNode(1, -2);
        AddNode(2, -2);

        AddNode(-2, -1);
        AddNode(-1, -1);
        AddNode(0, -1);
        AddNode(1, -1);
        AddNode(2, -1);

        AddNode(-2, 0);
        AddNode(-1, 0);
        AddNode(0, 0);
        AddNode(1, 0);
        AddNode(2, 0);

        AddNode(-2, 1);
        AddNode(-1, 1);
        AddNode(0, 1);
        AddNode(1, 1);
        AddNode(2, 1);

        AddNode(-2, 2);
        AddNode(-1, 2);
        AddNode(0, 2);
        AddNode(1, 2);
        AddNode(2, 2);

        AddNode(-2, 3);
        AddNode(-1, 3);
        AddNode(0, 3);
        AddNode(1, 3);
        AddNode(2, 3);

        AddNode(-2, 4);
        AddNode(-1, 4);
        AddNode(0, 4);
        AddNode(1, 4);
        AddNode(2, 4);
    }
}
