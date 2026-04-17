using Core.Damage.Components;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 1f;
    private Transform _playerPosition;

    [Header("Components")]
    [SerializeField] private DamageReceiver _damageReceiver;
    [SerializeField] private DamageDealer _damageDealer;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            _playerPosition = player.transform;
    }


    void FixedUpdate()
    {
        if (_playerPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition.position, _speed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _damageDealer?.TryDealDamage(collision.gameObject);
        }
    }
}
