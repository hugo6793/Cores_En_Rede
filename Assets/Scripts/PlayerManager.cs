using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public static readonly Color[] availableColors = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.magenta,
        Color.cyan
    };

    private List<Color> usedColors = new List<Color>();

    void Awake()
    {
        Instance = this;
    }

    public Color GetAvailableColor()
    {
        List<Color> freeColors = new List<Color>();

        foreach (Color c in availableColors)
        {
            if (!usedColors.Contains(c))
                freeColors.Add(c);
        }

        if (freeColors.Count == 0)
            return Color.white;

        Color selected = freeColors[Random.Range(0, freeColors.Count)];
        usedColors.Add(selected);

        return selected;
    }

    public void ReleaseColor(Color color)
    {
        if (usedColors.Contains(color))
            usedColors.Remove(color);
    }
}