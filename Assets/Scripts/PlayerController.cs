using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Renderer rend;
    private Color currentColor;

    void Start()
    {
        rend = GetComponent<Renderer>();

        transform.position = new Vector3(
            Random.Range(-5f, 5f),
            0,
            Random.Range(-5f, 5f)
        );

        AssignNewColor();
    }

    public void ChangeColor()
    {
        PlayerManager.Instance.ReleaseColor(currentColor);
        AssignNewColor();
    }

    void AssignNewColor()
    {
        currentColor = PlayerManager.Instance.GetAvailableColor();
        rend.material.color = currentColor;
    }
}