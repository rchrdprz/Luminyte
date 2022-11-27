using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _popupTime = 0.75f;
    private bool _isSkipped, _isOpened;
    private float _timer;

    [Header("References")]
    [SerializeField] private TextBox _textBox;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private Typewriter _typewriter;

    public event SkipText OnSkip;
    public delegate void SkipText();

    private void Start()
    {
        _textBox.OnOpen += TextBox_OnOpen;
        _textBox.OnClose += Textbox_OnClose;

        _typewriter.OnComplete += Typewriter_OnComplete;

        _buttons.SetActive(false);
        StartCoroutine(ShowTime());
    }

    private void Typewriter_OnComplete() => _isSkipped = true;

    private void Update()
    {   // pressing any key twice may skip dialgue completely //
        if (Input.anyKeyDown) Skip();
    }

    private void Skip()
    {   // allows the diague to be skipped //
        if (!_isSkipped)
        {
            OnSkip?.Invoke();
        }
        else
        {
            if (!_isOpened) return;
            _textBox.GetComponent<Animator>().SetTrigger("isClosed");
        }
    }

    private void TextBox_OnOpen() => _isOpened = true;

    private void Textbox_OnClose()
    {   // when text box closes, then the players neccesary components get enabled //
        _buttons.SetActive(true);
        _textBox.gameObject.SetActive(false);
        GetComponent<Dialogue>().enabled = false;
    }

    private IEnumerator ShowTime()
    {  // enables the dialogue box which makes its animation play //
        _timer = _popupTime;
        while (_timer >= 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0) _textBox.gameObject.SetActive(true);
            yield return null;
        }
    }
}