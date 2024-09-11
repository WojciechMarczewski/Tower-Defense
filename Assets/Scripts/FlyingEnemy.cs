using Assets.Scripts;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [Tooltip("Prêdkoœæ w jednostkach na sekundê.")]
    public float moveSpeed;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetPosition = GroundEnemy.path.corners[GroundEnemy.path.corners.Length - 1];
        targetPosition.y = trans.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        trans.position = Vector3.MoveTowards(trans.position, targetPosition, moveSpeed * Time.deltaTime);
        if (trans.position == targetPosition)
        {
            Leak();
        }
    }
}
