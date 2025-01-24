using UnityEngine;

public class GunController : MonoBehaviour
{
    public float rotationSpeed = 180f; // Degrees per second
    public float bulletSpeed = 10f;
    public float shootCooldown = 0.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool isPlayerControlled = true;
    
    private float lastShotTime;
    private Transform targetTransform;
    
    void Start()
    {
        
        if (!isPlayerControlled)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            targetTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        }
    }
    
    void Update()
    {
        if (isPlayerControlled)
        {
            HandlePlayerInput();
        }
        else
        {
            HandleAIControl();
        }
    }
    
    void HandlePlayerInput()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        
        
        if (Input.GetKey(KeyCode.Space) && Time.time > lastShotTime + shootCooldown)
        {
            Shoot();
        }
    }
    
    void HandleAIControl()
    {
        if (targetTransform == null) return;
        
        
        Vector3 direction = targetTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
        
        
        if (Time.time > lastShotTime + shootCooldown)
        {
            float angleDifference = Mathf.Abs(Mathf.DeltaAngle(angle, transform.eulerAngles.z));
            if (angleDifference < 10f) 
            {
                Shoot();
            }
        }
    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        
        Vector2 shootDirection = transform.up;
        rb.velocity = shootDirection * bulletSpeed;
        
        lastShotTime = Time.time;
    }
}