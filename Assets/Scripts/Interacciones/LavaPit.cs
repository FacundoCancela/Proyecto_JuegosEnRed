using Photon.Pun;
using UnityEngine;

public class LavaPit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseHealth();
        }
    }
}
