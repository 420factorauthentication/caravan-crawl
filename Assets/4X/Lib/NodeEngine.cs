using UnityEngine;


namespace NodeEngine {

    public class NodeType {
        public Material Mat {get; private set;}
        public NodeType(string texResourceName) {
            Mat = new Material(NodeShader.Base);
            Texture2D mainTex = Resources.Load<Texture2D>(texResourceName);
            Mat.SetTexture("_MainTex", mainTex);
        }

        public static NodeType
            Test = new("nodetex/test-512");
    }

    public static class NodeShader {
        public static Shader
            Base = Shader.Find("Standard"),
            Hover = Shader.Find("VertexLit");
    }
}
