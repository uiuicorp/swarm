using UnityEngine;

namespace Core.Damage.Data
{
    public enum DamageType
    {
        Physical,
        Fire,
        Ice,
        Lightning,
        Holy,
        Dark,
        Pure  // Ignores resistances
    }
    
    // Struct instead of class - zero GC allocation!
    public struct DamageInfo
    {
        public int Value;
        public DamageType Type;
        public GameObject Source;  // Who caused it (enemy or player)
        public Vector3 HitPoint;
        public bool IsCritical;
        public float KnockbackForce;
        
        public DamageInfo(int value, DamageType type, GameObject source)
        {
            Value = value;
            Type = type;
            Source = source;
            HitPoint = Vector3.zero;
            IsCritical = false;
            KnockbackForce = 0f;
        }
        
        public DamageInfo WithKnockback(float force)
        {
            KnockbackForce = force;
            return this;
        }
        
        public DamageInfo WithHitPoint(Vector3 point)
        {
            HitPoint = point;
            return this;
        }
    }
}