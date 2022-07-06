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

        //Does nothing if Node GameObject doesnt exist
        public void SetNode(int newCol, int newRow) {
            GameObject nodeObj = HexGrid.GetNodeObjAt(newCol, newRow);
            if (nodeObj == null) return;
            AxHexVec2 pos = Node.GetOffsetAxialPos(newCol, newRow);
            transform.position = pos.ToWorld();
            transform.SetParent(nodeObj.transform);
            NodeCol = newCol;
            NodeRow = newRow;
        }

        public GameObject GetNode() {
            return transform.parent.gameObject;
        }
    }
}
