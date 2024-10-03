using Photon.Pun;
using UnityEngine;

public class WinDoor : MonoBehaviour
{
    public enum PlayerEnum { Player, Player2 }
    public PlayerEnum playerType; // Asignado en el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            // Condicional para comprobar qué jugador puede interactuar con la puerta
            if (playerType == PlayerEnum.Player && collision.gameObject.CompareTag("Player"))
            {
                OpenDoor();
            }
            else if (playerType == PlayerEnum.Player2 && collision.gameObject.CompareTag("Player2"))
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        GameManager.Instance.CheckWin();
    }
}
