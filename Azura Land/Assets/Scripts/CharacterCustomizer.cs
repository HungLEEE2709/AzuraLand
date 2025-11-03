using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject panelCharacter;
        public Button btnChar1;
        public Button btnChar2;
        public Button btnDoiToc;
        public Image imgCharacter;

        [Header("Sprites")]
        public Sprite gohanBody;
        public Sprite yamchaBody;
        public Sprite ocTieuBody;
        public Sprite kamiBody;
        public Sprite radicBody;
        public Sprite kakalotBody;

        [Header("Tóc")]
        public Sprite hair1;
        public Sprite hair2;

        private Sprite currentHair;
        private bool usingHair1 = true;
        private string currentPlanet = "";

        void Start()
        {
            panelCharacter.SetActive(true);
        }

        public void ShowCharactersForPlanet(string planet)
        {
            panelCharacter.SetActive(true);
            currentPlanet = planet;

            btnChar1.onClick.RemoveAllListeners();
            btnChar2.onClick.RemoveAllListeners();

            switch (planet)
            {
                case "TraiDat":
                    btnChar1.onClick.AddListener(() => SelectCharacter(gohanBody));
                    btnChar2.onClick.AddListener(() => SelectCharacter(yamchaBody));
                    break;
                case "Namec":
                    btnChar1.onClick.AddListener(() => SelectCharacter(ocTieuBody));
                    btnChar2.onClick.AddListener(() => SelectCharacter(kamiBody));
                    break;
                case "Xayda":
                    btnChar1.onClick.AddListener(() => SelectCharacter(radicBody));
                    btnChar2.onClick.AddListener(() => SelectCharacter(kakalotBody));
                    break;
            }
        }

        public void SelectCharacter(Sprite body)
        {
            imgCharacter.sprite = body;
            imgCharacter.color = Color.white;
            currentHair = hair1;
            usingHair1 = true;
        }

        public void DoiToc()
        {
            if (imgCharacter.sprite == null) return;

            usingHair1 = !usingHair1;
            currentHair = usingHair1 ? hair1 : hair2;
            imgCharacter.sprite = currentHair;
        }
    }
}
