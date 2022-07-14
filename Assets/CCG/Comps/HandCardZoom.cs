using UnityEngine;
using UnityEngine.UI;
using CardEngine;


// ================================================================ //
// Singleton that shows a zoomed preview of a HandCard when hovered //
// ================================================================ //
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]

public class HandCardZoom : MonoBehaviour {
    public static HandCardZoom Manager {get; private set;}
    static RectTransform tr;
    static Image img;

    void Awake() {
        //CREATE singleton instance       //
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
        //COMP handles for frequent tasks //
        tr = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        //INIT transform width and height //
        float x = HandCardSize.Width / 2;
        float y = HandCardSize.Height / 2;
        tr.offsetMin = new Vector2(-x, -y);
        tr.offsetMax = new Vector2(x, y);
        tr.localScale = HandCardSize.ZoomScaleVec3;
        //ORIENT position to bottom left  //
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.zero;
        tr.position = Vector3.zero;
        //ORIENT movement to center       //
        tr.pivot = new Vector2(0.5f, 0.5f);
        //PREVENT mouse hovering this     //
        img.raycastTarget = false;
        //INITIALLY hide card preview     //
        img.color = Color.clear;
    }

    // Updates graphics/text to match hovered card //
    public static void SetUI(CardStats stats) {

    }

    // Updates, unhides, moves to mouse cursor //
    // -- Called by HandCard OnPointerEnter -- //
    public static void Zoom(int handSlot, CardStats stats) {
        SetUI(stats);
        tr.localPosition = HandCard.GetHandPos(handSlot);
        img.color = Color.white;
    }

    // -- Called by HandCard OnPointerExit -- //
    public static void Unzoom() {
        img.color = Color.clear;
    }
}
