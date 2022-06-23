using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CardEngine;
using GuiEngine;
using EventEngine;


[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]

public class HandCard : CanvasDraggable,
                        IPointerEnterHandler,
                        IPointerExitHandler {

////////////////
// Properties //
////////////////

    //Params to add new card to hand
    public int handSlot = -1;  // -1 means unset by hand
    public CardStats stats;

    //Manipulate hand UI
    public CanvasGroup cg {get; private set;}
    public Image img {get; private set;}

    //Play card if target valid
    bool isTargetValid = false;

    //Update hand position on resolution change
    int lastScreenWidth = Screen.width;

////////////////////
// Unity Messages //
////////////////////

    void OnDestroy() {
        //Cleanup event handlers from static event
        AnyHandCardMagnet -= OnAnyHandCardMagnet;
        AnyHandCardDemagnet -= OnAnyHandCardDemagnet;
    }

    protected override void Awake() {
        base.Awake();
        //Event handlers from all existing cards sub to one static event
        AnyHandCardMagnet += OnAnyHandCardMagnet;
        AnyHandCardDemagnet += OnAnyHandCardDemagnet;
        //Init component handles
        cg = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();
        //Init size
        float x = HandCardSize.Width / 2;
        float y = HandCardSize.Height / 2;
        tr.offsetMin = new Vector2(-x, -y);
        tr.offsetMax = new Vector2(x, y);
        tr.localScale = HandCardSize.GetUnzoomScale();
    }

    protected override void Update() {
        base.Update();
        //Update hand position on resolution change
        //TODO: Create resolution manager and use a res change event
        if (Screen.width != lastScreenWidth)
            ResetPos();
        lastScreenWidth = Screen.width;
        //Determine if hovering over valid target
    }

/////////////
// Methods //
/////////////

    //Play this card on a target then remove from hand
    public void PlayCard(/*Target t*/) {
        
    }

    //Get card position based on hand slot
    public static Vector3 GetHandPos(int handSlot) {
        int cards = Hand.cards.Count;
        float median = cards / 2f - 0.5f;
        //Get horiz offset from center of screen
        //If all cards fit, lay them edge to edge
        float x = HandCardSize.Width * (handSlot - median);
        //Else, overlap them
        float fitCards = HandSize.GetWidth() / HandCardSize.Width;
        if (fitCards < cards)
            x = x * (fitCards - 1f) / (cards - 1f);
        //Translate horiz offset to left offset
        x = x + (Screen.width / 2f);
        //Return position
        float y = HandCardSize.Height / 2f;
        return new Vector3(x, y, 0);
    }

    //Set card position based on hand slot
    public void ResetPos() {
        tr.localPosition = GetHandPos(handSlot);
        cg.alpha = 1f;  //Unhide, incase it was hidden for targeting reticle 
    }

    //Attach card to cursor while dragging or when clicked
    public override void Magnetize() {
        base.Magnetize();
        tr.SetAsLastSibling(); //Bring to front
        Cursor.visible = false;
        CurrMagnetized = this;
        AnyHandCardMagnet?.Invoke();
    }

    //Detach card from cursor and play card if valid target
    public override void Demagnetize() {
        base.Demagnetize();
        tr.SetSiblingIndex(handSlot); //Bring to back
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
        CurrMagnetized = null;
        AnyHandCardDemagnet?.Invoke();
        if (isTargetValid) PlayCard(); else ResetPos();
    }


///////////////////
// Event Senders //
///////////////////

    //When any card is magnetized, turn off raycast on all cards
    public static bool IsAnyMagnetized = false;
    public static HandCard CurrMagnetized;

    public static event GenericEventHandler AnyHandCardMagnet
        = delegate { IsAnyMagnetized = true; };
    public static event GenericEventHandler AnyHandCardDemagnet
        = delegate { IsAnyMagnetized = false; };

////////////////////
// Event Handlers //
////////////////////

    void OnAnyHandCardMagnet() {
        img.raycastTarget = false;
    }

    void OnAnyHandCardDemagnet() {
        img.raycastTarget = true;
    }

//////////////////////////
// Unity Event Handlers //
//////////////////////////

    public void OnPointerEnter(PointerEventData e) {
        HandCardZoom.Zoom(handSlot, stats);
    }

    public void OnPointerExit(PointerEventData e) {
        HandCardZoom.Unzoom();
    }
}
