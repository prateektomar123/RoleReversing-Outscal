using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float enemyMoveSpeed;
    [SerializeField] private BoxCollider2D boundary;
    [SerializeField]private int enemyHealth;
    Vector3 targetPosition;
    private Rigidbody2D rb;
    public bool isTarget;

    void Start()
    {
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!isTarget) return;
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (transform.position == targetPosition)
        {
            targetPosition = getRandomPatrolPoint();
        }
        var step=enemyMoveSpeed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private Vector3 getRandomPatrolPoint()
    {
        Bounds bounds = boundary.bounds;
        float xpos = Random.Range(bounds.min.x, bounds.max.x);
        float ypos = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(xpos, ypos, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyHealth--;
    }
}