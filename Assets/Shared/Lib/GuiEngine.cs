using UnityEngine;
using UnityEngine.EventSystems;
namespace GuiEngine /*;*/ {


// ======================================== //
// Magnetizes a Canvas Object to the cursor //
// ======================================== //
[RequireComponent(typeof(RectTransform))]

public class CursorCanvasMagnet : MonoBehaviour {
    protected RectTransform tr;
    public bool isMagnetized {get; private set;} = false;

    protected virtual void Awake() {
        //INIT component handles         //
        tr = GetComponent<RectTransform>();
        //ORIENT position to bottom left //
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.zero;
        //ORIENT movement to center      //
        tr.pivot = new Vector2(0.5f, 0.5f);
    }

    protected virtual void Update() {
        if (!isMagnetized) return;
        tr.localPosition = Input.mousePosition;
    }

    public virtual void Magnetize() {
        isMagnetized = true;
    }

    public virtual void Demagnetize() {
        isMagnetized = false;
    }

    // Orients parent transform to bottom left //
    // Do this somewhere on parent's RectTransform once //
    public static void InitParentAnchor(RectTransform parentTr) {
        parentTr.anchorMin = Vector2.zero;
        parentTr.anchorMax = Vector2.zero;
        parentTr.position = Vector3.zero;
        parentTr.pivot = Vector2.zero;
    }
}




// =============================================================== //
// A Canvas Object that can drag-and-drop, or click-to-toggle-drag //
// =============================================================== //
public class CanvasDraggable : CursorCanvasMagnet,
                               IPointerClickHandler,
                               IDragHandler,
                               IBeginDragHandler,
                               IEndDragHandler {

////////////////
// Properties //
////////////////

    // -- Used to check for 2nd click, to toggle off magnetization       - //
    //  - Clicking is checked in both Update() and OnPointerClick()      - //
    //  - Update() comes after OnPointerClick()                          - //
    //  - This bool is used to prevent 1st click from registering twice -- //
    bool acceptNextDemagClick = false;

    // -- Used to check if a click is a single click-to-toggle, or a drag -- //
    bool dontMagnetizeOnClick = false;

////////////////////
// Unity Messages //
////////////////////

    protected override void Update() {
        base.Update();
        if (!isMagnetized) return;
        //GET 2nd mouse click to demagnetize (incase raycast is disabled) //
        if (Input.GetMouseButtonUp(0)) {
            //FIRST mouse click in OnPointerClick bleeds through          //
            if (!acceptNextDemagClick) acceptNextDemagClick = true;
            //DEMAGNETIZE on 2nd mouse click                              //
            else Demagnetize();
        }
    }

/////////////
// Methods //
/////////////

    public override void Demagnetize() {
        base.Demagnetize();
        acceptNextDemagClick = false;
    }

//////////////////////////
// Unity Event Handlers //
//////////////////////////

    public void OnPointerClick(PointerEventData e) {
        if (dontMagnetizeOnClick) return;
        if (isMagnetized) Demagnetize();
        else Magnetize();
    }

    public void OnDrag(PointerEventData e) {

    }

    public void OnBeginDrag(PointerEventData e) {
        Magnetize();
        dontMagnetizeOnClick = true;
    }

    public void OnEndDrag(PointerEventData e) {
        Demagnetize();
        dontMagnetizeOnClick = false;
    }
}


/***************************************************************************/ }
