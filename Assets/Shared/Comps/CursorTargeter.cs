using UnityEngine;
using DelegateEngine;
using HoverEngine;


// ================================================ //
// Singleton that manages cursor hover interactions //
// ================================================ //
public class CursorTargeter : MonoBehaviour {
    public static CursorTargeter Manager {get; private set;}
    public static RaycastHit OldObjHit {get; private set;}
    public static RaycastHit NewObjHit {get; private set;}
    public static event GenericEventHandler NewObjHover;

    void Start() {
        NewObjHover += HoverParent.OnNewObjHover;
    }

    void Awake() {
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
    }

    void Update() {
        if (Camera.main == null) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider == NewObjHit.collider) return;
        //COLLIDER can be null, which means mouse is hovering no obj //
        OldObjHit = NewObjHit;
        NewObjHit = hit;
        NewObjHover?.Invoke();
    }
}
