using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Instance;

    public List<Color> availableColors = new List<Color>()
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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Color GetColor()
    {
        List<Color> freeColors = new List<Color>();

        foreach (Color c in availableColors)
        {
            if (!usedColors.Contains(c))
                freeColors.Add(c);
        }

        if (freeColors.Count == 0)
            return Color.white; // fallback

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