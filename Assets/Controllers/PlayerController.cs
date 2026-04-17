using UnityEngine;
using UnityEngine.InputSystem;
using Core.Damage.Components;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float _speed = 5f;

    [Header("Components")]
    [SerializeField] private DamageReceiver _damageReceiver;

    private Rigidbody2D _rb;

    private Vector2 direction;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_damageReceiver == null)
            _damageReceiver = GetComponent<DamageReceiver>();

        // Safety check
        if (_damageReceiver == null)
            Debug.LogError("DamageReceiver component missing on Player!");

    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(direction.x, direction.y);
        _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime);
    }

    public void Heal(int amount)
    {
        _damageReceiver?.Heal(amount);
    }
}
