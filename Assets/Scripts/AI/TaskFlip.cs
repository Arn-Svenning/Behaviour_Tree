using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class TaskFlip : BTNode
{
    private Transform _transform;

    public static bool BIsFlipped = false;

    private bool bHasFlipped = false;
      
    public TaskFlip(Transform transform)
    {
        _transform = transform;
    }
    public override NodeState Evaluate()
    {
        if(!bHasFlipped)
        {
            // Get the current scale of the sprite
            Vector2 currentScale = _transform.localScale;

            // Flip the sprite horizontally by negating the X scale
            currentScale.x = -currentScale.x;

            // Apply the new scale to the sprite
            _transform.localScale = currentScale;

            bHasFlipped = true;
            if (CheckNearbyWalls.BIsFromRight)
            {

                BIsFlipped = false;
                _transform.position += new Vector3(0.2f, 0, 0);
                State = NodeState.SUCCESS;
                return State;
            }
            else
            {

                BIsFlipped = true;
                _transform.position -= new Vector3(0.2f, 0, 0);
                State = NodeState.SUCCESS;
                return State;
            }
        }
        else
        {
            State = NodeState.FAILURE;
            bHasFlipped = false;
            return State;
        }
        
        
    }
}
