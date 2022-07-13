using UnityEngine;
namespace CursorEngine /*;*/ {


// =========================================== //
// Static methods for changing cursor textures //
// =========================================== //
public static class CursorManager {
    public static void SetCursor(CursorTex cursorTex) {
        Cursor.SetCursor(cursorTex.Tex, Vector2.zero, CursorMode.Auto);
    }
}




// ===================== //
// Enum: Cursor textures //
// ===================== //
public class CursorTex {
    public Texture2D Tex {get; private set;}
    public CursorTex(string resourceName) {
        if ((resourceName == null) || (resourceName == "")) return;
        Tex = Resources.Load<Texture2D>(resourceName);
    }

    public static CursorTex
        Default = new(null),
        Build = new("cursor-build-32");
}


/***************************************************************************/ }
