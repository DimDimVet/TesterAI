using UnityEngine;
using Zenject;

namespace LogicAI
{
    public class MoveAI1 : MonoBehaviour
    {
        private MoveAI moveAI;

        [SerializeField] private Transform target;
        [SerializeField][Range(0, 100)] private float speedForward = 1f;
        [SerializeField][Range(0, 100)] private float speedTurn = 1f;
        private Quaternion targetRotation;
        private Rigidbody thisRigibody;

        private bool isStopClass = false, isRun = false;

        //private IMoveAIExecutor moveAI;
        //[Inject]
        //public void Init(IMoveAIExecutor _moveAI)
        //{
        //    moveAI = _moveAI;
        //}
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                moveAI = new MoveAI(this.gameObject);
                moveAI.SetSpeed(speedForward, speedTurn);
                moveAI.SetTarget(target);
                //moveAI.SetMoveAI(gameObject.GetHashCode(), speedTurn);
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
            gameObject.transform.rotation=moveAI.GetRotation();
            thisRigibody.velocity = moveAI.GetMove();
            //moveAI.SetTarget(gameObject.transform, transforms.position);

            //transform.rotation = moveAI.GetRotationTarget();
            //thisRigibody.velocity= moveAI.GetVelocityTarget()*speedForward;
        }

    }
}


