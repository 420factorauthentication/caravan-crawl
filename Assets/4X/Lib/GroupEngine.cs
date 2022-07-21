using System;
using UnityEngine;
namespace GroupEngine /*;*/ {


// ========================================================= //
// Auto-arranges transform positions of children GameObjects //
// ========================================================= //
public abstract class GroupBase : MonoBehaviour {

/////////////
// Methods //
/////////////

    // Arrange transform positions of children GameObjects in a pattern //
    protected abstract void ArrangeChildren();


    // Creates a new GameObject, sets its parent to this, then Arranges //
    // -- Adds the new GameObject to the end of the list of children -- //
    public GameObject AddChild() {
        GameObject obj = new();
        obj.transform.SetParent(transform);
        ArrangeChildren();
        return obj;
    }

    // -- More efficient way to use AddChild() multiple times -- //
    public GameObject[] AddChildren(int count) {
        GameObject[] objs = new GameObject[count];
        for (int i = 0; i < count; i++) {
            objs[i] = new GameObject();
            objs[i].transform.SetParent(transform);
        }
        ArrangeChildren();
        return objs;
    }


    // Removes and destroys a child GameObject, then Arranges //
    // -- After, children indices are re-sorted like Lists -- //
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
    }  //  --  Depth first search  --  //
    public void RemoveChild(Type componentType) {
        Component childComp = transform.GetComponentInChildren(componentType);
        if (childComp != null) {
            Destroy(childComp.gameObject);
            ArrangeChildren();
        }
    }
    public void RemoveChild<T>() {
        Component childComp = transform.GetComponentInChildren<T>();
        if (childComp != null) {
            Destroy(childComp.gameObject);
            ArrangeChildren();
        }
    }


    // -- More efficient way to use RemoveChild() multiple times -- //
    public void RemoveChildren(int index, int count) {
        /*  min = index */
        int max = index + count - 1;
        if ((index < 0) || (index > (transform.childCount - 1))) return;
        if   ((max < 0) ||   (max > (transform.childCount - 1))) return;
        for (int i = 0; i < count; i++)
            Destroy(transform.GetChild(index));
        ArrangeChildren();
    }
    public void RemoveChildren(GameObject[] objs) {
        foreach (GameObject obj in objs)
            if (obj.transform.parent == transform)
                Destroy(obj);
        ArrangeChildren();
    }
    public void RemoveChildren(Component[] comps) {
        foreach (Component comp in comps)
            if (comp.transform.parent == transform)
                Destroy(comp.gameObject);
        ArrangeChildren();
    }  //  --  Depth first search  --  //
    public void RemoveChildren(Type componentType, int count) {
        if (count <= 0) return;
        int i = 0;
        Component[] childComps = transform.GetComponentsInChildren(componentType);
        foreach (Component childComp in childComps) {
            Destroy(childComp.gameObject);
            if (++i >= count) break;
        } ArrangeChildren();
    }
    public void RemoveChildren<T>(int count) {
        if (count <= 0) return;
        int i = 0;
        Component[] childComps = transform.GetComponentsInChildren<T>();
        foreach (Component childComp in childComps) {
            Destroy(childComp.gameObject);
            if (++i >= count) break;
        } ArrangeChildren();
    }
}




/***************************************************************************/ }
