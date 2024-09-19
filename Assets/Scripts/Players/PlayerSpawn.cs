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
        int playerIndex = GetPlayerIndex();
        SpawnPlayer(playerIndex);
        pv.RPC("ChangeColor", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID, playerIndex);
    }

    private int GetPlayerIndex()
    {
        return PhotonNetwork.LocalPlayer.ActorNumber - 1;
    }

    [PunRPC]
    private void ChangeColor(int playerViewID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerViewID);

        if (targetPhotonView != null)
        {
            targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = (playerIndex == 0) ? Color.red : Color.blue;
        }
    }

    private void SpawnPlayer(int playerIndex)
    {
        int spawnIndex = Mathf.Clamp(playerIndex, 0, spawnPoints.Count - 1);

        player = PhotonNetwork.Instantiate(playerPrefab.name,
                        spawnPoints[spawnIndex].position,
                        Quaternion.identity);
    }
}
