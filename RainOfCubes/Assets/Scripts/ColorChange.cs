using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ApplyColor()
    {
        if(_renderer != null)
        {
            _renderer.material.color = Color.red;
        }
    }

    public void ResetColor()
    {
        if (_renderer != null)
        {
            _renderer.material.color = Color.white;
        }
    }
}