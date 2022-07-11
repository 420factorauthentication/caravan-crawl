using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CardEngine;
using CursorEngine;


// ==================================== //
// Singleton that manages cards in hand //
// ==================================== //
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]

public class Hand : MonoBehaviour,
                    IPointerEnterHandler,
                    IPointerExitHandler {

    public static Hand Manager {get; private set;}

////////////////
// Properties //
////////////////

    // Index == slot in hand //
    static List<HandCard> _cards = new();
    public static IReadOnlyList<HandCard> cards => _cards.AsReadOnly();

    // Manipulate hand UI //
    static RectTransform tr;

////////////////////
// Unity Messages //
////////////////////

    void Awake() {
        //CREATE singleton instance              //
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
        //INIT component handles                 //
        tr = GetComponent<RectTransform>();
        //ORIENT position to bottom left         //
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.zero;
        tr.position = Vector3.zero;
        tr.pivot = Vector2.zero;
        //INIT collision size (not display size) //
        //TODO: update on resolution change      //
        float x = Screen.width;
        float y = HandSize.GetHeight();
        tr.offsetMin = Vector2.zero;
        tr.offsetMax = new Vector2(x, y);
        //INIT clear image for raycasting        //
        Image img = GetComponent<Image>();
        img.color = Color.clear;

        Test(); //TODO//
    }

/////////////
// Methods //
/////////////

    // Adds a card to the end of the hand //
    public static void PushCard(CardStats stats) {
        GameObject obj = new();
        obj.transform.SetParent(tr);
        obj.name = "Card" + cards.Count;
        HandCard card = obj.AddComponent<HandCard>();
        card.stats = stats;
        card.handSlot = cards.Count;
        _cards.Add(card);
        for (int i = 0; i < cards.Count; i++)
            cards[i].ResetPos();
    }

    // NOTE: Moves cards ahead of slot backwards to fill the gap //
    public static void RemoveCardAt(int slot) {
        if (cards[slot] == null) return;
        GameObject obj = cards[slot].gameObject;
        _cards.RemoveAt(slot);
        Destroy(obj);
        for (int i = 0; i < cards.Count; i++) {
            cards[i].handSlot = i;
            cards[i].ResetPos();
            cards[i].gameObject.name = "Card" + i;
        }
    }

    // -- TODO --//
    void Test() {
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        PushCard(new CardStats());
        for (int i = 0; i < cards.Count; i++) {
            if (i % 2 == 0)
                cards[i].GetComponent<Image>().color = Color.black;
        }
    }

////////////////////
// Event Handlers //
////////////////////

    // When cursor leaves deck area, change reticle if a card is magnetized //
    public void OnPointerEnter(PointerEventData e) {
        if (!HandCard.IsAnyMagnetized) return;
        CursorManager.SetCursor(CursorTex.Default);
        Cursor.visible = false;
        HandCard.CurrMagnetized.cg.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData e) {
        if (!HandCard.IsAnyMagnetized) return;
        CursorManager.SetCursor(CursorTex.Build);
        Cursor.visible = true;
        HandCard.CurrMagnetized.cg.alpha = 0f;
    }
}
