using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> CubeDissapeared;

    [SerializeField] private ColorChange _color;

    private Rigidbody _rigidbody;

    private int _maxTime = 5;
    private int _minTime = 2;

    private bool _isConflict = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isConflict == true)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<Platform>(out _))
        {
            _color.ApplyColor();
            StartCoroutine(LyingPlatform());
            _isConflict = true;
        }
    }

    public void ResetPosition()
    {
        _isConflict = false;

        _color.ResetColor();

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
    }

    private IEnumerator LyingPlatform()
    {
        float delay = Random.Range(_minTime, _maxTime);
        WaitForSeconds _wait = new WaitForSeconds(delay);

        yield return _wait;
        CubeDissapeared?.Invoke(this);
    }
}