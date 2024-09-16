
using UnityEngine;
using UnityEngine.AI;
namespace Assets.Scripts
{
    public class GroundEnemy : Enemy
    {
<<<<<<< HEAD
        public static NavMeshPath Path;
        public float MoveSpeed = 22;
=======
        public static NavMeshPath path;
        public float moveSpeed = 22;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        private int currentCornerIndex = 0;
        private Vector3 currentCorner;
        private bool CurrentCornerIsFinal
        {
<<<<<<< HEAD
            get { return currentCornerIndex == (Path.corners.Length - 1); }
=======
            get { return currentCornerIndex == (path.corners.Length - 1); }
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
        private void GetNextCorner()
        {
            currentCornerIndex += 1;
<<<<<<< HEAD
            currentCorner = Path.corners[currentCornerIndex];
=======
            currentCorner = path.corners[currentCornerIndex];
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
        protected override void Start()
        {
            base.Start();
<<<<<<< HEAD
            currentCorner = Path.corners[0];
=======
            currentCorner = path.corners[0];
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
        private void Update()
        {
            if (currentCornerIndex != 0)
            {
<<<<<<< HEAD
                Trans.forward = (currentCorner - Trans.position).normalized;
            }
            Trans.position = Vector3.MoveTowards(Trans.position, currentCorner, MoveSpeed * Time.deltaTime);
            if (Trans.position == currentCorner)
=======
                trans.forward = (currentCorner - trans.position).normalized;
            }
            trans.position = Vector3.MoveTowards(trans.position, currentCorner, moveSpeed * Time.deltaTime);
            if (trans.position == currentCorner)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
