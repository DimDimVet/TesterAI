using UnityEngine;

public struct SettingMoveAI
{
    public GameObject GameObject;
    public Rigidbody Rigidbody;
    public float MoveSpeed;
    public float RotationSpeed;
    public float StopDistance;
}

public class MoveAI
{
    private SettingMoveAI settingMoveAI;
    private Vector3 moveDirection, targetPosition;
    private float distance;
    private Quaternion deltaRotation, directionRotation;
    //
    private bool isTriggerRunMove=true;
    //
    public MoveAI(SettingMoveAI _settingMoveAI)
    {
        settingMoveAI = _settingMoveAI;
        if (settingMoveAI.StopDistance == 0) { settingMoveAI.StopDistance = 1f; }
        if (settingMoveAI.MoveSpeed == 0) { settingMoveAI.MoveSpeed = 1f; }
        if (settingMoveAI.RotationSpeed == 0) { settingMoveAI.RotationSpeed = 1f; }
    }
    public void UpdateMove(Vector3 target, bool isOffAxisY = true)
    {
        targetPosition = target;
        MoveInTargetLine(isOffAxisY);
    }
    private Vector3 MoveDirection()
    {
        return targetPosition - settingMoveAI.GameObject.transform.position;
    }
    private Quaternion GetRotation(Vector3 moveDirection, bool isOffAxisY)
    {
        if (isOffAxisY) { moveDirection.y = 0; }

        deltaRotation = Quaternion.LookRotation(moveDirection);
        directionRotation = Quaternion.Lerp(settingMoveAI.GameObject.transform.rotation,
                                            deltaRotation,
                                            settingMoveAI.RotationSpeed);
        return directionRotation;
    }
    private void MoveInTargetLine(bool isOffAxisY)
    {
        if (isOffAxisY) { distance = Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z); }
        else { distance = Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z) + Mathf.Abs(moveDirection.y); }

        moveDirection = MoveDirection();

        if (distance <= settingMoveAI.StopDistance)
        {
            StopMove(moveDirection, isOffAxisY);
        }
        else
        {
            RunMove(moveDirection, isOffAxisY);
        }

        if(distance > settingMoveAI.StopDistance * 1.1f) { isTriggerRunMove=true; }
    }
    private void StopMove(Vector3 moveDirection,bool isOffAxisY)
    {
        if (isTriggerRunMove)
        {
            isTriggerRunMove = !isTriggerRunMove;
            settingMoveAI.Rigidbody.velocity = Vector3.zero;
            settingMoveAI.Rigidbody.MoveRotation(GetRotation(moveDirection, isOffAxisY));
        }
    }
    private void RunMove(Vector3 moveDirection, bool isOffAxisY)
    {
        if (isTriggerRunMove)
        {
            settingMoveAI.Rigidbody.velocity = settingMoveAI.Rigidbody.transform.forward * settingMoveAI.MoveSpeed;
            settingMoveAI.Rigidbody.MoveRotation(GetRotation(moveDirection, isOffAxisY));
        }
    }
}
