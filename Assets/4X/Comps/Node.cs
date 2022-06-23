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
        Col = newCol;
        Row = newRow;
        //Calculate world coord offsets
        HexGrid parentGrid = transform.parent.GetComponent<HexGrid>();
        float qOffR = parentGrid.qOffR;
        float rOffQ = parentGrid.rOffQ;
        int q = (qOffR==0)?Col:  Col - Mathf.FloorToInt(1f / qOffR * Row);
        int r = (rOffQ==0)?Row:  Row - Mathf.FloorToInt(1f / rOffQ * Col);
        SetWorldCoords(q, r, parentGrid.scale, parentGrid.rotDeg);
        name = "Node " + Col + "q " + Row + "r";
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
