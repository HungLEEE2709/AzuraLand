using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{
    [Header("Buttons")]
    public UnityEngine.UI.Button continueButton;     
    public UnityEngine.UI.Button newGameButton;      
    public UnityEngine.UI.Button changeAccountButton; 

    [Header("Scene Names")]
    public string quantumGameScene = "QuantumGameScene";
    public string createPlayerScene = "Create Player";
    public string loginUIScene = "LoginUI";

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueClick);
        newGameButton.onClick.AddListener(OnNewGameClick);
        changeAccountButton.onClick.AddListener(OnChangeAccountClick);
    }

    private void OnContinueClick()
    {
        Debug.Log("Loading Quantum Game Scene...");
        SceneManager.LoadScene(quantumGameScene);
    }

    private void OnNewGameClick()
    {
        Debug.Log("Loading Create Player Scene...");
        SceneManager.LoadScene(createPlayerScene);
    }

    private void OnChangeAccountClick()
    {
        Debug.Log("Loading Login UI...");
        SceneManager.LoadScene(loginUIScene);
    }
}
