using UnityEngine;
using Core.Damage.Components;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] private DamageDealer _damageDealer;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3 _shootDirection;

    void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    public void Initialize(Vector3 targetPosition)
    {
        _shootDirection = (targetPosition - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        transform.position += _shootDirection * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _damageDealer?.TryDealDamage(collision.gameObject);
        }
    }
}
