using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum PlayerEnum { Player, Player2 }
    public PlayerEnum playerType; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            if (playerType == PlayerEnum.Player && collision.gameObject.CompareTag("Player"))
            {
                Collect(collision);
            }
            else if (playerType == PlayerEnum.Player2 && collision.gameObject.CompareTag("Player2"))
            {
                Collect(collision);
            }
        }
    }

    void Collect(Collider2D collision)
    {
        PhotonView coinPhotonView = PhotonView.Get(this);
        if (coinPhotonView != null)
        {
            coinPhotonView.RPC("CollectCoin", RpcTarget.AllBuffered, coinPhotonView.ViewID);
        }
    }

    [PunRPC]
    void CollectCoin(int coinViewID)
    {
        GameManager.Instance.CollectCoin();

        PhotonView coinPhotonView = PhotonView.Find(coinViewID);
        if (coinPhotonView != null && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(coinPhotonView.gameObject);
        }
    }
}
