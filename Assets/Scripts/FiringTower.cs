using Assets.Scripts;
using UnityEngine;

public class FiringTower : TargetingTower
{
    [Tooltip("Szybkie odwo�anie do g��wnego sk�adnika Transform wie�y.")]
    public Transform trans;
    [Tooltip("Odwo�anie do sk�adnika Transform, w miejscu kt�rego pocisk powinien by� pocz�tkowo umieszczony")]
    public Transform projectileSpawnPoint;
    [Tooltip("Odwo�anie do sk�adnika Transform, kt�ry powinien wskazywa� na wroga.")]
    public Transform aimer;
    [Tooltip("Liczba sekund pomi�dzy kolejnymi wystrza�ami pocisk�w.")]
    public float fireInterval = .5f;
    [Tooltip("Odwo�anie do prefabrykatu pocisku, kt�ry powinien by� wystrzeliwany.")]
    public Projectile projectilePrefab;
    [Tooltip("Obra�enia zadawane przez ka�dy pocisk.")]
    public float damage = 4;
    [Tooltip("Szybko�� lotu pocisk�w w jednostkach na sekund�.")]
    public float projectileSpeed = 60;
    [Tooltip("Czy wie�a mo�e atakowa� wrog�w lataj�cych?")]
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
