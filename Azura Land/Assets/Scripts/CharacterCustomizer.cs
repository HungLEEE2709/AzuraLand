using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameScripts
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject panelCharacter;
        public Button btnChar1;
        public Button btnChar2;
        public Button btnConfirm;        
        public TMP_Text statusText;      

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

        private string selectedCharacter = null;
        private string selectedPlanet = null;

        // API Save Character
        public string apiSelect = "http://localhost:5000/api/character/select";
        public string nextScene = "GameScene";

        void Start()
        {
            panelCharacter.SetActive(false);

            if (btnConfirm != null)
            {
                btnConfirm.gameObject.SetActive(false);
                btnConfirm.onClick.AddListener(() =>
                {
                    StartCoroutine(SaveCharacterToDatabase());
                });
            }
        }

        public void ShowCharactersForPlanet(string planet)
        {
            selectedPlanet = planet; 

            panelCharacter.SetActive(true);

            btnChar1.onClick.RemoveAllListeners();
            btnChar2.onClick.RemoveAllListeners();

            TextMeshProUGUI txt1 = btnChar1.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI txt2 = btnChar2.GetComponentInChildren<TextMeshProUGUI>();

            switch (planet)
            {
                case "TraiDat":
                    SetupButton(btnChar1, gohanPrefab, "Gohan", txt1);
                    SetupButton(btnChar2, yamchaPrefab, "Yamcha", txt2);
                    break;

                case "Namec":
                    SetupButton(btnChar1, ocTieuPrefab, "OcTieu", txt1);
                    SetupButton(btnChar2, kamiPrefab, "Kami", txt2);
                    break;

                case "Xayda":
                    SetupButton(btnChar1, radicPrefab, "Radic", txt1);
                    SetupButton(btnChar2, kakalotPrefab, "Kakalot", txt2);
                    break;
            }
        }

        private void SetupButton(Button btn, GameObject prefab, string name, TextMeshProUGUI txt)
        {
            if (txt != null)
                txt.text = name;

            btn.onClick.AddListener(() => SelectCharacter(prefab, name));   
        }

        public void SelectCharacter(GameObject prefab, string name)
        {
            if (currentCharacter != null)
                Destroy(currentCharacter);

            currentCharacter = Instantiate(prefab, new Vector3(0, -2, 0), Quaternion.identity);

            selectedCharacter = name; 

            if (btnConfirm != null)
                btnConfirm.gameObject.SetActive(true);

            if (statusText != null)
                statusText.text = $"Đã chọn: {name}";
        }

        IEnumerator SaveCharacterToDatabase()
        {
            if (selectedPlanet == null || selectedCharacter == null)
            {
                if (statusText != null)
                    statusText.text = "Bạn chưa chọn đủ hành tinh & nhân vật!";
                yield break;
            }

            string idUser = PlayerPrefs.GetString("idUser", null);
            if (string.IsNullOrEmpty(idUser))
            {
                if (statusText != null)
                    statusText.text = "Lỗi: Không tìm thấy idUser!";
                yield break;
            }

            var data = new
            {
                idUser = idUser,
                Planet = selectedPlanet,
                CharacterName = selectedCharacter
            };

            string json = JsonUtility.ToJson(data);

            UnityWebRequest req = new UnityWebRequest(apiSelect, "POST");
            req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            if (statusText != null)
                statusText.text = "Đang lưu nhân vật...";

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                if (statusText != null)
                    statusText.text = "Lưu thành công!";

                PlayerPrefs.SetString($"CharacterName_{idUser}", selectedCharacter);
                PlayerPrefs.SetString($"Planet_{idUser}", selectedPlanet);
                PlayerPrefs.SetInt("CharacterChosen", 1);
                PlayerPrefs.Save();

                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                if (statusText != null)
                    statusText.text = "❌ Lỗi lưu nhân vật!";
            }
        }
    }
}
