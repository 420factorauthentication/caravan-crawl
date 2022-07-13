using UnityEngine;
namespace HoverEngine /*;*/ {


// =============================================================== //
// Something should happen to this when its HoverParent is hovered //
// =============================================================== //
public interface IHoverChild {
    public void OnParentHover();
    public void OnParentUnhover();
}




// ========================================================== //
// Does something to itself and its children On Pointer Hover //
// ========================================================== //
public abstract class HoverParent : MonoBehaviour {

////////////////////
// Event Handlers //
////////////////////

    // The things to do to this HoverParent only (not children) //
    protected abstract void OnHover();
    protected abstract void OnUnhover();

    // -- Called once by CursorTargeter, when any new object is hovered -- //
    public static void OnNewObjHover() {
        HoverParent oldParent =
            CursorTargeter.OldObjHit.transform?.GetComponent<HoverParent>();
        HoverParent newParent =
            CursorTargeter.NewObjHit.transform?.GetComponent<HoverParent>();

        if (oldParent != null) {
            oldParent.OnUnhover();
            foreach (IHoverChild child in
                oldParent.gameObject.GetComponentsInChildren<IHoverChild>())
                    child.OnParentUnhover();

        } if (newParent != null) {
            newParent.OnHover();
            foreach (IHoverChild child in
                newParent.gameObject.GetComponentsInChildren<IHoverChild>())
                    child.OnParentHover();
        }
    }
}


/***************************************************************************/ }
