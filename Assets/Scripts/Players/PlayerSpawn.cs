using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Vector3 cameraOffset;

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

        if (player.GetComponent<PhotonView>().IsMine)
        {
            SetupCamera(player.transform);
        }
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
        int prefabIndex = Mathf.Clamp(playerIndex, 0, playerPrefabs.Count - 1);

        player = PhotonNetwork.Instantiate(playerPrefabs[prefabIndex].name,
                        spawnPoints[spawnIndex].position,
                        Quaternion.identity);
    }

    private void SetupCamera(Transform playerTransform)
    {
        Camera.main.GetComponent<PlayerCamera>().AssignPlayer(playerTransform);
        Camera.main.GetComponent<PlayerCamera>().offset = cameraOffset;
    }
}
