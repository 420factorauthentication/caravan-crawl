using UnityEngine;
using GroupEngine;
using HexEngine;


// ====================================================================== //
// Arranges children GameObjects into a parallelogram that fits on a Node //
// ====================================================================== //
public class GroupHex : GroupBase {

    protected override void ArrangeChildren() {
        //DIVIDE parallelogram into equal areas                         //
        //PLACE each child into their own area. Fill row-wise first.    //
        //EACH row/col has  ceil(sqrt(children))  areas                 //
        int children = transform.childCount;
        int cPerRow = 1;
        while ((cPerRow*cPerRow) < children) cPerRow++;
        //NUMBER of rows (there will always be cPerRow cols)            //
        int cRows = Mathf.CeilToInt((float) children / cPerRow);
        //NUMBER of spaces inbetween rows/cols (number + 1)             //
        int cRowPaddings = cPerRow + 1;
        int cColPaddings = cRows + 1;
        //SHIFT each child a fraction of a Node size along q and r      //
        HexGeo geo = new(HexGrid.scale, HexGrid.rotDeg);
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
