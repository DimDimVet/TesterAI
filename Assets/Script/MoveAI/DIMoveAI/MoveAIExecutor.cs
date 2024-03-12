using UnityEngine;

namespace LogicAI
{
    struct SettingsMoveAI
    {
        public int HashGameObject;
        public Quaternion CurrentGameObjectRotation;
        public float SpeedRotation;

        public Vector3 CurrentPositionGameObject;
    }
    public class MoveAIExecutor : IMoveAIExecutor
    {
        private Vector3 direction;
        private Quaternion rotationTarget, lookRotationTarget;
        private SettingsMoveAI settings;

        public void SetMoveAI(int hashGameobject, float speedRotation)
        {
            if (settings.HashGameObject == 0)
            {
                settings = new SettingsMoveAI()
                {
                    SpeedRotation=speedRotation,
                };
            }
        }
        public void SetTarget(Transform gameObject, Vector3 target)
        {
            direction = target - gameObject.position;
            settings.CurrentGameObjectRotation = gameObject.rotation;
        }
        public void CurrentTarget()
        {

        }
        public Quaternion GetRotationTarget()
        {
            lookRotationTarget = Quaternion.LookRotation(direction);
            rotationTarget = Quaternion.Lerp(settings.CurrentGameObjectRotation,
                                            lookRotationTarget,
                                            settings.SpeedRotation);
            return rotationTarget;
        }
        public Vector3 GetVelocityTarget()
        {
            return direction;
        }
    }
}

