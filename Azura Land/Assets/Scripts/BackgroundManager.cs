using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Quan trọng: Cần import để chuyển Scene
using System.Collections;

namespace GameScripts
{
    public class BGManager : MonoBehaviour
    {
        public Image bgTraiDat;
        public Image bgNamec;
        public Image bgXayda;

        public CharacterManager characterManager; 
        public InputField characterNameInput; 
        public GameObject characterCreationPanel; 

        private Image currentBG;

        void Start()
        {
            // Thiết lập trạng thái ban đầu
            currentBG = bgTraiDat;
            SetAlpha(bgTraiDat, 1);
            SetAlpha(bgNamec, 0);
            SetAlpha(bgXayda, 0);

            // Đảm bảo Panel tạo nhân vật đang đóng khi Start
            if (characterCreationPanel != null)
                characterCreationPanel.SetActive(false);
        }

        // --- BACKGROUND LOGIC ---

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

        public void OnCloseCharacterCreation()
        {
            Debug.Log("Đóng giao diện tạo nhân vật.");
            SceneManager.LoadScene("StartUI");
        }

        public void OnCreateCharacter()
        {
            if (characterNameInput == null)
            {
                Debug.LogError("Chưa gán InputField cho characterNameInput.");
                return;
            }

            string playerName = characterNameInput.text.Trim();

            // 1. Kiểm tra điều kiện tên nhân vật
            if (string.IsNullOrEmpty(playerName))
            {
                Debug.LogWarning("Vui lòng nhập tên nhân vật!");
                // (Bạn nên hiển thị một thông báo UI cho người chơi ở đây)
                return;
            }

            if (playerName.Length < 5)
            {
                Debug.LogWarning("Tên nhân vật phải có ít nhất 5 ký tự.");
                return;
            }

            Debug.Log($"Nhân vật {playerName} đã được tạo thành công!");

            // (LƯU Ý: Nếu bạn cần lưu trữ dữ liệu nhân vật, hãy làm ở đây)
            // Ví dụ: PlayerPrefs.SetString("PlayerName", playerName);

            SceneManager.LoadScene("QuantumGameScene");
        }
    }
}