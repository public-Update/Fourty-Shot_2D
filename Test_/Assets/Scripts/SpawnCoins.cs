using UnityEngine;
using Photon.Pun;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] GameObject prefabCoin;
    [SerializeField] Transform[] PointForCoins;

    private void Start() {
        StartServer.onStart += Spawn;
    } 

    void Spawn() {
      for(int i = 0; i < PointForCoins.Length; i++) {
            PhotonNetwork.Instantiate(prefabCoin.name, PointForCoins[i].position, Quaternion.identity);
        }
    }

}
