using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
        createFunc: () => {Cube cube = Instantiate(_prefab);
        return cube.gameObject; },
        actionOnGet: (obj) => PrepareObject(obj),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void PrepareObject(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        gameObject.SetActive(true);
    }

    public void ReceiveLocation(Vector3 position)
    {
        GameObject cube = _pool.Get();
        cube.transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        _pool.Release(other.gameObject);
    }
}