using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Camera mainCam;
    private float width;
    private float height;
    [SerializeField] private float moveSpeed = 5f;
    private float horizontalInput;
    private float verticalInput;
    void Start()
    {
        
        mainCam = Camera.main;
        height = mainCam.orthographicSize;
        width = height * mainCam.aspect;
    }
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
        ClampPosition();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
   
    
     void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 moveDelta = moveDirection.normalized * moveSpeed * Time.deltaTime;
        transform.position += moveDelta;
    }
    
    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -width + 0.5f, width - 0.5f);
        pos.y = Mathf.Clamp(pos.y, -height + 0.5f, height - 0.5f);
        transform.position = pos;
    }
}