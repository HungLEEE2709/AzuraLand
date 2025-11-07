using UnityEngine;
using UnityEngine.UI;
using TMPro; // 👈 thêm dòng này

namespace GameScripts
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject panelCharacter;
        public Button btnChar1;
        public Button btnChar2;
        public Button btnDoiToc;

        [Header("Ảnh nhân vật")]
        public Image imgBody;
        public Image imgHair;

        [Header("Sprites thân - Trái Đất")]
        public Sprite gohanBody;
        public Sprite yamchaBody;

        [Header("Sprites thân - Namec")]
        public Sprite ocTieuBody;
        public Sprite kamiBody;

        [Header("Sprites thân - Xayda")]
        public Sprite radicBody;
        public Sprite kakalotBody;

        [Header("Sprites tóc")]
        public Sprite hair1;
        public Sprite hair2;

        private bool usingHair1 = true;
        private string currentPlanet = "";
        private Sprite currentBody;

        void Start()
        {
            if (btnDoiToc != null)
                btnDoiToc.onClick.AddListener(DoiToc);
        }

        public void ShowCharactersForPlanet(string planet)
        {
            currentPlanet = planet;
            panelCharacter.SetActive(true);

            btnChar1.onClick.RemoveAllListeners();
            btnChar2.onClick.RemoveAllListeners();

            // 👉 Dùng TextMeshProUGUI thay vì Text
            TextMeshProUGUI txt1 = btnChar1.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI txt2 = btnChar2.GetComponentInChildren<TextMeshProUGUI>();

            switch (planet)
            {
                case "TraiDat":
                    SetupButton(btnChar1, gohanBody, "Gohan", txt1);
                    SetupButton(btnChar2, yamchaBody, "Yamcha", txt2);
                    break;

                case "Namec":
                    SetupButton(btnChar1, ocTieuBody, "Ốc tiêu", txt1);
                    SetupButton(btnChar2, kamiBody, "Kami", txt2);
                    break;

                case "Xayda":
                    SetupButton(btnChar1, radicBody, "Radic", txt1);
                    SetupButton(btnChar2, kakalotBody, "Kakalot", txt2);
                    break;
            }
        }

        private void SetupButton(Button btn, Sprite body, string name, TextMeshProUGUI txt)
        {
            if (txt != null)
                txt.text = name; // đổi chữ trên nút

            btn.onClick.AddListener(() => SelectCharacter(body));
        }

        public void SelectCharacter(Sprite body)
        {
            currentBody = body;
            imgBody.sprite = currentBody;
            imgBody.color = Color.white;

            usingHair1 = true;
            imgHair.sprite = hair1;
            imgHair.color = Color.white;
        }

        public void DoiToc()
        {
            if (imgBody.sprite == null) return;
            usingHair1 = !usingHair1;
            imgHair.sprite = usingHair1 ? hair1 : hair2;
        }
    }
}
