using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _circleCollider;

    private Vector3 _velocity = Vector3.zero;
    private bool _flipped;
    private bool _grounded;
    private bool _wannaJump;
    private float _moveDirection;

    private EventSystem _eventSystem;
    private Stats _stats;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _eventSystem = GetComponent<EventSystem>();

        _eventSystem.AddListener(MovementEvent.JUMP, Jump);
        _eventSystem.AddListener(MovementEvent.MOVING_LEFT_STARTED, OnMovingStarted);
        _eventSystem.AddListener(MovementEvent.MOVING_LEFT_STOPPED, OnMovingStarted);
        _eventSystem.AddListener(MovementEvent.MOVING_RIGHT_STARTED, OnMovingStarted);
        _eventSystem.AddListener(MovementEvent.MOVING_RIGHT_STOPPED, OnMovingStarted);
    }

    private void OnMovingStarted(EventData e)
    {
        switch (e.Type)
        {
            case MovementEvent.MOVING_LEFT_STARTED:
                _moveDirection = -1f;
                break;

            case MovementEvent.MOVING_RIGHT_STARTED:
                _moveDirection = 1f;
                break;
            
            case MovementEvent.MOVING_LEFT_STOPPED:
                if (_moveDirection < 0)
                    _moveDirection = 0f;
                break;
            
            case MovementEvent.MOVING_RIGHT_STOPPED:
                if (_moveDirection > 0)
                    _moveDirection = 0f;
                break;
        }
    }

    private void FixedUpdate()
    {
        Move(_moveDirection * Time.deltaTime * _stats.movementSpeed);

        bool wasGrounded = _grounded;
        _grounded = false;

        var contacts = new List<ContactPoint2D>();

        _circleCollider.GetContacts(contacts);

        if (contacts.Count > 0)
        {
            _grounded = true;
            if (!wasGrounded)
            {
                _eventSystem.Dispatch(MovementEvent.GROUNDED);
            }
        }

        if (_wannaJump && _grounded)
        {
            _wannaJump = false;
            _rigidbody.AddForce(new Vector2(0f, _stats.jumpForce));
        }
    }

    public void Move(float move)
    {
        if (_grounded)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody.velocity.y);
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity,
                _stats.movementSmoothing);

            if (move < 0 && !_flipped || (move > 0 && _flipped))
            {
                FlipByDirection();
            }
        }
    }

    public void Jump(EventData e)
    {
        _wannaJump = true;
    }


    private void FlipByDirection()
    {
        _flipped = !_flipped;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}