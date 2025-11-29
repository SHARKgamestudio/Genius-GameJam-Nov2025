using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Action OnMovementEnd;

    protected float progression = 0;
    [SerializeField] protected float progressSpeed = 0.5f;
    protected Vector3 initialPos;
    protected Vector3 targetPos;


    public void SetPosition(Vector3 _position)
    {
        progression = 1;
        transform.position = _position;
        targetPos = _position;
    }
    public void SetTarget(Vector3 _position)
    {
        progression = 0;
        initialPos = transform.position;
        targetPos = _position;
    }

    void Update()
    {
        if (progression < 1)
        {
            progression += progressSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos, targetPos, progression);

            if (progression >= 1)
            {
                OnMovementEnd.Invoke();
            }
        }
    }
}
