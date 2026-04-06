using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity = 5f;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(direction.x, direction.y);
        rb.MovePosition(rb.position + movement * velocity * Time.fixedDeltaTime);
    }
}
