using Assets.Scripts;
using UnityEngine;

public class FlyingEnemy : Enemy
{
<<<<<<< HEAD
    [Tooltip("Move speed in units per second.")]
    public float MoveSpeed;
=======
    [Tooltip("Prêdkoœæ w jednostkach na sekundê.")]
    public float moveSpeed;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    private Vector3 targetPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
<<<<<<< HEAD
        targetPosition = GroundEnemy.Path.corners[GroundEnemy.Path.corners.Length - 1];
        targetPosition.y = Trans.position.y;
=======
        targetPosition = GroundEnemy.path.corners[GroundEnemy.path.corners.Length - 1];
        targetPosition.y = trans.position.y;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        Trans.position = Vector3.MoveTowards(Trans.position, targetPosition, MoveSpeed * Time.deltaTime);
        if (Trans.position == targetPosition)
=======
        trans.position = Vector3.MoveTowards(trans.position, targetPosition, moveSpeed * Time.deltaTime);
        if (trans.position == targetPosition)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        {
            Leak();
        }
    }
}
