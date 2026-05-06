using UnityEngine;
using Unity.Netcode;

public class UIManager : MonoBehaviour
{
    public void ChangeColor()
    {
        if (NetworkManager.Singleton.LocalClient.PlayerObject == null)
            return;

        PlayerController player =
            NetworkManager.Singleton.LocalClient.PlayerObject
            .GetComponent<PlayerController>();

        player.RequestColorChange();
    }
}