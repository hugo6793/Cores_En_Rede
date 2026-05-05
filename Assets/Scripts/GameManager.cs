using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public int maxPlayers = 6;

    private int currentPlayers = 0;

    public void SpawnPlayer()
    {
        if (currentPlayers >= maxPlayers)
        {
            Debug.Log("Máximo de xogadores alcanzado");
            return;
        }

        Instantiate(playerPrefab);
        currentPlayers++;
    }
}
