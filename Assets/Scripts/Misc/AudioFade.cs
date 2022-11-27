using System.Collections;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    [SerializeField] private float modifier = 0.025f;
    [SerializeField] private float _finalVolume = 0.3f;  

    private AudioSource _music;

    void Start()
    {
        _music = GetComponent<AudioSource>();
        _music.volume = 0f;

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (_music.volume <= _finalVolume)
        {
            _music.volume += modifier * Time.deltaTime;

            yield return null;
        }

        _music.volume = _finalVolume;
    }
}