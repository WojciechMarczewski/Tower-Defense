using UnityEngine;

public class ArcingProjectile : Projectile
{
    public Transform trans;
    [Tooltip("Maska warstw u¿ywana przy wykrywaniu wrogów, na których wp³ynie wybuch.")]
    public LayerMask enemyLayerMask;
    [Tooltip("Promieñ wybuchu.")]
    public float explosionRadius = 25;
    [Tooltip("Krzywa przechodz¹ca od wartoœci 0 do 1 przez 1 sekundê. Definiuje krzyw¹ lotu pocisku.")]
    public AnimationCurve curve;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private float xzDistanceToTravel;
    private float spawnTime;
    private float FractionOfDistanceTraveled
    {
        get
        {
            float timeSinceSpawn = Time.time - spawnTime;
            float timeToReachDestination = xzDistanceToTravel / speed;
            return timeSinceSpawn / timeToReachDestination;
        }
    }
    protected override void OnSetup()
    {
        initialPosition = trans.position;
        targetPosition = targetEnemy.trans.position;
        targetPosition.y = 0;
        xzDistanceToTravel = Vector3.Distance(new Vector3(trans.position.x, targetPosition.y, trans.position.z), targetPosition);
        spawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = trans.position;
        currentPosition.y = 0;
        currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        currentPosition.y = Mathf.Lerp(initialPosition.y, targetPosition.y, curve.Evaluate(FractionOfDistanceTraveled));
        trans.position = currentPosition;
        if (currentPosition == targetPosition)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(trans.position, explosionRadius, enemyLayerMask.value);
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            var enemy = enemyColliders[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                float distToEnemy = Vector3.Distance(trans.position, enemy.trans.position);
                float damageToDeal = damage * (1 - Mathf.Clamp(distToEnemy / explosionRadius, 0f, 1f));
                enemy.TakeDamage(damageToDeal);
            }
        }
        Destroy(gameObject);
    }
}
