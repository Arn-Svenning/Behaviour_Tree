using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

using UnityEngine;


public class CheckNearbyObstacles : BTNode
{
    private Transform _transform;
    private float _searchRadius;
    private int _obstacleLayer = 7;
    private bool sideCollisionDetected = false;

    public CheckNearbyObstacles(Transform transform, float searchRadius)
    {
        this._transform = transform;
        this._searchRadius = searchRadius;
    }

    public override NodeState Evaluate()
    {
        object obstacle = GetNodeData("Obstacle");

        if (!sideCollisionDetected && obstacle == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, _searchRadius);

            foreach (Collider2D collider in colliders)
            {
                // Check if the collider is on the correct layer
                if (collider.gameObject.layer == _obstacleLayer)
                {
                    // Calculate the relative position of the colliders
                    Vector3 relativePosition = collider.transform.position - _transform.position;

                    // Check if the collision is from the sides based on the relative position
                    if (Mathf.Abs(relativePosition.x) > Mathf.Abs(relativePosition.y))
                    {
                        
                        // Set or update the "Obstacle" data in your behavior tree
                        parent.parent.SetNodeData("Obstacle", collider.transform);

                        // Optionally clear the "Obstacle" data if needed
                        parent.parent.ClearNodeData("Obstacle");

                        // Set the flag to prevent further checks
                        sideCollisionDetected = true;

                        // Set the state to SUCCESS
                        State = NodeState.SUCCESS;
                        return State;
                    }
                }
            }

            // No suitable side collision found on the specified layer
            State = NodeState.FAILURE;
            return State;
        }

        // Reset the flag for the next evaluation
        sideCollisionDetected = false;

        // Continue with the regular logic
        State = NodeState.SUCCESS;
        return State;
    }
}

