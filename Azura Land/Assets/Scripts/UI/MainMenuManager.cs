using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnContinueBTN()
    {
        SceneManager.LoadScene("QuantumGameScene");
    }

    public void OnNewPlayBTN()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnChangeAccBTN()
    {
        SceneManager.LoadScene("LoginUI");
    }
}
