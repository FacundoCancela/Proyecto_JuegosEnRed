using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 offset;  // Distancia de la cámara al jugador
    private Transform playerTransform;

    // Este método se llamará para asignar el jugador al que seguirá la cámara
    public void AssignPlayer(Transform player)
    {
        playerTransform = player;
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = playerTransform.position + offset;
            newPosition.z = transform.position.z;  // Mantener la Z original de la cámara
            transform.position = newPosition;
        }
    }
}
