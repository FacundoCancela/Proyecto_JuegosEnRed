using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<Transform> spawnPoints;

    private GameObject player;
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        int playerIndex = PhotonNetwork.PlayerList.Length;
        SpawnPlayer(playerIndex);
        pv.RPC("ChangeColor", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID, playerIndex);
    }

    [PunRPC]
    private void ChangeColor(int playerViewID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerViewID);

        if (targetPhotonView != null)
        {
            targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = (playerIndex == 1) ? Color.red : Color.blue;
        }
    }

    private void SpawnPlayer(int playerIndex)
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name,
                        spawnPoints[playerIndex - 1].position,
                        Quaternion.identity);
    }


}
