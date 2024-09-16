using Assets.Scripts;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [Tooltip("Move speed in units per second.")]
    public float MoveSpeed;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetPosition = GroundEnemy.Path.corners[GroundEnemy.Path.corners.Length - 1];
        targetPosition.y = Trans.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Trans.position = Vector3.MoveTowards(Trans.position, targetPosition, MoveSpeed * Time.deltaTime);
        if (Trans.position == targetPosition)
        {
            Leak();
        }
    }
}
