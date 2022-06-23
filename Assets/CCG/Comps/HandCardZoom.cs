using UnityEngine;
using UnityEngine.UI;
using CardEngine;


//Singleton that shows a zoomed preview of cards hovered in hand//
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]

public class HandCardZoom : MonoBehaviour {
    public static HandCardZoom Manager {get; private set;}
    RectTransform tr;
    Image img;

    void Awake() {
        //Create singleton instance
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
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
    public void Zoom(int handSlot, CardStats stats) {
        SetUI(stats);
        tr.localPosition = HandCard.GetHandPos(handSlot);
        img.color = Color.white;
    }

    public void Unzoom() {
        img.color = Color.clear;
    }
}
