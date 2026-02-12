using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _center;
    [SerializeField] private float _platformWidth;
    [SerializeField] private float _platformDepth;
    [SerializeField] private float _hight = 20;

    private float _replay = 0.8f;
    private float _separator = 2;

    private void Start()
    {
        InvokeRepeating(nameof(ChangeLocation), _replay, _replay);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(ChangeLocation));
    }

    private void ChangeLocation()
    {
        float minX = -_platformWidth / _separator;
        float maxX = +_platformWidth / _separator;

        float minZ = -_platformDepth / _separator;
        float maxZ = +_platformDepth / _separator;

        float randomX = Random.Range(minX, maxX);
        float randomZ  = Random.Range(minZ, maxZ);

        Vector3 positionSpawn = new Vector3(_center.position.x + randomX,
            _center.position.y + 5,
            _center.position.z + randomZ);

        _spawner.ReceiveLocation(positionSpawn);
    }
}