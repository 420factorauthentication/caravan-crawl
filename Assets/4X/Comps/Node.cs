using UnityEngine;
using HexEngine;
using NodeEngine;
using UnitEngine;


//Note: GameObject children are all units/buildings on that node
[RequireComponent(typeof(MeshRenderer))]

public class Node : MonoBehaviour {
    public int Col {get; private set;}
    public int Row {get; private set;}
    public NodeType Type {get; private set;}
    MeshRenderer rend;

    void Awake() {
        rend = GetComponent<MeshRenderer>();
        gameObject.AddComponent<NodeMesh>();
        Test(); //TODO//
    }

    public GameObject AddEntity<T>() {
        GameObject obj = new();
        obj.AddComponent(typeof(T));
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        return obj;
    }

    public void SetType(NodeType nodeType) {
        Type = nodeType;
        rend.material = nodeType.Mat;
    }

    public void SetAxialPos(int newCol, int newRow) {
        AxHexVec2 offPos = GetOffsetAxialPos(newCol, newRow);
        SetWorldCoords(offPos.Q, offPos.R);
        name = "Node " + newCol + "q " + newRow + "r";
    }

    public static AxHexVec2 GetOffsetAxialPos(int col, int row) {
        int q = (HexGrid.qOffR == 0) ? col :
            col - Mathf.FloorToInt(1f / HexGrid.qOffR * row);
        int r = (HexGrid.rOffQ == 0) ? row :
            row - Mathf.FloorToInt(1f / HexGrid.rOffQ * col);
        HexGeo geo = new(HexGrid.scale, HexGrid.rotDeg);
        return new AxHexVec2(q, r, geo);
    }

    void SetWorldCoords(float q, float r) {
        // set mesh //
        NodeMesh nodeMesh = GetComponent<NodeMesh>();
        HexGeo geo = new(HexGrid.scale, HexGrid.rotDeg);
        nodeMesh.SetMesh(geo);
        // set world pos //
        AxHexVec2 coords = new(q, r, geo);
        transform.localPosition = coords.ToWorld();
    }

    //Change shader on mouse hover
    public static void OnNewObjMouseHover() {
        Node oldNode = CursorTargeter.OldObjHit.transform?.GetComponent<Node>();
        Node newNode = CursorTargeter.NewObjHit.transform?.GetComponent<Node>();
        if (oldNode != null) {
            oldNode.rend.material.shader = NodeShader.Base;
            foreach (Entity e in oldNode.gameObject.GetComponentsInChildren<Entity>())
                e.OnNodeUnhover();
        } if (newNode != null) {
            newNode.rend.material.shader = NodeShader.Hover;
            foreach (Entity e in newNode.gameObject.GetComponentsInChildren<Entity>())
                e.OnNodeHover();
        }
    }


    //TODO//
    void Test() {
        AddEntity<Building>();
    }
}
