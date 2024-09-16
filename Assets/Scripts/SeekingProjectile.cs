using UnityEngine;

public class SeekingProjectile : Projectile
{
    [Header("References")]
<<<<<<< HEAD
    public Transform Trans;
=======
    public Transform trans;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    private Vector3 targetPosition;
    protected override void OnSetup()
    {

    }
<<<<<<< HEAD
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
=======

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
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f

            }
            Destroy(gameObject);
        }
    }
}
