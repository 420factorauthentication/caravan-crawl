using UnityEngine;
using NodeEngine;


//Singleton that manages creating a map of hexagonal tiles //
public class HexGrid : MonoBehaviour {
    public static HexGrid Manager {get; private set;}

    // Set before adding nodes; doesn't update on change //
    // Offsets render at different world coords, //
    //  but row/column axial coords are the same //
    [HideInInspector] public static float
        scale  = 10f,
        rotDeg = 0f,
        rOffQ  = 0f,  //Every rOffQ cols, shift rows back 1
        qOffR  = 2f;  //Every qOffR rows, shift cols back 1

    void Awake() {
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
        gameObject.name = "HexGrid";
    }

    void Start() {
        Test(); //TODO//
    }

    public static Node AddNode(int col, int row, NodeType type) {
        GameObject obj = new();
        obj.transform.SetParent(Manager.transform);
        Node node = obj.AddComponent<Node>();
        node.SetAxialPos(col, row);
        node.SetType(type);
        //Bandaid fix for weird glitch:
        //A node at (0,0) isnt initially detected by raycasts
        //TODO: Breaks coroutines; look into better fix if coroutine is needed
        if ((col == 0) && (row == 0)) {
            obj.SetActive(false);
            obj.SetActive(true);
        }
        return node;
    }

    public static void RemoveNodeAt(int col, int row) {
        GameObject nodeObj = GetNodeObjAt(col, row);
        if (nodeObj != null) Destroy(nodeObj);
    }

    public static GameObject GetNodeObjAt(int col, int row) {
        return GameObject.Find("HexGrid/Node " + col + "q " + row + "r");
    }

    //TODO//
    void Test() {
        AddNode(-2, -2, NodeType.Test);
        AddNode(-1, -2, NodeType.Test);
        AddNode( 0, -2, NodeType.Test);
        AddNode( 1, -2, NodeType.Test);
        AddNode( 2, -2, NodeType.Test);

        AddNode(-2, -1, NodeType.Test);
        AddNode(-1, -1, NodeType.Test);
        AddNode( 0, -1, NodeType.Test);
        AddNode( 1, -1, NodeType.Test);
        AddNode( 2, -1, NodeType.Test);

        AddNode(-2, 0, NodeType.Test);
        AddNode(-1, 0, NodeType.Test);
        AddNode( 0, 0, NodeType.Test);
        AddNode( 1, 0, NodeType.Test);
        AddNode( 2, 0, NodeType.Test);

        AddNode(-2, 1, NodeType.Test);
        AddNode(-1, 1, NodeType.Test);
        AddNode( 0, 1, NodeType.Test);
        AddNode( 1, 1, NodeType.Test);
        AddNode( 2, 1, NodeType.Test);

        AddNode(-2, 2, NodeType.Test);
        AddNode(-1, 2, NodeType.Test);
        AddNode( 0, 2, NodeType.Test);
        AddNode( 1, 2, NodeType.Test);
        AddNode( 2, 2, NodeType.Test);
    }
}
