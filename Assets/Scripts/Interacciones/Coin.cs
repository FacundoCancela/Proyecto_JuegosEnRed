using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player"))
        {
            PhotonView coinPhotonView = PhotonView.Get(this);
            if (coinPhotonView != null)
            {
                coinPhotonView.RPC("CollectCoin", RpcTarget.AllBuffered, coinPhotonView.ViewID);
            }
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
