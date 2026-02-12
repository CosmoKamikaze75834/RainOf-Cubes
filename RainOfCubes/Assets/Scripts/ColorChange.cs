using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public void ApllyColor(GameObject cube)
    {
        Renderer renderer = GetComponent<Renderer>();

        if(renderer != null)
        {
            renderer.material.color = Color.red;
        }
    }
}