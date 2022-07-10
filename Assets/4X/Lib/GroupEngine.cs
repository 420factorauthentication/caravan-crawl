using UnityEngine;
using HexEngine;
namespace GroupEngine /*;*/ {


// ========================================================= //
// Auto-arranges transform positions of children GameObjects //
// ========================================================= //
public abstract class GroupBase : MonoBehaviour {

/////////////
// Methods //
/////////////

    protected abstract void ArrangeChildren();

    public GameObject AddChild() {
        GameObject obj = new();
        obj.transform.SetParent(transform);
        ArrangeChildren();
        return obj;
    }

    public GameObject[] AddChildren(int count) {
        GameObject[] objs = new GameObject[count];
        for (int i = 0; i < count; i++) {
            objs[i] = new GameObject();
            objs[i].transform.SetParent(transform);
        }
        ArrangeChildren();
        return objs;
    }

    public void RemoveChild(int index) {
        if ((index >= 0) && (index < transform.childCount)) {
            Destroy(transform.GetChild(index));
            ArrangeChildren();
        }
    }

    public void RemoveChild(GameObject obj) {
        if (obj.transform.parent == transform) {
            Destroy(obj);
            ArrangeChildren();
        }
    }

    public void RemoveChild(Component comp) {
        if (comp.transform.parent == transform) {
            Destroy(comp.gameObject);
            ArrangeChildren();
        }
    }
}




/***************************************************************************/ }
