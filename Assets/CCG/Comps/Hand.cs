using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CardEngine;
using CursorEngine;


// =============================================================== //
// Singleton that manages existing card objects in a player's hand //
// =============================================================== //
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]

public class Hand : MonoBehaviour,
                    IPointerEnterHandler,
                    IPointerExitHandler {

    public static Hand Manager {get; private set;}

///////////////////////////
// Properties and Fields //
///////////////////////////

    static RectTransform tr;

    // -- Index is slot in hand -- //
    static List<HandCard> _cards = new();
    public static IReadOnlyList<HandCard> cards => _cards.AsReadOnly();

////////////////////
// Unity Messages //
////////////////////

    void Awake() {
        //CREATE singleton instance              //
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
        //COMPONENT handles for frequent tasks   //
        tr = GetComponent<RectTransform>();
        //ORIENT position to bottom left         //
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.zero;
        tr.position  = Vector3.zero;
        tr.pivot     = Vector2.zero;
        //INIT collision size (not display size) //
        //TODO: update on resolution change      //
        float x = Screen.width;
        float y = HandSize.Height;
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

    // Adds card(s) to the end of the hand //
    public static void PushCards(CardStats stats, int count = 1) {
        for (int i = 0; i < count; i++) {
            GameObject obj = new();
            obj.transform.SetParent(tr);
            obj.name = "Card" + cards.Count;
            HandCard card = obj.AddComponent<HandCard>();
            card.stats = stats;
            card.handSlot = cards.Count;
            _cards.Add(card);
        }
        for (int i = 0; i < cards.Count; i++)
            cards[i].ResetPos();
    }

    // -- After removing, slot indices are re-sorted like a List  -- //
    // -- TODO: Handle removing a card that is currently dragging -- //
    public static void RemoveCardsAt(int slot, int count = 1) {
        if (slot >= cards.Count) return;
        for (int i = 0; i < count; i++) {
            if (slot >= cards.Count) break;
            GameObject obj = cards[slot].gameObject;
            _cards.RemoveAt(slot);
            Destroy(obj);
        }
        for (int i = 0; i < cards.Count; i++) {
            cards[i].handSlot = i;
            cards[i].ResetPos();
            cards[i].gameObject.name = "Card" + i;
        }
    }

    // -- TODO -- //
    void Test() {
        // StartCoroutine(TestAddCards());
        StartCoroutine(TestCardConds());
        StartCoroutine(TestCardEffects());
    }
    IEnumerator TestAddCards() {
        PushCards(new CardStats(), 5);
        cards[0].GetComponent<Image>().color = Color.red;
        cards[1].GetComponent<Image>().color = Color.blue;
        cards[2].GetComponent<Image>().color = Color.green;
        cards[3].GetComponent<Image>().color = Color.yellow;
        cards[4].GetComponent<Image>().color = Color.magenta;
        yield return new WaitForSeconds(1f);
        RemoveCardsAt(3, 42069);  //SHOULD only remove yellow and magenta //
    }
    IEnumerator TestCardConds() {
        PushCards(new CardStats(), 5);
        //cards[0]
    }
    IEnumerator TestCardEffects() {

    }

////////////////////
// Event Handlers //
////////////////////

    // When cursor leaves hand area, changes reticle if a card is magnetized //
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
