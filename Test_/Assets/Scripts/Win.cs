using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class Win : MonoBehaviour
{
    PhotonView view;
    int DeceasedPlayers;
    [SerializeField] TMP_Text Text_Info; 
    [SerializeField] Image Hero;

    private void Start() {
        view = GetComponent<PhotonView>(); 
    }

    [PunRPC]
    public void Check() {
        view.RPC("WinOfCheck", RpcTarget.All);
    }

    [PunRPC]
    public void WinOfCheck() {
       foreach(Player player in SpawnPlayers.Players) {
         if(player.Died)  DeceasedPlayers++;
       }
       if(DeceasedPlayers == PhotonNetwork.CurrentRoom.PlayerCount - 1) {
            foreach(Player winner in SpawnPlayers.Players) {
                if(!winner.Died) {
                transform.GetChild(0).gameObject.SetActive(true);
                Text_Info.text = $"Winner: {winner._name}  Coins: {winner.Coins}";
                Hero.color = winner.GetComponent<SpriteRenderer>().color;
               }
            }
        }
        else DeceasedPlayers = 0;
    }
}
