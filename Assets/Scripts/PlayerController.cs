using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Renderer rend;

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.magenta,
        Color.cyan
    };

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Posición aleatoria
        transform.position = new Vector3(
            Random.Range(-5f, 5f),
            0,
            Random.Range(-5f, 5f)
        );

        AssignRandomColor();
    }

    public void ChangeColor()
    {
        AssignRandomColor();
    }

    void AssignRandomColor()
    {
        Color newColor = colors[Random.Range(0, colors.Length)];
        rend.material.color = newColor;
    }
}