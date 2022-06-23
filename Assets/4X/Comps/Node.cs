using UnityEngine;
using HexEngine;
using NodeEngine;


[RequireComponent(typeof(MeshRenderer))]

public class Node : MonoBehaviour {
    public int Col {get; private set;}
    public int Row {get; private set;}
    public NodeType Type {get; private set;}
    MeshRenderer rend;

    void Awake() {
        rend = GetComponent<MeshRenderer>();
        gameObject.AddComponent<NodeMesh>();
    }

    public void SetType(NodeType nodeType) {
        Type = nodeType;
        rend.material = nodeType.Mat;
    }

    public void SetAxialPos(int newCol, int newRow) {
        int q = (HexGrid.Manager.qOffR==0) ? newCol:
            newCol - Mathf.FloorToInt (1f / HexGrid.Manager.qOffR * newRow);
        int r = (HexGrid.Manager.rOffQ==0) ? newRow:
            newRow - Mathf.FloorToInt (1f / HexGrid.Manager.rOffQ * newCol);
        SetWorldCoords(q, r, HexGrid.Manager.scale, HexGrid.Manager.rotDeg);
        name = "Node " + newCol + "q " + newRow + "r";
    }

    void SetWorldCoords(int q, int r, float scale, float rotDeg) {
        // set mesh //
        NodeMesh nodeMesh = GetComponent<NodeMesh>();
        HexGeo geo = new(scale, rotDeg);
        nodeMesh.SetMesh(geo);
        // set world pos //
        AxHexVec2 coords = new(q, r, geo);
        transform.localPosition = coords.ToWorld();
    }

    //Change shader on mouse hover
    public static void OnNewObjMouseHover() {
        Node oldNode = CursorTargeter.OldObjHit.transform?.GetComponent<Node>();
        Node newNode = CursorTargeter.NewObjHit.transform?.GetComponent<Node>();
        if (oldNode != null)
            oldNode.rend.material.shader = NodeShader.Base;
        if (newNode != null)
            newNode.rend.material.shader = NodeShader.Hover;
    }
}
