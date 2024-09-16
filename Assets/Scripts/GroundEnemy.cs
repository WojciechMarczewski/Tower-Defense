
using UnityEngine;
using UnityEngine.AI;
namespace Assets.Scripts
{
    public class GroundEnemy : Enemy
    {
        public static NavMeshPath Path;
        public float MoveSpeed = 22;
        private int currentCornerIndex = 0;
        private Vector3 currentCorner;
        private bool CurrentCornerIsFinal
        {
            get { return currentCornerIndex == (Path.corners.Length - 1); }
        }
        private void GetNextCorner()
        {
            currentCornerIndex += 1;
            currentCorner = Path.corners[currentCornerIndex];
        }
        protected override void Start()
        {
            base.Start();
            currentCorner = Path.corners[0];
        }
        private void Update()
        {
            if (currentCornerIndex != 0)
            {
                Trans.forward = (currentCorner - Trans.position).normalized;
            }
            Trans.position = Vector3.MoveTowards(Trans.position, currentCorner, MoveSpeed * Time.deltaTime);
            if (Trans.position == currentCorner)
            {
                if (CurrentCornerIsFinal)
                {
                    Leak();
                }
                else
                {
                    GetNextCorner();
                }
            }
        }
    }
}
