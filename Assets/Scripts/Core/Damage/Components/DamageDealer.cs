using UnityEngine;
using Core.Damage.Data;

namespace Core.Damage.Components
{
    // Enemies OR projectiles
    public class DamageDealer : MonoBehaviour
    {
        [Header("Damage Settings")]
        [SerializeField] private int _damage = 10;
        [SerializeField] private DamageType _damageType = DamageType.Physical;
        [SerializeField] private float _knockbackForce = 5f;
        [SerializeField] private bool _destroyOnHit = true;  // For projectiles
        
        [Header("Cooldown (for enemies)")]
        [SerializeField] private float _damageCooldown = 0.5f;
        private float _lastDamageTime;
        
        // Cache for performance
        private Collider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        
        // Called by OnCollisionEnter or OnTriggerEnter
        public bool TryDealDamage(GameObject target)
        {
            // Cooldown check
            if (Time.time - _lastDamageTime < _damageCooldown)
                return false;
            
            // Get receiver from target
            DamageReceiver receiver = target.GetComponent<DamageReceiver>();
            if (receiver == null)
                return false;
            
            // Deal damage
            DamageInfo damage = new DamageInfo(_damage, _damageType, gameObject)
                .WithKnockback(_knockbackForce);
            
            bool damaged = receiver.TryTakeDamage(damage);
            
            if (damaged)
            {
                _lastDamageTime = Time.time;
                
                if (_destroyOnHit)
                    Destroy(gameObject);
            }
            
            return damaged;
        }
        
        // Optional: Override damage for specific enemies (bosses, buffed enemies)
        public void SetDamage(int newDamage) => _damage = newDamage;
        public void SetDamageType(DamageType newType) => _damageType = newType;
    }
}