using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private int _index = 3;
    [SerializeField] private FadeScreen _background;

    private void Start() => _background.OnEnd += Background_OnEnd;

    private void Background_OnEnd() => LoadScene();

    public void EndGame() => _background.StartFadeIn();

    private void LoadScene() => SceneManager.LoadScene(_index);
}
