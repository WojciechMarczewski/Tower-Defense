using UnityEngine;

public class ArcingProjectile : Projectile
{
<<<<<<< HEAD
    public Transform Trans;
    [Tooltip("Layer mask used for detecting enemies affected by the explosion.")]
    public LayerMask EnemyLayerMask;
    [Tooltip("Explosion radius.")]
    public float ExplosionRadius = 25;
    [Tooltip("Curve going from 0 to 1 over 1 second. Defines the projectile's flight path.")]
    public AnimationCurve Curve;
=======
    public Transform trans;
    [Tooltip("Maska warstw u¿ywana przy wykrywaniu wrogów, na których wp³ynie wybuch.")]
    public LayerMask enemyLayerMask;
    [Tooltip("Promieñ wybuchu.")]
    public float explosionRadius = 25;
    [Tooltip("Krzywa przechodz¹ca od wartoœci 0 do 1 przez 1 sekundê. Definiuje krzyw¹ lotu pocisku.")]
    public AnimationCurve curve;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private float xzDistanceToTravel;
    private float spawnTime;
    private float FractionOfDistanceTraveled
    {
        get
        {
            float timeSinceSpawn = Time.time - spawnTime;
<<<<<<< HEAD
            float timeToReachDestination = xzDistanceToTravel / Speed;
=======
            float timeToReachDestination = xzDistanceToTravel / speed;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            return timeSinceSpawn / timeToReachDestination;
        }
    }
    protected override void OnSetup()
    {
<<<<<<< HEAD
        initialPosition = Trans.position;
        targetPosition = TargetEnemy.Trans.position;
        targetPosition.y = 0;
        xzDistanceToTravel = Vector3.Distance(new Vector3(Trans.position.x, targetPosition.y, Trans.position.z), targetPosition);
=======
        initialPosition = trans.position;
        targetPosition = targetEnemy.trans.position;
        targetPosition.y = 0;
        xzDistanceToTravel = Vector3.Distance(new Vector3(trans.position.x, targetPosition.y, trans.position.z), targetPosition);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        spawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        Vector3 currentPosition = Trans.position;
        currentPosition.y = 0;
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
        currentPosition.y = Mathf.Lerp(initialPosition.y, targetPosition.y, Curve.Evaluate(FractionOfDistanceTraveled));
        Trans.position = currentPosition;
=======
        Vector3 currentPosition = trans.position;
        currentPosition.y = 0;
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        currentPosition.y = Mathf.Lerp(initialPosition.y, targetPosition.y, curve.Evaluate(FractionOfDistanceTraveled));
        trans.position = currentPosition;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        if (currentPosition == targetPosition)
        {
            Explode();
        }
    }

    private void Explode()
    {
<<<<<<< HEAD
        Collider[] enemyColliders = Physics.OverlapSphere(Trans.position, ExplosionRadius, EnemyLayerMask.value);
=======
        Collider[] enemyColliders = Physics.OverlapSphere(trans.position, explosionRadius, enemyLayerMask.value);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            var enemy = enemyColliders[i].GetComponent<Enemy>();
            if (enemy != null)
            {
<<<<<<< HEAD
                float distToEnemy = Vector3.Distance(Trans.position, enemy.Trans.position);
                float damageToDeal = Damage * (1 - Mathf.Clamp(distToEnemy / ExplosionRadius, 0f, 1f));
=======
                float distToEnemy = Vector3.Distance(trans.position, enemy.trans.position);
                float damageToDeal = damage * (1 - Mathf.Clamp(distToEnemy / explosionRadius, 0f, 1f));
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
                enemy.TakeDamage(damageToDeal);
            }
        }
        Destroy(gameObject);
    }
}
