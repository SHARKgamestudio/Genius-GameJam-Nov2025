using System;
using UnityEngine;

public enum MoveState
{
    STAY,
    EXIT
}

public class PlayerMover : MonoBehaviour
{
    public Action OnStayMovementEnd;
    public Action OnExitMovementEnd;

    protected float progression = 0;
    [SerializeField] protected float progressSpeed;
    protected Vector3 initialPos;
    protected Vector3 targetPos;
    float deltaPosition;
    MoveState moveState;

    public void SetPosition(Vector3 _position)
    {
        progression = 1;
        transform.position = _position;
        targetPos = _position;
    }
    public void SetTarget(Vector3 _position, MoveState _state)
    {
        progression = 0;
        initialPos = transform.position;
        targetPos = _position;
        moveState = _state;
        deltaPosition = (targetPos - initialPos).magnitude;
    }

    void Update()
    {
        if (progression < 1)
        {
            progression += progressSpeed * Time.deltaTime / deltaPosition;
            transform.position = Vector3.Lerp(initialPos, targetPos, progression);

            if (progression >= 1)
            {
                switch(moveState)
                {
                    case MoveState.STAY:
                        OnStayMovementEnd?.Invoke();

                        break;
                    case MoveState.EXIT:
                        OnExitMovementEnd?.Invoke();

                        break;
                }
            }
        }
    }
}
