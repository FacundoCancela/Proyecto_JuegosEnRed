using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuerda : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
            {
                Collect();
            }
        }
    }

    public void Collect()
    {
        PhotonView ropePhotonView = PhotonView.Get(this);
        if (ropePhotonView != null)
        {
            ropePhotonView.RPC("CollectRope", RpcTarget.AllBuffered, ropePhotonView.ViewID);
        }

    }

    [PunRPC]
    void CollectRope(int ropeViewID)
    {
        GameManager.Instance.GetRope();
        PhotonView ropePhotonView = PhotonView.Find(ropeViewID);
        if (ropePhotonView != null && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(ropePhotonView.gameObject);
        }
    }

}
