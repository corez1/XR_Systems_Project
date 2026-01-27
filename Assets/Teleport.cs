using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    public Transform player;           // Player
    public Transform roomAnchor;                // Room
    public Transform outsideAnchor;            // Outside
    public InputActionReference teleportAction; // For VR Buttons

    private bool isOutside = false;             // For tracking player

    void Start()
    {
        player.position = roomAnchor.position;
        player.rotation = roomAnchor.rotation;

        teleportAction.action.Enable();
        teleportAction.action.performed += (ctx) =>
        {
            TeleportPosition();
        };
    }

    void TeleportPosition()
    {
        if (isOutside)
        {
            player.position = roomAnchor.position;
            player.rotation = roomAnchor.rotation;
            isOutside = false;
        }
        else
        {
            player.position = outsideAnchor.position;
            player.rotation = outsideAnchor.rotation;
            isOutside = true;
        }
    }
}
