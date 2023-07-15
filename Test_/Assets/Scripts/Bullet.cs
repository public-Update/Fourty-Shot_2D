using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int Damage;
    [Range(0.1f,15)] [SerializeField] float speedBullet;

    private void Update() {
        transform.Translate(Vector3.right * speedBullet, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()) {
            other.GetComponent<Player>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
