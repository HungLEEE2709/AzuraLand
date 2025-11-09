using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameScripts
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject panelCharacter;
        public Button btnChar1;
        public Button btnChar2;

        [Header("Prefab nhân vật - Trái Đất")]
        public GameObject gohanPrefab;
        public GameObject yamchaPrefab;

        [Header("Prefab nhân vật - Namec")]
        public GameObject ocTieuPrefab;
        public GameObject kamiPrefab;

        [Header("Prefab nhân vật - Xayda")]
        public GameObject radicPrefab;
        public GameObject kakalotPrefab;

        private GameObject currentCharacter;

        void Start()
        {
            panelCharacter.SetActive(false);
        }

        // 🔹 Gọi khi chọn hành tinh
        public void ShowCharactersForPlanet(string planet)
        {
            panelCharacter.SetActive(true);

            // Xóa sự kiện click cũ để tránh lỗi
            btnChar1.onClick.RemoveAllListeners();
            btnChar2.onClick.RemoveAllListeners();

            // Lấy Text hiển thị trên nút
            TextMeshProUGUI txt1 = btnChar1.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI txt2 = btnChar2.GetComponentInChildren<TextMeshProUGUI>();

            // Gán prefab & tên theo hành tinh
            switch (planet)
            {
                case "TraiDat":
                    SetupButton(btnChar1, gohanPrefab, "Gohan", txt1);
                    SetupButton(btnChar2, yamchaPrefab, "Yamcha", txt2);
                    break;

                case "Namec":
                    SetupButton(btnChar1, ocTieuPrefab, "Ốc Tiêu", txt1);
                    SetupButton(btnChar2, kamiPrefab, "Kami", txt2);
                    break;

                case "Xayda":
                    SetupButton(btnChar1, radicPrefab, "Radic", txt1);
                    SetupButton(btnChar2, kakalotPrefab, "Kakalot", txt2);
                    break;
            }
        }

        // 🔹 Gán sự kiện click đúng chuẩn
        private void SetupButton(Button btn, GameObject prefab, string name, TextMeshProUGUI txt)
        {
            if (txt != null)
                txt.text = name;

            btn.onClick.AddListener(() => SelectCharacter(prefab));
        }

        // 🔹 Khi click chọn nhân vật
        public void SelectCharacter(GameObject prefab)
        {
            // Xóa nhân vật cũ
            if (currentCharacter != null)
                Destroy(currentCharacter);

            // Tạo nhân vật mới giữa màn hình
            currentCharacter = Instantiate(prefab, new Vector3(0, -2, 0), Quaternion.identity);
        }
    }
}
