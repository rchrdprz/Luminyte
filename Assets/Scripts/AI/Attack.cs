using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private EndGame _gameOver;
    [SerializeField] private PlayAudio _audio;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<PlayerMovement>(out _)) _gameOver.End();
        _audio.Play();
    }
}
