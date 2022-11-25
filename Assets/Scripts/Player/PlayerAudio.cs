using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _moveOne;
    [SerializeField] private AudioSource _moveTwo;
    [SerializeField] private AudioSource _moveThree;

    public void PlayStepOne()
    {
        _moveOne.Stop();
        _moveOne.Play();
    }

    public void PlayStepThree()
    {
        _moveTwo.Stop();
        _moveTwo.Play();
    }

    public void PlayStepTwo()
    {
        _moveThree.Stop();
        _moveThree.Play();
    }
}