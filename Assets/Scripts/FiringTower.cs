using Assets.Scripts;
using UnityEngine;

public class FiringTower : TargetingTower
{
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


    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null)
        {
            if (!targetedEnemy.alive || isEnemyOutOfRange)
            {
                GetNextTarget();
            }
            else
            {
                if (canAttackFlying || targetedEnemy is GroundEnemy)
                {

                    AimAtTarget();
                    if (Time.time > lastFireTime + fireInterval)
                    {
                        Fire();
                    }
                }
            }
        }
        else if (targeter.TargetsAreAvailable)
        {
            GetNextTarget();
        }

    }
    private void AimAtTarget()
    {
        if (aimer)
        {
            Vector3 to = targetedEnemy.trans.position;
            to.y = 0;
            Vector3 from = aimer.position;
            from.y = 0;
            Quaternion desiredRotation = Quaternion.LookRotation((to - from).normalized, Vector3.up);
            aimer.rotation = Quaternion.Slerp(aimer.rotation, desiredRotation, .08f);
        }
    }
    private void GetNextTarget()
    {
        targetedEnemy = targeter.GetClosestEnemy(trans.position);
    }
    private void Fire()
    {
        lastFireTime = Time.time;
        var proj = Instantiate<Projectile>(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        proj.Setup(damage, projectileSpeed, targetedEnemy);
    }
}
