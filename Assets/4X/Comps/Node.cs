using System;
using System.Collections;
using UnityEngine;
using HexEngine;
using NodeEngine;


// ===================================================================== //
// A hexagonal tile on the game grid.                                    //
// -- GameObj children = List of stationed units/buildings (entities) -- //
// ===================================================================== //
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(GroupHex))]
[RequireComponent(typeof(NodeHover))]
[RequireComponent(typeof(NodeMesh))]

public class Node : MonoBehaviour {

///////////////////////////
// Properties and Fields //
///////////////////////////

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

    // Adds a unit/building to this node hex //
    public GameObject AddEntity(Type type) {
        GameObject obj = groupHex.AddChild();
        obj.AddComponent(type);
        return obj;
    }
    public GameObject AddEntity<T>() {
        GameObject obj = groupHex.AddChild();
        obj.AddComponent(typeof(T));
        return obj;
    }


    // -- More efficient way to use AddEntity() multiple times -- //
    public GameObject[] AddEntities(Type type, int count) {
        GameObject[] objs = groupHex.AddChildren(count);
        for (int i = 0; i < count; i++)
            objs[i].AddComponent(type);
        return objs;
    }
    public GameObject[] AddEntities<T>(int count) {
        GameObject[] objs = groupHex.AddChildren(count);
        for (int i = 0; i < count; i++)
            objs[i].AddComponent(typeof(T));
        return objs;
    }


    // Removes a unit/building from this Node. Destroys its GameObject. //
    public void RemoveEntity(int index) {
        groupHex.RemoveChild(index);
    }
    public void RemoveEntity(GameObject obj) {
        groupHex.RemoveChild(obj);
    }
    public void RemoveEntity(Component comp) {
        groupHex.RemoveChild(comp);
    }// -- Depth first search -- //
    public void RemoveEntity(Type componentType) {
        groupHex.RemoveChild(componentType);
    }


    // -- More efficient way to use RemoveEntity() multiple times -- //
    public void RemoveEntities(int index, int count) {
        groupHex.RemoveChildren(index, count);
    }
    public void RemoveEntities(GameObject[] objs) {
        groupHex.RemoveChildren(objs);
    }
    public void RemoveEntities(Component[] comps) {
        groupHex.RemoveChildren(comps);
    }// -- Depth first search -- //
    public void RemoveEntities(Type componentType, int count) {
        groupHex.RemoveChildren(componentType, count);
    }


    // Sets Node terrain type and updates game to render it //
    public void SetType(NodeType nodeType) {
        Type = nodeType;
        GetComponent<MeshRenderer>().material = nodeType.Mat;
    }


    // Sets position on hexagonal game grid //
    public void SetAxialPos(int newCol, int newRow) {
        AxHexVec2 offPos = HexGrid.GetOffsetAxialPos(newCol, newRow);
        SetAbsWorldCoords(offPos.Q, offPos.R);
        name = "Node " + newCol + "q " + newRow + "r";
    }


    // -- This func takes hidden q and r from HexGrid.GetOffsetAxialPos() -- //
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
    IEnumerator TestCoroutine() {
        for (int i = 0; i < 16; i++) {
            AddEntity<Building>();
            yield return new WaitForSeconds(1f);
        }
    }
}
