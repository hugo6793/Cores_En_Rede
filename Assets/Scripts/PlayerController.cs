using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private Renderer rend;

    private NetworkVariable<Color> playerColor =
        new NetworkVariable<Color>();

    private Color currentColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void SetRandomPosition()
    {
        transform.position = new Vector3(
            Random.Range(-5f, 5f),
            0,
            Random.Range(-5f, 5f)
        );
    }

    void AssignNewColor()
    {
        if (!IsServer) return;

        // 🔥 ESPERA ata que exista PlayerManager
        if (PlayerManager.Instance == null)
        {
            Debug.LogWarning("Esperando a PlayerManager...");
            Invoke(nameof(AssignNewColor), 0.1f);
            return;
        }

        if (currentColor != default)
            PlayerManager.Instance.ReleaseColor(currentColor);

        currentColor = PlayerManager.Instance.GetColor();
        playerColor.Value = currentColor;
    }

    void OnColorChanged(Color oldColor, Color newColor)
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        rend.material.color = newColor;
    }

    // Chamado dende UI
    public void RequestColorChange()
    {
        if (IsOwner)
            ChangeColorServerRpc();
    }

    [ServerRpc]
    void ChangeColorServerRpc()
    {
        AssignNewColor();
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            SetRandomPosition();
            AssignNewColor(); // ✔ só aquí
        }

        playerColor.OnValueChanged += OnColorChanged;
        OnColorChanged(Color.white, playerColor.Value);
    }
}