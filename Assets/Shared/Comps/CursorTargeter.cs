using UnityEngine;
using DelegateEngine;
using HoverEngine;


// ================================================ //
// Singleton that manages cursor hover interactions //
// ================================================ //
public class CursorTargeter : MonoBehaviour {
    public static CursorTargeter Manager {get; private set;}
    public static RaycastHit OldRayHit {get; private set;}
    public static RaycastHit NewRayHit {get; private set;}
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
        if (hit.collider == NewRayHit.collider) return;
        //COLLIDER can be null, which means mouse is hovering no obj //
        OldRayHit = NewRayHit;
        NewRayHit = hit;
        NewObjHover?.Invoke();
    }
}
