using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Chơi tiếp (Continue)
    public void OnContinueBTN()
    {
        // Load scene game
        SceneManager.LoadScene("GameScene");
    }

    // Chơi mới (New Play)
    public void OnNewPlayBTN()
    {
        // Load scene game
        SceneManager.LoadScene("GameScene");
    }

    // Đổi tài khoản (Change Account)
    public void OnChangeAccBTN()
    {
        SceneManager.LoadScene("LoginUI");
    }
}
