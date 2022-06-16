using UnityEngine;
using UnityEngine.UI;
using CardEngine;


//Singleton that shows a zoomed preview of cards hovered in hand//
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]

public class HandCardZoom : MonoBehaviour {
    RectTransform tr;
    Image img;

    void Awake() {
        //Subscribe to events
        HandCard.ZoomEvent += OnZoomEvent;
        HandCard.UnzoomEvent += OnUnzoomEvent;
        //Init component handles
        tr = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        //Init size
        float x = HandCardSize.Width / 2;
        float y = HandCardSize.Height / 2;
        tr.offsetMin = new Vector2(-x, -y);
        tr.offsetMax = new Vector2(x, y);
        tr.localScale = HandCardSize.GetZoomScale();
        //Orient position to bottom left
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.zero;
        tr.position = Vector3.zero;
        //Orient movement to center
        tr.pivot = new Vector2(0.5f, 0.5f);
        //Turn off raycasting
        img.raycastTarget = false;
        //Initially invisible
        img.color = Color.clear;
    }

    //Update graphics and text
    public void SetUI(CardStats stats) {
        
    }

    //When a HandCard is hovered, move, update, and unhide this zoomed image
    void OnZoomEvent(int handSlot, CardStats stats) {
        SetUI(stats);
        tr.localPosition = HandCard.GetHandPos(handSlot);
        img.color = Color.white;
    }

    void OnUnzoomEvent() {
        img.color = Color.clear;
    }
}
