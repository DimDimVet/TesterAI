using UnityEngine;
using Zenject;

namespace LogicAI
{
    public class MoveAI : MonoBehaviour
    {
        [SerializeField] private Transform transforms;
        [SerializeField][Range(0, 100)] private float speedForward = 1f;
        [SerializeField][Range(0, 100)] private float speedTurn = 1f;
        private Quaternion targetRotation;
        private Rigidbody thisRigibody;

        private bool isStopClass = false, isRun = false;

        private IMoveAIExecutor moveAI;
        [Inject]
        public void Init(IMoveAIExecutor _moveAI)
        {
            moveAI = _moveAI;
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                moveAI.SetMoveAI(gameObject.GetHashCode(), speedTurn);
                thisRigibody = gameObject.GetComponent<Rigidbody>();
                if (!(thisRigibody as Rigidbody))
                {
                    thisRigibody = gameObject.AddComponent<Rigidbody>();
                    isRun = true;
                }
                else { isRun = true; }
            }
        }

        void FixedUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            moveAI.SetTarget(gameObject.transform, transforms.position);

            transform.rotation = moveAI.GetRotationTarget();
            thisRigibody.velocity= moveAI.GetVelocityTarget()*speedForward;
        }
    }
}


