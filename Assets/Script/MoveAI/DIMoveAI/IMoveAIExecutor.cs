using UnityEngine;

namespace LogicAI
{
    public interface IMoveAIExecutor
    {
        void SetMoveAI(int hashGameobject, float speedRotation);
        Vector3 GetVelocityTarget();
        Quaternion GetRotationTarget();
        void SetTarget(Transform gameObject, Vector3 target);
        void CurrentTarget();
    }
}