using UnityEngine;
using HexEngine;


namespace UnitEngine {

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]

    public class Entity : MonoBehaviour {
        public int NodeCol {get; private set;}
        public int NodeRow {get; private set;}

        public void SetModel(string resourceName) {
            Mesh mesh = Resources.Load<Mesh>(resourceName);
            GetComponent<MeshFilter>().mesh = mesh;
        }

        public void SetNode(int newCol, int newRow) {
            AxHexVec2 pos = Node.GetOffsetAxialPos(newCol, newRow);
            transform.localPosition = pos.ToWorld();
            NodeCol = newCol;
            NodeRow = newRow;
        }

        public GameObject GetNodeObj() {
            return HexGrid.GetNodeObjAt(NodeCol, NodeRow);
        }
    }
}
