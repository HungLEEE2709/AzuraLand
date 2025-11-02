using System;
using UnityEngine;
// Avoid direct dependency on UnityEngine.UI so external dotnet builds (outside Unity Editor)
// can compile. We implement a minimal local substitute for the members used here.

namespace Mod.Graphics
{
    // We derive from MonoBehaviour and provide the minimal API used by the mod so the
    // project can compile when the UnityEngine.UI assembly is not referenced by the
    // external build system. In Unity Editor the behaviour will be similar enough for
    // this project's usage.
    internal class UIImage : MonoBehaviour
    {
        internal static Mesh mesh = new Mesh();
        static Texture2D texture;
        internal static UIImage image;

        // Minimal representation of the UI.Image 'type' and fill method used in this file.
        internal enum Type { Filled }
        internal enum FillMethod { Radial360 }

        // Fields that mimic UnityEngine.UI.Image members used by the code. Use fully-qualified
        // UnityEngine types where necessary to avoid name collisions with project types.
        public UnityEngine.Sprite sprite;
        public Type type;
        public FillMethod fillMethod;
        public bool fillClockwise;

        // Additional members used by other code paths (CustomGraphics expects these).
        public RectTransform rectTransform;
        public float fillAmount;
        public Color color;
        public Material material;

        // This method is kept so callers (PopulateMesh) can invoke it. When the real
        // UnityEngine.UI.Image exists in an actual Unity runtime, it has its own
        // OnPopulateMesh override; here we simply capture the mesh for tooling purposes.
        internal void OnPopulateMesh(Mesh m)
        {
            mesh = m;
        }

        internal static void PopulateMesh() => image.OnPopulateMesh(mesh);

        internal static void OnStart()
        {
            GameObject mainCamera = GameObject.Find("Main Camera");
            Canvas canvas = mainCamera.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            GameObject uiImageCooldown = new GameObject("Cooldown Effect");
            image = uiImageCooldown.AddComponent<UIImage>();
            // Ensure a RectTransform exists (UI layout uses RectTransform)
            uiImageCooldown.AddComponent<RectTransform>();
            image.rectTransform = uiImageCooldown.GetComponent<RectTransform>();
            texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.black);
            texture.Apply();
            image.sprite = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            image.type = Type.Filled;
            image.fillMethod = FillMethod.Radial360;
            image.fillClockwise = true;
            image.fillAmount = 1f;
            image.color = Color.white;
            // Provide a simple material so callers can call material.SetPass(0) safely.
            Shader shader = Shader.Find("Sprites/Default");
            image.material = shader != null ? new Material(shader) : new Material(Shader.Find("Standard"));
            uiImageCooldown.transform.SetParent(mainCamera.transform);
            uiImageCooldown.transform.position = new Vector3(-500, -500);
        }
    }
}