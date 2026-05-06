using UnityEngine;
using Unity.Netcode;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        private NetworkManager m_NetworkManager;

        private bool isConnected;

        private void Awake()
        {
            m_NetworkManager = GetComponent<NetworkManager>();

            m_NetworkManager.OnClientConnectedCallback += OnClientConnected;
            m_NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;
        }

        private void OnDestroy()
        {
            if (m_NetworkManager != null)
            {
                m_NetworkManager.OnClientConnectedCallback -= OnClientConnected;
                m_NetworkManager.OnClientDisconnectCallback -= OnClientDisconnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            isConnected = true;
        }

        private void OnClientDisconnected(ulong clientId)
        {
            isConnected = false;
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            // 🔹 Estado inicial (antes de conectar)
            if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer && !m_NetworkManager.IsHost)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                if (!isConnected)
                {
                    GUILayout.Label("Conectando / esperando xogador...");
                }
                else
                {
                    SubmitNewPosition();
                }
            }

            GUILayout.EndArea();
        }

        private void StartButtons()
        {
            if (GUILayout.Button("Host")) m_NetworkManager.StartHost();
            if (GUILayout.Button("Client")) m_NetworkManager.StartClient();
            if (GUILayout.Button("Server")) m_NetworkManager.StartServer();
        }

        private void StatusLabels()
        {
            var mode = m_NetworkManager.IsHost
                ? "Host"
                : m_NetworkManager.IsServer
                    ? "Server"
                    : "Client";

            GUILayout.Label("Transport: " +
                m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);

            GUILayout.Label("Mode: " + mode);
        }

        private void SubmitNewPosition()
        {
            if (GUILayout.Button(
                m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
            {
                if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient)
                {
                    foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
                    {
                        var playerObj = m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid);

                        if (playerObj == null) continue;

                        var player = playerObj.GetComponent<HelloWorldPlayer>();

                        if (player != null)
                            player.Move();
                    }
                }
                else
                {
                    var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();

                    if (playerObject == null)
                    {
                        Debug.Log("Esperando spawn do player...");
                        return;
                    }

                    var player = playerObject.GetComponent<HelloWorldPlayer>();

                    if (player == null)
                    {
                        Debug.LogWarning("HelloWorldPlayer non atopado.");
                        return;
                    }

                    player.Move();
                }
            }
        }
    }
}