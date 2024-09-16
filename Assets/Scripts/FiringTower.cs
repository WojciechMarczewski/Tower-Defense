using Assets.Scripts;
using UnityEngine;

public class FiringTower : TargetingTower
{

    [Tooltip("Quick reference to the main Transform component of the tower.")]
    public Transform Trans;
    [Tooltip("Reference to the Transform component where the projectile should initially be placed.")]
    public Transform ProjectileSpawnPoint;
    [Tooltip("Reference to the Transform component that should point at the enemy.")]
    public Transform Aimer;
    [Tooltip("Number of seconds between consecutive projectile shots.")]
    public float FireInterval = .5f;
    [Tooltip("Reference to the projectile prefab that should be fired.")]
    public Projectile ProjectilePrefab;
    [Tooltip("Damage dealt by each projectile.")]
    public float Damage = 4;
    [Tooltip("Projectile flight speed in units per second.")]
    public float ProjectileSpeed = 60;
    [Tooltip("Can the tower attack flying enemies?")]
    public bool CanAttackFlying = true;


    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null)
        {
            if (!targetedEnemy.Alive || isEnemyOutOfRange)

            {
                GetNextTarget();
            }
            else
            {
                if (CanAttackFlying || targetedEnemy is GroundEnemy)
                {

                    AimAtTarget();
                    if (Time.time > lastFireTime + FireInterval)
                    {
                        Fire();
                    }
                }
            }
        }
        else if (Targeter.TargetsAreAvailable)
        {
            GetNextTarget();
        }

    }
    private void AimAtTarget()
    {
        if (Aimer)
        {
            Vector3 to = targetedEnemy.Trans.position;
            to.y = 0;
            Vector3 from = Aimer.position;
            from.y = 0;
            Quaternion desiredRotation = Quaternion.LookRotation((to - from).normalized, Vector3.up);
            Aimer.rotation = Quaternion.Slerp(Aimer.rotation, desiredRotation, .08f);

        }
    }
    private void GetNextTarget()
    {
        targetedEnemy = Targeter.GetClosestEnemy(Trans.position);
    }
    private void Fire()
    {
        lastFireTime = Time.time;
        var proj = Instantiate<Projectile>(ProjectilePrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
        proj.Setup(Damage, ProjectileSpeed, targetedEnemy);
    }
}
