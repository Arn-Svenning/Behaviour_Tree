using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using UnityEditor.Rendering;

public class AIBT : BT.Tree
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private BoxCollider2D collider;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float AISpeed;
    [SerializeField] private float obstacleSearchRadius;
    [SerializeField] private float wallSearchRadius;
    [SerializeField] private float jumpForce;
    protected override BTNode SetupTree()
    {
        BTNode root = new BTSelector(new List<BTNode>
        {
            new BTSequence(new List<BTNode>()
            {

                new CheckNearbyObstacles(transform, obstacleSearchRadius),
                new TaskJump(rigidBody, jumpForce, AISpeed, groundLayer, collider)
            }),
            new BTSelector(new List<BTNode>
            {
                new BTSequence(new List<BTNode>()
                {
                   new CheckNearbyWalls(transform, wallSearchRadius),
                   new TaskFlip(transform)

                }),
                new TaskMove(rigidBody, AISpeed)
            })



        });   
            

        return root;
    }
}
