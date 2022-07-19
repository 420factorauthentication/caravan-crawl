using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CardEngine;
using CardEffectEngine;
using ConditionEngine;
using GuiEngine;
using DelegateEngine;


// ============================================ //
// A card object that exists in a player's hand //
// ============================================ //
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]

public class HandCard : CanvasDraggable,
                        IPointerEnterHandler,
                        IPointerExitHandler {

///////////////////////////
// Properties and Fields //
///////////////////////////

    Image img;

    // -- TODO: Property set functions -- //
    public CardStats stats;
    public int handSlot = -1;  // -1 means unset by hand //

    // -- Used by Hand; set alpha when cursor changes -- //
    public CanvasGroup cg {get; private set;}

    // Checked when the player attempts to play the card //
    public Condition conditions;
    public List<ICardEffect> effects;

    // Used to update hand position on resolution change //
    int lastScreenWidth = Screen.width;

////////////////////
// Unity Messages //
////////////////////

    void OnDestroy() {
        //CLEANUP event handlers from static event //
        AnyHandCardMagnet -= OnAnyHandCardMagnet;
        AnyHandCardDemagnet -= OnAnyHandCardDemagnet;
    }

    protected override void Awake() {
        base.Awake();
        //COMPONENT handles for frequent tasks     //
        cg = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();
        //ALL existing cards sub to 1 static event //
        AnyHandCardMagnet += OnAnyHandCardMagnet;
        AnyHandCardDemagnet += OnAnyHandCardDemagnet;
        //INIT object transform width and height   //
        float x = HandCardSize.Width / 2;
        float y = HandCardSize.Height / 2;
        tr.offsetMin = new Vector2(-x, -y);
        tr.offsetMax = new Vector2(x, y);
        tr.localScale = HandCardSize.ScaleVec3;
    }

    protected override void Update() {
        base.Update();
        //UPDATE hand position on resolution change //
        //TODO: Create res manager and use an event //
        if (Screen.width != lastScreenWidth)
            ResetPos();
        lastScreenWidth = Screen.width;
    }

/////////////
// Methods //
/////////////

    // Gets world coords for a hand slot //
    public static Vector3 GetHandPos(int handSlot) {
        int cards = Hand.cards.Count;
        float median = cards / 2f - 0.5f;
        //GET horizontal offset from center of screen     //
        //IF ALL cards fit in hand, lay them edge to edge //
        float x = HandCardSize.Width * (handSlot - median);
        //OTHERWISE, overlap all cards equally            //
        float fitCards = HandSize.Width / HandCardSize.Width;
        if (fitCards < cards)
            x = x * (fitCards - 1f) / (cards - 1f);
        //TRANSLATE horizontal offset to left offset      //
        x = x + (Screen.width / 2f);
        float y = HandCardSize.Height / 2f;
        return new Vector3(x, y, 0);
    }

    // Moves this card back to it's hand slot and resets other settings //
    public void ResetPos() {
        tr.localPosition = GetHandPos(handSlot);
        cg.alpha = 1f;  //UNHIDE incase it was hidden for targeting reticle
    }

    // Attaches card to cursor (position follows the cursor on Update) //
    // -- Called by CanvasDraggable while dragging or when clicked -- //
    public override void Magnetize() {
        base.Magnetize();
        tr.SetAsLastSibling(); //BRING to front
        Cursor.visible = false;
        CurrMagnetized = this;
        _AnyHandCardMagnet?.Invoke();
    }

    // Detaches card from cursor and plays card if hovering valid target //
    // -- Called by CanvasDraggable when drag ends or on second click -- //
    public override void Demagnetize() {
        base.Demagnetize();
        tr.SetSiblingIndex(handSlot); //BRING to back
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
        CurrMagnetized = null;
        _AnyHandCardDemagnet?.Invoke();
        if (conditions.Evaluate()) PlayCard(); else ResetPos();
    }

    // Plays this card on a target then removes from hand //
    void PlayCard() {
        foreach(ICardEffect cardEffect in effects) {
            cardEffect.Activate();
        }
        //TODO: remove card//
    }

////////////
// Events //
////////////

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
    // When any card is magnetized, turns off raycast on all cards //
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
    public static bool IsAnyMagnetized {get; private set;} = false;
    public static HandCard CurrMagnetized {get; private set;}

    static GenericEventHandler _AnyHandCardMagnet
        = delegate { IsAnyMagnetized = true; };
    static GenericEventHandler _AnyHandCardDemagnet
        = delegate { IsAnyMagnetized = false; };

    public static event GenericEventHandler AnyHandCardMagnet {
        add {
            _AnyHandCardMagnet += value;
            if (IsAnyMagnetized) value();   }
        remove {
            _AnyHandCardMagnet -= value;    }
    }

    public static event GenericEventHandler AnyHandCardDemagnet {
        add {
            _AnyHandCardDemagnet += value;
            if (!IsAnyMagnetized) value();  }
        remove {
            _AnyHandCardDemagnet -= value;  }
    }


////////////////////
// Event Handlers //
////////////////////

    void OnAnyHandCardMagnet() {
        img.raycastTarget = false;
    }

    void OnAnyHandCardDemagnet() {
        img.raycastTarget = true;
    }

    public static void OnNewObjHover() {
        //DETERMINE if mouse hovering valid target //

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
