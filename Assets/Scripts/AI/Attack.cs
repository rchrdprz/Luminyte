using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private PlayAudio _audio;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<PlayerMovement>(out _)) _gameOver.EndGame();
        _audio.Play();
    }
}
