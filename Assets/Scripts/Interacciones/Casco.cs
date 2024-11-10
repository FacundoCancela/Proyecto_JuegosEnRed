using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casco : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
            {
                Collect(collision.gameObject);
            }
        }
    }

    public void Collect(GameObject player)
    {
        Control control = player.GetComponent<Control>();
        control.ActivarCasco();

        PhotonView cascoPhotonView = PhotonView.Get(this);
        if (cascoPhotonView != null)
        {
            cascoPhotonView.RPC("CollectCasco", RpcTarget.AllBuffered, cascoPhotonView.ViewID);
        }

    }

    [PunRPC]
    void CollectCasco(int cascoViewID)
    {
        PhotonView cascoPhotonView = PhotonView.Find(cascoViewID);
        if (cascoPhotonView != null && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(cascoPhotonView.gameObject);
        }
    }

}
