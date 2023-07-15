using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Color[] colorPlayers;
    [SerializeField] string[] namePlayers;
    [SerializeField] Transform[] pointSpawn;
    public static Player[] Players;
    PhotonView view;

    void Start() {
        view = GetComponent<PhotonView>();
        SpawnPlayer();
        StartServer.onStart += RPCStart;
    }

    [PunRPC]
    void RPCStart() {
      view.RPC("ReColorToPlayers", RpcTarget.All);
    }

    void SpawnPlayer() {
        PhotonNetwork.Instantiate(Player.name, pointSpawn[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, Quaternion.identity);
    }

    [PunRPC]
    void ReColorToPlayers() {
       Players = FindObjectsOfType<Player>();
       for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++) {
        Players[i].GetComponent<SpriteRenderer>().color = colorPlayers[i];
        Players[i]._name = namePlayers[i];
       }
    }
}
