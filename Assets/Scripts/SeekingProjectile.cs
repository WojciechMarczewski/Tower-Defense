using UnityEngine;

public class SeekingProjectile : Projectile
{
    [Header("References")]
    public Transform trans;
    private Vector3 targetPosition;
    protected override void OnSetup()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy != null)
        {
            targetPosition = targetEnemy.projectileSeekPoint.position;
        }
        trans.forward = (targetPosition - transform.position).normalized;
        trans.position = Vector3.MoveTowards(trans.position, targetPosition, speed * Time.deltaTime);
        if (trans.position == targetPosition)
        {
            if (targetEnemy != null)
            {
                targetEnemy.TakeDamage(damage);

            }
            Destroy(gameObject);
        }
    }
}
