using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameScripts
{
    public class BGManager : MonoBehaviour
    {
        public Image bgTraiDat;
        public Image bgNamec;
        public Image bgXayda;

        public CharacterManager characterManager; // kéo CharacterManager vào Inspector

        private Image currentBG;

        void Start()
        {
            currentBG = bgTraiDat;
            SetAlpha(bgTraiDat, 1);
            SetAlpha(bgNamec, 0);
            SetAlpha(bgXayda, 0);
        }

        public void ShowPlanet(string planet)
        {
            Image nextBG = currentBG;
            switch (planet)
            {
                case "TraiDat": nextBG = bgTraiDat; break;
                case "Namec": nextBG = bgNamec; break;
                case "Xayda": nextBG = bgXayda; break;
            }

            if (nextBG != currentBG)
                StartCoroutine(SwitchBackground(nextBG));

            // Hiển thị nhân vật cho planet
            if (characterManager != null)
                characterManager.ShowCharactersForPlanet(planet);
        }

        IEnumerator SwitchBackground(Image nextBG)
        {
            float duration = 0.8f;
            float t = 0f;
            Image prevBG = currentBG;

            while (t < duration)
            {
                t += Time.deltaTime;
                float alpha = t / duration;
                SetAlpha(prevBG, 1 - alpha);
                SetAlpha(nextBG, alpha);
                yield return null;
            }

            SetAlpha(prevBG, 0);
            SetAlpha(nextBG, 1);
            currentBG = nextBG;
        }

        void SetAlpha(Image img, float alpha)
        {
            if (img == null) return;
            Color c = img.color;
            c.a = Mathf.Clamp01(alpha);
            img.color = c;
        }
    }
}
