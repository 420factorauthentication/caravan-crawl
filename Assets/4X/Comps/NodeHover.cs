using UnityEngine;
using HoverEngine;
using NodeEngine;


// =================================================== //
// Changes shaders of Node + children On Pointer Hover //
// =================================================== //
[RequireComponent(typeof(MeshRenderer))]
public class NodeHover : HoverParent {

    MeshRenderer rend;

    void Awake() {
        rend = GetComponent<MeshRenderer>();
    }

    protected override void OnHover() {
        rend.material.shader = NodeShader.Hover;
    }

    protected override void OnUnhover() {
        rend.material.shader = NodeShader.Base;
    }
}
