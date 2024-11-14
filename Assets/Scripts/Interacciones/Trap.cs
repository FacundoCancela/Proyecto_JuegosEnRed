using Photon.Pun;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public enum PlayerEnum { Player, Player2, Both }
    public PlayerEnum playerType;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        Actor player = collision.gameObject.GetComponent<Actor>();
        if (pv != null && pv.IsMine)
        {
            if (playerType == PlayerEnum.Player && collision.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.LoseHealth();
                player.GetComponent<Actor>().Respawn();
            }
            else if (playerType == PlayerEnum.Player2 && collision.gameObject.CompareTag("Player2"))
            {
                GameManager.Instance.LoseHealth();
                player.GetComponent<Actor>().Respawn();
            }
            else if (playerType == PlayerEnum.Both && (collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Player")))
            {
                GameManager.Instance.LoseHealth();
                player.GetComponent<Actor>().Respawn();

            }

        }
    }
}
