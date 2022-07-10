using UnityEngine;
using GroupEngine;
using HexEngine;


// ========================================================================= //
// Arranges children GameObjects in a parallelogram that fits in HexGrid hex //
// ========================================================================= //
public class GroupHex : GroupBase {

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
