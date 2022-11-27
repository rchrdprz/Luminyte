using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FadeObjective : MonoBehaviour
{
    [SerializeField] private float _counter = 3;
    [SerializeField] private TextMeshProUGUI _text;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (_counter > 0)
        {
            _counter -= Time.deltaTime;

            if (_counter <= _image.color.a)
            _image.color = new(_image.color.r, _image.color.g, _image.color.b, _counter);
            _text.color = new(_text.color.r, _text.color.g, _text.color.b, _counter);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}