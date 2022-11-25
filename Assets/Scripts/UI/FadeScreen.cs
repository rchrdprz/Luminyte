using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image _fade;

    private float _counter;
    private bool _isActive;

    public event End OnEnd;
    public delegate void End();

    private void Start()
    {
        _counter = 1;
        _fade.color = new(_fade.color.r, _fade.color.g, _fade.color.b, _counter);

        StartCoroutine(FadeOut());
    }

    public void StartFadeIn() => StartCoroutine(FadeIn());

    private IEnumerator FadeOut()
    {
        while (_counter > 0)
        {
            _counter -= Time.deltaTime;
            _fade.color = new(_fade.color.r, _fade.color.g, _fade.color.b, _counter);

            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        while (_counter < 1)
        {
            _counter += Time.deltaTime;
            _fade.color = new(_fade.color.r, _fade.color.g, _fade.color.b, _counter);

            yield return null;
        }

        OnEnd?.Invoke();
    }
}