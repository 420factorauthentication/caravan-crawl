using System;
using UnityEngine;


namespace CardEngine {

    public struct CardStats {
        
    }

    public static class HandCardSize {
        public const float Width = 60f;
        public const float Height = 80f;
        public const float Scale = 1f;
        public const float ZoomScale = 2.5f;
        public const float ZoomOffsetX = 0f;
        public const float ZoomOffsetY = 40f;

        public static Vector3 GetUnzoomScale() {
            return new Vector3(Scale, Scale, 1f);
        }

        public static Vector3 GetZoomScale() {
            return new Vector3(ZoomScale, ZoomScale, 1f);
        }

        public static Vector3 GetZoomOffset() {
            return new Vector3(ZoomOffsetX, ZoomOffsetY, 0);
        }
    }

    public static class HandSize {
        public const float ScreenWidthFactor = 0.5f;
        public const float ScreenHeightFactor = 0.225f;

        public static float GetWidth() {
            return Screen.width * ScreenWidthFactor;
        }

        public static float GetHeight() {
            return Screen.height * ScreenHeightFactor;
        }
    }
}
