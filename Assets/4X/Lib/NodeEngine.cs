using UnityEngine;


namespace NodeEngine {

    public class NodeType {
        public Material Mat {get; private set;}
        public NodeType(string texResourceName) {
            Mat = new Material(Shader.Find("Standard"));
            Texture2D mainTex = Resources.Load<Texture2D>(texResourceName);
            Mat.SetTexture("_MainTex", mainTex);
        }

        public static NodeType
            Test = new("nodetex/test-512");
    }
}
