using System.Collections;
using UnityEngine;

public class DelayGameObject : MonoBehaviour
{
    [SerializeField] private float _counter = 5;
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        _gameObject.SetActive(false);

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        while (_counter > 0)
        {
            _counter -= Time.deltaTime;
            yield return null;
        }

        _gameObject.SetActive(true);
    }
}