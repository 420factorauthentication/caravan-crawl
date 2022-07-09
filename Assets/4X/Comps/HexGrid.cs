using UnityEngine;
using NodeEngine;
using HexEngine;


// ================================================================ //
// Singleton that manages creating the game grid of hexagonal tiles //
// ================================================================ //
public class HexGrid : MonoBehaviour {
    public static HexGrid Manager {get; private set;}

////////////////
// Properties //
////////////////

    // -- TODO: update existing nodes on change -- //
    [HideInInspector] public static float
        scale  = 10f,
        rotDeg = 0f,
        rOffQ  = 0f,  //Every rOffQ cols, shift world coords back 1 hex
        qOffR  = 2f;  //Every qOffR rows, shift world coords back 1 hex

////////////////////
// Unity Messages //
////////////////////

    void Awake() {
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
        gameObject.name = "HexGrid";
    }

    void Start() {
        Test(); //TODO//
    }

/////////////
// Methods //
/////////////

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

    public static void RemoveNode(int col, int row) {
        GameObject nodeObj = GetNodeObjAt(col, row);
        if (nodeObj != null) Destroy(nodeObj);
    }

    public static GameObject GetNodeObjAt(int col, int row) {
        return GameObject.Find("HexGrid/Node " + col + "q " + row + "r");
    }

    // Calculate hidden q and r (what they would be with offsets) //
    public static AxHexVec2 GetOffsetAxialPos(int col, int row) {
        int q = (HexGrid.qOffR == 0) ? col :
            col - Mathf.FloorToInt(1f / HexGrid.qOffR * row);
        int r = (HexGrid.rOffQ == 0) ? row :
            row - Mathf.FloorToInt(1f / HexGrid.rOffQ * col);
        HexGeo geo = new(HexGrid.scale, HexGrid.rotDeg);
        return new AxHexVec2(q, r, geo);
    }

    // TODO //
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
