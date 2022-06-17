using UnityEngine;
using HexEngine;


[RequireComponent(typeof(MeshRenderer))]

public class Node : MonoBehaviour {
    public int col {get; private set;}
    public int row {get; private set;}

    void Awake() {
        gameObject.AddComponent<NodeMesh>();
    }

    public void SetAxialPos(int newCol, int newRow) {
        col = newCol;
        row = newRow;
        //Calculate world coord offsets
        HexGrid parentGrid = transform.parent.GetComponent<HexGrid>();
        float qOffR = parentGrid.qOffR;
        float rOffQ = parentGrid.rOffQ;
        int q = (qOffR==0)?col:  col - Mathf.FloorToInt(1f / qOffR * row);
        int r = (rOffQ==0)?row:  row - Mathf.FloorToInt(1f / rOffQ * col);
        SetWorldCoords(q, r, parentGrid.scale, parentGrid.rotDeg);
        name = "Node " + col + "q " + row + "r";
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
}
