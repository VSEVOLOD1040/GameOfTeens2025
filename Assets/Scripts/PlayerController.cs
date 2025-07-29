using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public GameObject ObjectToRotate;

    private Rigidbody2D rb;
    private float direction = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleRotation();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleRotation()
    {
        float input = Input.GetAxis("Horizontal");

        if (input > 0.1f)
            direction = 1f;
        else if (input < -0.1f)
            direction = -1f;

        ObjectToRotate.transform.Rotate(0f, 0f, -rotationSpeed * direction * Time.deltaTime);
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector2 moveDirection = ObjectToRotate.transform.up * moveInput;

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
