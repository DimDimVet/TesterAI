using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private MoveAI moveAI;

    [SerializeField][Range(0, 100)] private float speedForward = 100f;
    [SerializeField][Range(0, 100)] private float stopDistance = 2f;
    [SerializeField] private Transform target;
    private Rigidbody thisRigibody;

    private bool isStopClass = false, isRun = false;

    void Start()
    {
        SetClass();
    }
    private void SetClass()
    {
        isRun = true;
        thisRigibody = GetComponent<Rigidbody>();
        SettingMoveAI settingMoveAI = new SettingMoveAI
        {
            GameObject = this.gameObject,
            Rigidbody = thisRigibody,
            MoveSpeed = speedForward,
            StopDistance = stopDistance,
        };
        moveAI = new MoveAI(settingMoveAI);
    }

    void FixedUpdate()
    {
        if (isStopClass) { return; }
        if (!isRun) { SetClass(); }
        RunUpdate();
    }
    private void RunUpdate()
    {
        moveAI.UpdateMove(target.position);
    }
}
