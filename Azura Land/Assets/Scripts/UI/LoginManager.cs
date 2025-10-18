using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement; // *** THÊM THƯ VIỆN NÀY ***

// --- Dữ liệu gửi đi (Payload) ---
[System.Serializable]
public class AuthPayload
{
    public string username;
    public string password;
}

public class LoginManager : MonoBehaviour
{
    // ... (các biến khác giữ nguyên)
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;
    public string baseUrl = "http://localhost:5000/api/users";

    // Đặt tên Scene Register của bạn vào đây.
    public string RegisterUI = "RegisterUI"; // *** TÊN SCENE REGISTER ***

    // Gán vào nút REGISTER
    public void OnRegister()
    {

        SceneManager.LoadScene(RegisterUI);
    }

    // Gán vào nút LOGIN
    public void OnLogin() { StartCoroutine(LoginCoroutine()); }
    IEnumerator LoginCoroutine()
    {
        // ... (logic đăng nhập giữ nguyên)
        AuthPayload data = new AuthPayload
        {
            username = usernameInput.text,
            password = passwordInput.text
        };
        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest req = new UnityWebRequest(baseUrl + "/login", "POST"))
        {
            byte[] body = System.Text.Encoding.UTF8.GetBytes(json);
            req.uploadHandler = new UploadHandlerRaw(body);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                string res = req.downloadHandler.text;
                string token = ExtractToken(res);

                if (!string.IsNullOrEmpty(token))
                {
                    PlayerPrefs.SetString("jwt_token", token);
                    statusText.text = "Đăng nhập thành công! ✅";
                    // **TODO:** Thêm logic chuyển cảnh (Scene) tại đây
                }
                else statusText.text = "Không nhận được token từ server.";
            }
            else statusText.text = "Lỗi Đăng nhập: " + req.downloadHandler.text;
        }
    }

    string ExtractToken(string json)
    {
        int idx = json.IndexOf("\"token\":\"");
        if (idx == -1) return null;
        int start = idx + 9;
        int end = json.IndexOf("\"", start);
        if (end == -1) return null;
        return json.Substring(start, end - start);
    }
}