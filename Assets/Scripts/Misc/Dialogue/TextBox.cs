using UnityEngine;

public class TextBox : MonoBehaviour
{
    public event TextOpen OnOpen;
    public delegate void TextOpen();

    public event TextClosed OnClose;
    public delegate void TextClosed();

    public void AsOpened() => OnOpen?.Invoke();

    public void AsClosed()
    {
        OnClose?.Invoke();
        gameObject.SetActive(false);
    }
}