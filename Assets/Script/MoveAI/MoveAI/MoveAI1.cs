using UnityEngine;

public class MoveAI1 
{
    private GameObject gameObject;
    private Transform target;
    private float speedRotation;
    private float speedMove;
    //
    public Quaternion rotationGameObject;
    public Vector3 positionGameObject;
    public Quaternion rotationTarget;
    public Vector3 positionTarget, forward;
    //
    private Quaternion lookRotationTarget;
    private Vector3 direction;
    public MoveAI1(GameObject _gameObject)
    {
        gameObject = _gameObject;
        rotationGameObject = _gameObject.transform.rotation;
        positionGameObject = _gameObject.transform.position;
    }
    public void SetSpeed(float _speedMove = 1, float _speedRotation = 1)
    {
        speedMove = _speedMove;
        speedRotation = _speedRotation;
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
        rotationTarget = target.rotation;
        positionTarget = target.position;
    }
    private void UpDateTransform()
    {
        positionTarget = target.position;
        rotationTarget = target.rotation;
        positionGameObject = gameObject.transform.position;
        rotationGameObject = gameObject.transform.rotation;
        direction = positionTarget - positionGameObject;
    }
    public Quaternion GetRotation(bool isOnAxisY = true)
    {
        UpDateTransform();
        lookRotationTarget = Quaternion.LookRotation(direction);
        rotationTarget = Quaternion.Lerp(rotationGameObject,
                                        lookRotationTarget,
                                        speedRotation * Time.fixedDeltaTime);

        if (!isOnAxisY) { rotationTarget.x = 0; }
        return rotationTarget;
    }
    public Vector3 GetMove(bool isOnAxisY = true)
    {
        forward = gameObject.transform.forward;
        if (!isOnAxisY) { forward.y = 0f; }
        return forward * speedMove;
    }
    public void SetStopDistance(float stopDistance)
    {

    }
}
