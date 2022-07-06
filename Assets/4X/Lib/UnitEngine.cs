using UnityEngine;
using HexEngine;
using NodeEngine;


namespace UnitEngine {

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]

    public class Entity : MonoBehaviour {
        public int NodeCol {get; private set;}
        public int NodeRow {get; private set;}
        MeshRenderer rend;

        protected virtual void Awake() {
            rend = GetComponent<MeshRenderer>();
        }

        public void SetModel(string resourceName) {
            Mesh mesh = Resources.Load<Mesh>(resourceName);
            GetComponent<MeshFilter>().mesh = mesh;
            rend.material = Resources.Load<Material>(resourceName);  //TODO//
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

        //Change shader on mouse hover
        public void OnNodeHover() {
            rend.material.shader = NodeShader.Hover;
        }

        public void OnNodeUnhover() {
            rend.material.shader = NodeShader.Base;
        }
    }
}
