using UnityEngine;

public class coinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSfx;
    [SerializeField] int pointsForCoinPickup = 100;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<gameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSfx,Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
