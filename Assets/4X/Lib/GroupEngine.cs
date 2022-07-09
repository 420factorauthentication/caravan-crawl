using UnityEngine;
using HexEngine;
namespace GroupEngine /*;*/ {


// ======================================================================== //
// A GameObject that automatically arranges children positions in a pattern //
// ======================================================================== //
public abstract class GroupBase : MonoBehaviour {

/////////////
// Methods //
/////////////

    protected abstract void ArrangeChildren();

    protected GameObject AddChild() {
        GameObject obj = new();
        obj.transform.SetParent(transform);
        ArrangeChildren();
        return obj;
    }

    protected GameObject[] AddChildren(int count) {
        GameObject[] objs = new GameObject[count];
        for (int i = 0; i < count; i++) {
            objs[i] = new GameObject();
            objs[i].transform.SetParent(transform);
        }
        ArrangeChildren();
        return objs;
    }

    protected void RemoveChild(int index) {
        if ((index >= 0) && (index < transform.childCount)) {
            Destroy(transform.GetChild(index));
            ArrangeChildren();
        }
    }

    protected void RemoveChild(GameObject obj) {
        if (obj.transform.parent == transform) {
            Destroy(obj);
            ArrangeChildren();
        }
    }

    protected void RemoveChild(Component comp) {
        if (comp.transform.parent == transform) {
            Destroy(comp.gameObject);
            ArrangeChildren();
        }
    }
}




// ========================================================================= //
// Arranges children GameObjects in a parallelogram that fits in HexGrid hex //
// ========================================================================= //
public abstract class GroupHex : GroupBase {

/////////////
// Methods //
/////////////

    protected override void ArrangeChildren() {
        HexGeo geo = new(HexGrid.scale, HexGrid.rotDeg);
        int children = transform.childCount;
        int cPerRow = 1;
        while ((cPerRow*cPerRow) < children) cPerRow++;
        int cRows = Mathf.CeilToInt((float) children / cPerRow);
        int cCols = cPerRow;
        int cRowPaddings = cCols + 1;
        int cColPaddings = cRows + 1;
        for (int i = 0; i < children; i++) {
            int eCol = (i % cPerRow) + 1;
            int eRow = (i / cPerRow) + 1;
            float localQ = ((float) eCol / cRowPaddings) - 0.5f;
            float localR = ((float) eRow / cColPaddings) - 0.5f;
            AxHexVec2 coords = new(localQ, localR, geo);
            transform.GetChild(i).localPosition = coords.ToWorld();
        }
    }
}


/***************************************************************************/ }
