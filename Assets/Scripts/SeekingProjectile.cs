using UnityEngine;

public class SeekingProjectile : Projectile
{
    [Header("References")]
    public Transform Trans;
    private Vector3 targetPosition;
    protected override void OnSetup()
    {

    }
    void Update()
    {
        if (TargetEnemy != null)
        {
            targetPosition = TargetEnemy.ProjectileSeekPoint.position;
        }
        Trans.forward = (targetPosition - transform.position).normalized;
        Trans.position = Vector3.MoveTowards(Trans.position, targetPosition, Speed * Time.deltaTime);
        if (Trans.position == targetPosition)
        {
            if (TargetEnemy != null)
            {
                TargetEnemy.TakeDamage(Damage);

            }
            Destroy(gameObject);
        }
    }
}
