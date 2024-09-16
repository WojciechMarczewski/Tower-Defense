using Assets.Scripts;
using UnityEngine;

public class FiringTower : TargetingTower
{
<<<<<<< HEAD
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
    private Enemy targetedEnemy;
    private float lastFireTime = Mathf.NegativeInfinity;
    private bool isEnemyOutOfRange { get { return Vector3.Distance(Trans.position, targetedEnemy.Trans.position) > Range; } }
=======
    [Tooltip("Szybkie odwo³anie do g³ównego sk³adnika Transform wie¿y.")]
    public Transform trans;
    [Tooltip("Odwo³anie do sk³adnika Transform, w miejscu którego pocisk powinien byæ pocz¹tkowo umieszczony")]
    public Transform projectileSpawnPoint;
    [Tooltip("Odwo³anie do sk³adnika Transform, który powinien wskazywaæ na wroga.")]
    public Transform aimer;
    [Tooltip("Liczba sekund pomiêdzy kolejnymi wystrza³ami pocisków.")]
    public float fireInterval = .5f;
    [Tooltip("Odwo³anie do prefabrykatu pocisku, który powinien byæ wystrzeliwany.")]
    public Projectile projectilePrefab;
    [Tooltip("Obra¿enia zadawane przez ka¿dy pocisk.")]
    public float damage = 4;
    [Tooltip("Szybkoœæ lotu pocisków w jednostkach na sekundê.")]
    public float projectileSpeed = 60;
    [Tooltip("Czy wie¿a mo¿e atakowaæ wrogów lataj¹cych?")]
    public bool canAttackFlying = true;
    private Enemy targetedEnemy;
    private float lastFireTime = Mathf.NegativeInfinity;
    private bool isEnemyOutOfRange { get { return Vector3.Distance(trans.position, targetedEnemy.trans.position) > range; } }
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f


    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null)
        {
<<<<<<< HEAD
            if (!targetedEnemy.Alive || isEnemyOutOfRange)
=======
            if (!targetedEnemy.alive || isEnemyOutOfRange)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            {
                GetNextTarget();
            }
            else
            {
<<<<<<< HEAD
                if (CanAttackFlying || targetedEnemy is GroundEnemy)
                {

                    AimAtTarget();
                    if (Time.time > lastFireTime + FireInterval)
=======
                if (canAttackFlying || targetedEnemy is GroundEnemy)
                {

                    AimAtTarget();
                    if (Time.time > lastFireTime + fireInterval)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
                    {
                        Fire();
                    }
                }
            }
        }
<<<<<<< HEAD
        else if (Targeter.TargetsAreAvailable)
=======
        else if (targeter.TargetsAreAvailable)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        {
            GetNextTarget();
        }

    }
    private void AimAtTarget()
    {
<<<<<<< HEAD
        if (Aimer)
        {
            Vector3 to = targetedEnemy.Trans.position;
            to.y = 0;
            Vector3 from = Aimer.position;
            from.y = 0;
            Quaternion desiredRotation = Quaternion.LookRotation((to - from).normalized, Vector3.up);
            Aimer.rotation = Quaternion.Slerp(Aimer.rotation, desiredRotation, .08f);
=======
        if (aimer)
        {
            Vector3 to = targetedEnemy.trans.position;
            to.y = 0;
            Vector3 from = aimer.position;
            from.y = 0;
            Quaternion desiredRotation = Quaternion.LookRotation((to - from).normalized, Vector3.up);
            aimer.rotation = Quaternion.Slerp(aimer.rotation, desiredRotation, .08f);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }
    private void GetNextTarget()
    {
<<<<<<< HEAD
        targetedEnemy = Targeter.GetClosestEnemy(Trans.position);
=======
        targetedEnemy = targeter.GetClosestEnemy(trans.position);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    }
    private void Fire()
    {
        lastFireTime = Time.time;
<<<<<<< HEAD
        var proj = Instantiate<Projectile>(ProjectilePrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
        proj.Setup(Damage, ProjectileSpeed, targetedEnemy);
=======
        var proj = Instantiate<Projectile>(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        proj.Setup(damage, projectileSpeed, targetedEnemy);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    }
}
