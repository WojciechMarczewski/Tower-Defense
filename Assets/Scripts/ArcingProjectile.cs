using UnityEngine;

public class ArcingProjectile : Projectile
{

    public Transform Trans;
    [Tooltip("Layer mask used for detecting enemies affected by the explosion.")]
    public LayerMask EnemyLayerMask;
    [Tooltip("Explosion radius.")]
    public float ExplosionRadius = 25;
    [Tooltip("Curve going from 0 to 1 over 1 second. Defines the projectile's flight path.")]
    public AnimationCurve Curve;

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private float xzDistanceToTravel;
    private float spawnTime;
    private float FractionOfDistanceTraveled
    {
        get
        {
            float timeSinceSpawn = Time.time - spawnTime;
            float timeToReachDestination = xzDistanceToTravel / Speed;
            return timeSinceSpawn / timeToReachDestination;
        }
    }
    protected override void OnSetup()
    {

        initialPosition = Trans.position;
        targetPosition = TargetEnemy.Trans.position;
        targetPosition.y = 0;
        xzDistanceToTravel = Vector3.Distance(new Vector3(Trans.position.x, targetPosition.y, Trans.position.z), targetPosition);


        spawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = Trans.position;
        currentPosition.y = 0;
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
        currentPosition.y = Mathf.Lerp(initialPosition.y, targetPosition.y, Curve.Evaluate(FractionOfDistanceTraveled));
        Trans.position = currentPosition;
        if (currentPosition == targetPosition)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(Trans.position, ExplosionRadius, EnemyLayerMask.value);

        for (int i = 0; i < enemyColliders.Length; i++)
        {
            var enemy = enemyColliders[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                float distToEnemy = Vector3.Distance(Trans.position, enemy.Trans.position);
                float damageToDeal = Damage * (1 - Mathf.Clamp(distToEnemy / ExplosionRadius, 0f, 1f));

                enemy.TakeDamage(damageToDeal);
            }
        }
        Destroy(gameObject);
    }
}
