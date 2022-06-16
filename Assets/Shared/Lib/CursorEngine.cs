using System.IO;
using UnityEngine;


namespace CursorEngine {

    // Singleton to change cursor textures //
    public class CursorTex {
        public CursorTex() {
            BuildTex = new Texture2D(64, 64);
            BuildTex.LoadImage(File.ReadAllBytes(BuildPath));
        }

        public static CursorTex Manager = new();

        public Texture2D BuildTex { get; private set; }

        public const string BuildPath = "Assets/CCG/Art/cursor-build-32.png";
    }
}
