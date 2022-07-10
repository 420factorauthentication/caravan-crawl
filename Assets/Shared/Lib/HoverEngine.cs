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
// Unity Messages //
////////////////////

    protected virtual void Awake() {
        CursorTargeter.NewObjHover += OnNewObjHover;
    }

/////////////
// Methods //
/////////////

    // The things to do to this HoverParent only (not children) //
    protected abstract void HoverEffectsSelf();

    // The things to do to this HoverParent only (not children) //
    protected abstract void UnhoverEffectsSelf();

    // -- Event delegate that handles both HoverParent and HoverChilds -- //
    public static void OnNewObjHover() {
        HoverParent oldParent =
            CursorTargeter.OldObjHit.transform?.GetComponent<HoverParent>();
        HoverParent newParent =
            CursorTargeter.NewObjHit.transform?.GetComponent<HoverParent>();

        if (oldParent != null) {
            oldParent.UnhoverEffectsSelf();
            foreach (IHoverChild child in
                oldParent.gameObject.GetComponentsInChildren<IHoverChild>())
                    child.OnParentUnhover();

        } if (newParent != null) {
            newParent.HoverEffectsSelf();
            foreach (IHoverChild child in newParent.gameObject.GetComponentsInChildren<IHoverChild>())
                child.OnParentHover();
        }
    }
}


/***************************************************************************/ }
