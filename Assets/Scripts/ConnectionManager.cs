using Unity.Netcode;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public int maxPlayers = 6;

    void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
                       NetworkManager.ConnectionApprovalResponse response)
    {
        if (NetworkManager.Singleton.ConnectedClients.Count >= maxPlayers)
        {
            response.Approved = false;
            response.Reason = "Servidor cheo";
            return;
        }

        response.Approved = true;
        response.CreatePlayerObject = true;
    }
}
