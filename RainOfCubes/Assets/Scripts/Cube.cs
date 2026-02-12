using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChange _color;
    [SerializeField] private Destroyer _destroyer;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Platform>(out _))
        {
            _color.ApllyColor(collision.gameObject);

            _destroyer.DestroyAfterTime();
        }
    }
}