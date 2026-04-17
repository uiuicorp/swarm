using System;
using UnityEngine;
using Core.Damage.Data;

namespace Core.Damage.Components
{
    public class DamageReceiver : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth;
        
        [Header("Defenses")]
        [SerializeField] private int _baseDefense = 0;
        [SerializeField] private float _fireResistance = 0f;    // 0 = normal, 0.5 = 50% resist, -0.5 = 50% weakness
        [SerializeField] private float _iceResistance = 0f;
        [SerializeField] private float _lightningResistance = 0f;
        [SerializeField] private float _holyResistance = 0f;
        [SerializeField] private float _darkResistance = 0f;
        
        [Header("Invincibility")]
        [SerializeField] private float _invincibilityDuration = 0.5f;
        private float _lastDamageTime = -999f;
        
        [Header("Death")]
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private bool _destroyOnDeath = true;
        
        // Events - be careful with many subscribers in VS-like games
        public event Action<DamageInfo> OnDamageTaken;
        public event Action<int, int> OnHealthChanged;  // current, max
        public event Action OnDeath;
        
        // Cached properties for performance
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsAlive => _currentHealth > 0;
        public bool IsInvincible => Time.time - _lastDamageTime < _invincibilityDuration;
        
        private void Awake()
        {
            _currentHealth = _maxHealth;
        }
        
        public bool TryTakeDamage(DamageInfo damage)
        {
            // Invincibility check
            if (IsInvincible)
                return false;
            
            // Calculate resistance multiplier
            float resistance = GetResistanceForType(damage.Type);
            int finalDamage = Mathf.Max(1, Mathf.RoundToInt(damage.Value * (1f - resistance) - _baseDefense));
            
            if (finalDamage <= 0)
                return false;
            
            // Apply damage
            _lastDamageTime = Time.time;
            _currentHealth = Mathf.Max(0, _currentHealth - finalDamage);
            
            // Create result for events
            var result = damage;
            result.Value = finalDamage;
            
            // Trigger events
            OnDamageTaken?.Invoke(result
                );
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            
            // Handle death
            if (_currentHealth <= 0)
            {
                Die();
            }
            
            return true;
        }
        
        private float GetResistanceForType(DamageType type)
        {
            switch (type)
            {
                case DamageType.Fire: return _fireResistance;
                case DamageType.Ice: return _iceResistance;
                case DamageType.Lightning: return _lightningResistance;
                case DamageType.Holy: return _holyResistance;
                case DamageType.Dark: return _darkResistance;
                default: return 0f;
            }
        }
        
        private void Die()
        {
            if (_deathEffect != null)
                Instantiate(_deathEffect, transform.position, Quaternion.identity);
            
            OnDeath?.Invoke();
            
            if (_destroyOnDeath)
                Destroy(gameObject);
        }
        
        public void Heal(int amount)
        {
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
        
        // For external forced damage (lava, traps)
        public void ForceDamage(int amount, DamageType type = DamageType.Pure)
        {
            TryTakeDamage(new DamageInfo(amount, type, null));
        }
    }
}