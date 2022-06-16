using UnityEngine;
using UnityEngine.EventSystems;


namespace GuiEngine {

    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||| //
    // Composite that magnetizes a Canvas Object to the cursor //
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||| //
    [RequireComponent(typeof(RectTransform))]

    public class CursorCanvasMagnet : MonoBehaviour {
        protected RectTransform tr;
        public bool isMagnetized {get; private set;} = false;

        protected virtual void Awake() {
            //Init component handles
            tr = GetComponent<RectTransform>();
            //Orient position to bottom left
            tr.anchorMin = Vector2.zero;
            tr.anchorMax = Vector2.zero;
            //Orient movement to center
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

        //Makes sure parent is oriented to bottom left
        //Do this somewhere on parent's RectTransform once
        public static void InitParentAnchor(RectTransform parentTr) {
            parentTr.anchorMin = Vector2.zero;
            parentTr.anchorMax = Vector2.zero;
            parentTr.position = Vector3.zero;
            parentTr.pivot = Vector2.zero;
        }
    }


    // |||||||||||||||||||||||||||||||||||||||||||||||||||||| //
    // Drag and drop || Click to pick-up/drop a Canvas Object //
    // |||||||||||||||||||||||||||||||||||||||||||||||||||||| //
    public class CanvasDraggable : CursorCanvasMagnet,
                                   IPointerClickHandler,
                                   IDragHandler,
                                   IBeginDragHandler,
                                   IEndDragHandler {

    ////////////////
    // Properties //
    ////////////////

        //Block 1st click event; Update() comes after OnPointerClick()
        bool acceptNextDemagClick = false;

        //Stop redundant click events if dragged
        bool dontMagnetizeOnClick = false;

    ////////////////////
    // Unity Messages //
    ////////////////////

        protected override void Update() {
            base.Update();
            if (!isMagnetized) return;
            //Get 2nd mouse click to demagnetize (incase raycast is disabled)
            if (Input.GetMouseButtonUp(0)) {
                //1st mouse click in OnPointerClick bleeds through
                if (!acceptNextDemagClick) acceptNextDemagClick = true;
                //Demagnetize on 2nd mouse click
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
}
