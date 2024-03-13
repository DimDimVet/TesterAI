using System;
using UnityEngine;

namespace LogicAI
{
    public struct SettingsMoveAI
    {
        public int HashGameObject;
        public Transform GameObjectTransform;
        public float SpeedRotation;
        public float SpeedMove;
        public Vector3 Target;

        //public Quaternion CurrentGameObjectRotation;
        //public Vector3 CurrentPositionGameObject;
    }
    public class MoveAIExecutor : IMoveAIExecutor
    {
        public Func<SettingsMoveAI> OnSettingsMoveAI { get { return onSettingsMoveAI; } set { onSettingsMoveAI = value; } }
        private Func<SettingsMoveAI> onSettingsMoveAI;

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
            settings.GameObjectTransform.rotation = gameObject.rotation;
        }
        public void CurrentTarget()
        {

        }
        public Quaternion GetRotationTarget()
        {
            lookRotationTarget = Quaternion.LookRotation(direction);
            rotationTarget = Quaternion.Lerp(settings.GameObjectTransform.rotation,
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

