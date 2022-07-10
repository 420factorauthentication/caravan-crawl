using System.Collections;
using UnityEngine;
using HexEngine;
using NodeEngine;
using UnitEngine;


// ===================================================================== //
// A hexagonal tile on the game grid.                                    //
// -- GameObj children = List of stationed units/buildings (entities) -- //
// ===================================================================== //
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(GroupHex))]
[RequireComponent(typeof(NodeHover))]
[RequireComponent(typeof(NodeMesh))]

public class Node : MonoBehaviour {

////////////////
// Properties //
////////////////

    public int Col {get; private set;}
    public int Row {get; private set;}
    public NodeType Type {get; private set;}
    GroupHex groupHex;

////////////////////
// Unity Messages //
////////////////////

    void Awake() {
        groupHex = GetComponent<GroupHex>();
        Test(); //TODO//
    }

/////////////
// Methods //
/////////////

    // Remove a unit/building from this node hex //
    public void RemoveEntity(int index) {
        groupHex.RemoveChild(index);
    }

    public void RemoveEntity(Component comp) {
        groupHex.RemoveChild(comp);
    }

    public void RemoveEntity(GameObject obj) {
        groupHex.RemoveChild(obj);
    }

    // Add a unit/building to this node hex //
    public GameObject AddEntity<T>() {
        GameObject obj = groupHex.AddChild();
        obj.AddComponent(typeof(T));
        return obj;
    }

    // Add x amount of a unit/building to this hex //
    public GameObject[] AddEntities<T>(int count) {
        GameObject[] objs = groupHex.AddChildren(count);
        for (int i = 0; i < count; i++)
            objs[i].AddComponent(typeof(T));
        return objs;
    }

    // Set Node terrain type and update game to show it //
    public void SetType(NodeType nodeType) {
        Type = nodeType;
        GetComponent<MeshRenderer>().material = nodeType.Mat;
    }

    // Set position on hexagonal game grid //
    public void SetAxialPos(int newCol, int newRow) {
        AxHexVec2 offPos = HexGrid.GetOffsetAxialPos(newCol, newRow);
        SetAbsWorldCoords(offPos.Q, offPos.R);
        name = "Node " + newCol + "q " + newRow + "r";
    }

    // -- Feed hidden q and r to this func -- //
    void SetAbsWorldCoords(float absQ, float absR) {
        // set mesh //
        NodeMesh nodeMesh = GetComponent<NodeMesh>();
        HexGeo geo = new(HexGrid.scale, HexGrid.rotDeg);
        nodeMesh.SetMesh(geo);
        // set world pos //
        AxHexVec2 coords = new(absQ, absR, geo);
        transform.localPosition = coords.ToWorld();
    }

    // -- TODO -- //
    void Test() {
        StartCoroutine(TestCoroutine());
    }

    // -- TODO -- //
    IEnumerator TestCoroutine() {
        for (int i = 0; i < 16; i++) {
            AddEntity<Building>();
            yield return new WaitForSeconds(1f);
        }
    }
}
