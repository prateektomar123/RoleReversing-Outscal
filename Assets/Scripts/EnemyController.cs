using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float directionChangeInterval = 2f;
    public bool isTarget = false; // True when enemy should move randomly, false when it's the gun
    
    private Vector2 moveDirection;
    private float nextDirectionChange;
    private Camera mainCam;
    private float width;
    private float height;
    
    void Start()
    {
        mainCam = Camera.main;
        height = mainCam.orthographicSize;
        width = height * mainCam.aspect;
        SetNewDirection();
    }
    
    void Update()
    {
        if (!isTarget) return;
        
        if (Time.time >= nextDirectionChange)
        {
            SetNewDirection();
        }
        
        Move();
        ClampPosition();
    }
    
    void SetNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        nextDirectionChange = Time.time + directionChangeInterval;
    }
    
    void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    
    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -width + 0.5f, width - 0.5f);
        pos.y = Mathf.Clamp(pos.y, -height + 0.5f, height - 0.5f);
        transform.position = pos;
    }
}