using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;
    private bool isPlayerBullet;
    private GameManager gameManager;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
        gameManager = FindObjectOfType<GameManager>();
        // Determine if this is a player bullet based on who shot it
        isPlayerBullet = GameObject.FindGameObjectWithTag("Gun")
            .GetComponent<GunController>().isPlayerControlled;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayerBullet && other.CompareTag("Enemy"))
        {
            gameManager.AddPlayerScore();
            // Respawn enemy at random position
            other.transform.position = gameManager.GetRandomPositionInCamera();
        }
        else if (!isPlayerBullet && other.CompareTag("Player"))
        {
            gameManager.AddEnemyScore();
            // Optional: Add visual feedback for player hit
            // You could add a flash effect or temporary invulnerability here
        }
        
        Destroy(gameObject);
    }
}