using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private int _maxTime = 5;
    private int _minTime = 2;

    public void DestroyAfterTime()
    {
        Destroy(gameObject, Random.Range(_minTime, _maxTime));
    }
}