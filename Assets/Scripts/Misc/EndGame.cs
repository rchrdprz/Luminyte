using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _winIndex = 4;
    [SerializeField] private int _loseIndex = 3;
    [SerializeField] private bool _isWin = true;

    [Header("References")]
    [SerializeField] private FadeScreen _fadeScreen;

    private void Start()
    {
        _fadeScreen.OnEnd += FadeScreen_OnEnd;
        _fadeScreen.OnWin += FadeScreen_OnWin;
    }

    private void FadeScreen_OnWin() => WinScene();

    private void FadeScreen_OnEnd() => EndScene();

    public void End() => _fadeScreen.StartEnd(_isWin);

    private void WinScene() => SceneManager.LoadScene(_winIndex);

    private void EndScene() => SceneManager.LoadScene(_loseIndex);
}