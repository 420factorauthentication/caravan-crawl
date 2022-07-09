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
[RequireComponent(typeof(NodeMesh))]

public class Node : MonoBehaviour {

////////////////
// Properties //
////////////////

    public int Col {get; private set;}
    public int Row {get; private set;}
    public NodeType Type {get; private set;}
    MeshRenderer rend;

////////////////////
// Unity Messages //
////////////////////

    void Awake() {
        rend = GetComponent<MeshRenderer>();
        gameObject.AddComponent<NodeMesh>();
        Test(); //TODO//
    }

/////////////
// Methods //
/////////////

    // Add a unit/building to this node hex //
    public GameObject AddEntity<T>() {
        GameObject obj = CreateEntity<T>();
        ArrangeEntities();
        return obj;
    }

    // Add x amount of a unit/building to this hex //
    public GameObject[] AddEntities<T>(int count) {
        GameObject[] entities = new GameObject[count];
        for (int i = 0; i < count; i++)
            entities[i] = CreateEntity<T>();
        ArrangeEntities();
        return entities;
    }

    public void SetType(NodeType nodeType) {
        Type = nodeType;
        rend.material = nodeType.Mat;
    }

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

    // -- In public funcs, create entities, then run iterative init funcs -- //
    GameObject CreateEntity<T>() {
        GameObject obj = new();
        obj.AddComponent(typeof(T));
        obj.transform.SetParent(transform);
        return obj;
    }

    // -- Iterative init func:  Arrange entities in a parallelogram -- //
    void ArrangeEntities() {
        HexGeo geo = new(HexGrid.scale, HexGrid.rotDeg);
        int entities = transform.childCount;
        int ePerRow = 1;
        while ((ePerRow*ePerRow) < entities) ePerRow++;
        int eRows = Mathf.CeilToInt((float) entities / ePerRow);
        int eCols = ePerRow;
        int eRowPaddings = eCols + 1;
        int eColPaddings = eRows + 1;
        for (int i = 0; i < entities; i++) {
            int eCol = (i % ePerRow) + 1;
            int eRow = (i / ePerRow) + 1;
            float localQ = ((float) eCol / eRowPaddings) - 0.5f;
            float localR = ((float) eRow / eColPaddings) - 0.5f;
            AxHexVec2 coords = new(localQ, localR, geo);
            transform.GetChild(i).localPosition = coords.ToWorld();
        }
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

////////////////////
// Event Handlers //
////////////////////

    // Change shader on mouse hover //
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
}
