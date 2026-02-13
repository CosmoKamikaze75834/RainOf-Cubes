using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    private const float Delay = 0.4f;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _center;
    [SerializeField] private float _platformWidth;
    [SerializeField] private float _platformDepth;
    [SerializeField] private float _hight = 20;
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;
    private float _separator = 2;
    private WaitForSeconds _wait = new WaitForSeconds(Delay);

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => {Cube cube = Instantiate(_prefab);
        return cube; },
        actionOnGet: (cube) => PrepareObject(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(ChangePositionTime());
    }

    private void PrepareObject(Cube cube)
    {
        cube.FindPosition();
        cube.gameObject.SetActive(true);
    }

    public void ReceiveLocation(Vector3 position)
    {
        Cube cube = _pool.Get();
        cube.CubeDissapeared += ReturnCubePool;
        cube.transform.position = position;
    }

    private void ReturnCubePool(Cube cube)
    {
        cube.CubeDissapeared -= ReturnCubePool;
        _pool.Release(cube);
    }

    private IEnumerator ChangePositionTime()
    {
        bool isOpen = true;

        while (isOpen)
        {
            ChangeLocation();
            yield return _wait;
        }
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
            _center.position.y + _hight,
            _center.position.z + randomZ);

       ReceiveLocation(positionSpawn);
    }
}