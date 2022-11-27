using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    private Image _image;
    private float _counter;

    public event End OnEnd;
    public delegate void End();

    public event Win OnWin;
    public delegate void Win();

    private void Start()
    {
        _image = GetComponent<Image>();

        _counter = 1;
        _image.color = new(_image.color.r, _image.color.g, _image.color.b, _counter);

        StartCoroutine(FadeIntro());
    }

    public void StartEnd(bool isWin) => StartCoroutine(EndFade(isWin));

    private IEnumerator FadeIntro()
    {
        while (_counter > 0)
        {
            _counter -= Time.deltaTime;
            _image.color = new(_image.color.r, _image.color.g, _image.color.b, _counter);

            yield return null;
        }
    }

    public IEnumerator EndFade(bool win)
    {
        while (_counter < 1)
        {
            _counter += Time.deltaTime;
            _image.color = new(_image.color.r, _image.color.g, _image.color.b, _counter);

            yield return null;
        }

        if (win)
        {
            OnWin?.Invoke();
        }
        else OnEnd?.Invoke();
    }
}