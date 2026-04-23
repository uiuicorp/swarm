using UnityEngine;
using Core.Damage.Components;

public class RangedAttacker : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private float _attackCooldown = 3f;

    private Vector3 target;
    private Transform firePoint;
    private float nextAttackTime;
    private DamageDealer damageDealer;
    
    void Start()
    {
        if (_projectilePrefab == null)
        {
            print("Projectile Prefab not found");
        }
        damageDealer = GetComponent<DamageDealer>();
        firePoint = gameObject.transform;
    }

    private void Update()
    {
        // Target closest Player
        target = Mechanics.FindClosestGameObjectWithTag(firePoint.position, "Player").transform.position;
        
        if (target != null)
        {
            TryRangedAttack();
        }
        
    }
    public void TryRangedAttack()
    {
        if (Time.time >= nextAttackTime && Vector3.Distance(transform.position, target) <= _attackRange)
        {
            PerformRangedAttack();
            nextAttackTime = Time.time + _attackCooldown;
        }
    }
    
    private void PerformRangedAttack()
    {
        GameObject projectile = CreateProjectile();
            

        // Initialize projectile movement
        projectile.GetComponent<ProjectileController>().Initialize(target);
    }

    private GameObject CreateProjectile()
    {
        return Instantiate(_projectilePrefab, firePoint.position, Quaternion.identity);
    }
}