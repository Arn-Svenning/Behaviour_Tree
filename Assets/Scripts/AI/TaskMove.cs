using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using Unity.VisualScripting;
public class TaskMove : BTNode
{
    private Rigidbody2D _rigidBody;
    private float _speed;

    public TaskMove(Rigidbody2D rigidBody, float speed)
    {
        this._rigidBody = rigidBody;
        this._speed = speed;
    }

    public override NodeState Evaluate()
    {
        // Get the current velocity
        Vector2 currentVelocity = _rigidBody.velocity;

        if (TaskFlip.BIsFlipped)
        {
            // Move to the left if flipped
            _rigidBody.velocity = new Vector2(-_speed, currentVelocity.y);
        }
        else
        {
            // Move to the right if not flipped
            _rigidBody.velocity = new Vector2(_speed, currentVelocity.y);
        }

        State = NodeState.RUNNING;
        return State;
    }
}


