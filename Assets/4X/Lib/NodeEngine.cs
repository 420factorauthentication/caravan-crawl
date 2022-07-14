using UnityEngine;
namespace NodeEngine /*;*/ {

// ============================== //
// Enum: Node terrain definitions //
// ============================== //
public class NodeType {
    public NodeType(string texResourceName) {
        Mat = new Material(NodeShader.Base);
        Texture2D mainTex = Resources.Load<Texture2D>(texResourceName);
        Mat.SetTexture("_MainTex", mainTex);
    }

///////////////////////////
// Properties and Fields //
///////////////////////////

    public Material Mat {get; private set;}

//////////////////
// Enumerations //
//////////////////

    public static NodeType
        Test = new("nodetex/test-512");
}




// ======================================= //
// Enum: Shaders for different Node states //
// ======================================= //
public static class NodeShader {
    public static Shader
        Base = Shader.Find("Standard"),
        Hover = Shader.Find("VertexLit");
}


/***************************************************************************/ }
