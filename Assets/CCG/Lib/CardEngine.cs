using UnityEngine;
namespace CardEngine /*;*/ {


// ================================================================ //
// Gameplay stats and art fields to create a unique card definition //
// ================================================================ //
public struct CardStats {

}




// =============================================== //
// Constants defining pixel size of a card in hand //
// =============================================== //
public static class HandCardSize {
    public const float Width = 60f;
    public const float Height = 80f;
    public const float Scale = 1f;
    public const float ZoomScale = 2.5f;
    public const float ZoomOffsetX = 0f;
    public const float ZoomOffsetY = 40f;

    public static Vector2 SizeVec2 {
        get { return new Vector2(Width, Height); } }

    public static Vector3 ScaleVec3 {
        get { return new Vector3(Scale, Scale, 1f); } }

    public static Vector3 ZoomScaleVec3 {
        get { return new Vector3(ZoomScale, ZoomScale, 1f); } }

    public static Vector3 ZoomOffsetVec3 {
        get { return new Vector3(ZoomOffsetX, ZoomOffsetY, 0); } }
}




// ============================================================== //
// Constants defining pixel size of GUI area containing all cards //
// ============================================================== //
public static class HandSize {
    public const float ScreenWidthFactor = 0.5f;
    public const float ScreenHeightFactor = 0.225f;

    public static float Width {
        get { return Screen.width * ScreenWidthFactor; } }

    public static float Height {
        get { return Screen.height * ScreenHeightFactor; } }
}


/***************************************************************************/ }
