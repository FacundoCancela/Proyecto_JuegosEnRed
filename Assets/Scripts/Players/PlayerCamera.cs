using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 offset;  // Distancia de la c�mara al jugador
    private Transform playerTransform;

    // Este m�todo se llamar� para asignar el jugador al que seguir� la c�mara
    public void AssignPlayer(Transform player)
    {
        playerTransform = player;
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = playerTransform.position + offset;
            newPosition.z = transform.position.z;  // Mantener la Z original de la c�mara
            transform.position = newPosition;
        }
    }
}
