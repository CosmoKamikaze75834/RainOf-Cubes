using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
        if(collision.gameObject.TryGetComponent<Platform>(out _))
        {
            if(_isConflict == true)
            {
                return;
            }

            _color.ApplyColor();
            StartCoroutine(LyingPlatform());
            _isConflict = true;
        }
    }

    public void FindPosition()
    {
        _isConflict = false;

        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.rotation = Quaternion.identity;
        }
    }

    private IEnumerator LyingPlatform()
    {
        float delay = Random.Range(_minTime, _maxTime);
        WaitForSeconds _wait = new WaitForSeconds(delay);

        yield return _wait;
        CubeDissapeared?.Invoke(this);
    }
}