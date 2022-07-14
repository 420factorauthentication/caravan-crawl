using UnityEngine;
namespace HexEngine /*;*/ {


// ======================================== //
// Math measurements to construct a hexagon //
// ======================================== //
public struct HexGeo {
    public HexGeo(float side, float rotDeg) {
        Side = side;
        Apo = Mathf.Sqrt(3f) / 2f * side;
        RotDeg = rotDeg;
    }

///////////////////////////
// Properties and Fields //
///////////////////////////

    // side length // circumcircle radius //
    public float Side {get; private set;}

    // apothem // incircle radius //
    public float Apo {get; private set;}

    // rotation (deg) from point top //
    // rotate 30 deg to get flat top //
    public float RotDeg;

/////////////
// Methods //
/////////////

    // Set hexagon dimensions //
    public void SetSideLen(float side) {
        Side = side;
        Apo = Mathf.Sqrt(3f) / 2f * side;
    }

    // Get world coords clockwise from origin //
    public Vector3[] GetVerts() {
        float rot = RotDeg * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rot);
        float sin = Mathf.Sin(rot);

        float x1 = Apo*cos - Side/2f*sin;
        float y1 = Apo*sin + Side/2f*cos;

        float x2 = 0f*cos - Side*sin;
        float y2 = 0f*sin + Side*cos;

        float x3 = -Apo*cos - Side/2f*sin;
        float y3 = -Apo*sin + Side/2f*cos;

        float x4 = -Apo*cos - -Side/2f*sin;
        float y4 = -Apo*sin + -Side/2f*cos;

        float x5 = 0f*cos - -Side*sin;
        float y5 = 0f*sin + -Side*cos;

        float x6 = Apo*cos - -Side/2f*sin;
        float y6 = Apo*sin + -Side/2f*cos;

        return new Vector3[] {
            new Vector3 (0f, 0f, 0f),
            new Vector3 (x1, 0f, y1),
            new Vector3 (x2, 0f, y2),
            new Vector3 (x3, 0f, y3),
            new Vector3 (x4, 0f, y4),
            new Vector3 (x5, 0f, y5),
            new Vector3 (x6, 0f, y6),
        };
    }
}




// =============================================== //
// Axial coordinates of a tile on a hexagonal grid //
// =============================================== //
public struct AxHexVec2 {
    public AxHexVec2(float q, float r, HexGeo hexGeo) {
        Q = q;
        R = r;
        HexGeo = hexGeo;
    }

///////////////////////////
// Properties and Fields //
///////////////////////////

    public float Q;        //Column
    public float R;        //Row
    public HexGeo HexGeo;  //The measurements for every tile on this grid

/////////////
// Methods //
/////////////

    // Get coordinates converted from Axial Basis to Standard Basis //
    public Vector3 ToWorld() {
        float rot = HexGeo.RotDeg * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rot);
        float sin = Mathf.Sin(rot);

        float x = 2f*HexGeo.Apo * Q  +  HexGeo.Apo * R;
        float y = 1.5f*HexGeo.Side * R;

        float x1 = x*cos - y*sin;
        float y1 = x*sin + y*cos;

        return new Vector3(x1,0f,y1);
    }
}


/***************************************************************************/ }
