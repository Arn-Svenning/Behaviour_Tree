using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using Unity.VisualScripting;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class TaskJump : BTNode
{
    private Rigidbody2D _rigidBody;

    private BoxCollider2D _collider;

    private LayerMask _groundLayer;

    private float _jumpForce;
    private float _speed;

    private bool _bIsGrounded;
    private bool _bIsJumping = false;

    public TaskJump(Rigidbody2D rigidBody,  float jumpForce, float speed, LayerMask layer, BoxCollider2D collider)
    {
        this._rigidBody = rigidBody;
        this._jumpForce = jumpForce;
        this._speed = speed;
        this._groundLayer = layer;
        this._collider = collider;

        _bIsGrounded = false;
    }
    public override NodeState Evaluate()
    {
        Vector2 raycastPosition = new Vector2(_collider.bounds.center.x, _collider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.down, 0.1f, _groundLayer);
        _bIsGrounded = hit.collider != null;

        if (_bIsGrounded)
        {
            if (!_bIsJumping)
            {
                _rigidBody.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
                _bIsJumping = true;
            }
            State = NodeState.SUCCESS;
        }
        else
        {
            _bIsJumping = false;
            State = NodeState.FAILURE;
        }

        return State;
    }
}
