using UnityEngine;
using HexEngine;
using NodeEngine;
using HoverEngine;
namespace UnitEngine /*;*/ {


// =============== //
// A unit/building //
// =============== //
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public abstract class Entity : MonoBehaviour, IHoverChild {

////////////////
// Properties //
////////////////

    public int NodeCol {get; private set;}
    public int NodeRow {get; private set;}
    MeshRenderer rend;

////////////////////
// Unity Messages //
////////////////////

    protected virtual void Awake() {
        rend = GetComponent<MeshRenderer>();
    }

/////////////
// Methods //
/////////////

    // Apply mesh and material from a 3D model file in Resources folder //
    public void SetModel(string resourceName) {
        Mesh mesh = Resources.Load<Mesh>(resourceName);
        GetComponent<MeshFilter>().mesh = mesh;
        rend.material = Resources.Load<Material>(resourceName);  //TODO//
    }

    // -- Does nothing if Node GameObject doesnt exist -- //
    public void SetNode(int newCol, int newRow) {
        GameObject nodeObj = HexGrid.GetNodeObjAt(newCol, newRow);
        if (nodeObj == null) return;
        AxHexVec2 pos = HexGrid.GetOffsetAxialPos(newCol, newRow);
        transform.position = pos.ToWorld();
        transform.SetParent(nodeObj.transform);
        NodeCol = newCol;
        NodeRow = newRow;
    }

    public GameObject GetNode() {
        return transform.parent.gameObject;
    }

////////////////////
// Event Handlers //
////////////////////

    // Changes shader when parent Node is hovered //
    public void OnParentHover() {
        rend.material.shader = NodeShader.Hover;
    }

    // Changes shader when parent Node is unhovered //
    public void OnParentUnhover() {
        rend.material.shader = NodeShader.Base;
    }
}


/***************************************************************************/ }
