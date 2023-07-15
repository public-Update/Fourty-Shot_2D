using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] AudioClip getCoinSound;
    AudioSource Alayer;

    private void Start() {
        Alayer = FindObjectOfType<AudioSource>();        
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.GetComponent<Player>()) {
          other.GetComponent<Player>().UpdateCoinCounter();
          Alayer.PlayOneShot(getCoinSound);
          Destroy(gameObject);
       } 
    }

    private void Update() {
        transform.Rotate(0,2,0, Space.World);
    }

}
