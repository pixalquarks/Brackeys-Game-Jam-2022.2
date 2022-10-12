using System;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public enum PlayerMovementState
    {
        Normal,
        Slowed,
        Paused,
    }
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour, IDamageable
    {
        public float moveSpeed = 3.0f;
        public bool rotateSprite;
        public GameObject sprite;
        public PlayerMovementState movementState;

        public event EventHandler OnPlayerDeath;

        private Rigidbody2D _rb;
        public Vector2 Direction { get; private set; }
        public bool IsAlive { get; set; }
        
        private PlayerMovementState _prevState;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            IsAlive = true;
            movementState = PlayerMovementState.Normal;
        }

        private void Update()
        {
            if (PauseMenu.State == PauseMenu.States.Paused) return;
            Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (rotateSprite && Direction.magnitude > 0)
            {
                sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg);
            }
            Direction = Direction.normalized;
        }

        private void FixedUpdate()
        {
            
            var speed = movementState == PlayerMovementState.Normal ? moveSpeed : moveSpeed / 2;
            if (movementState == PlayerMovementState.Paused)
            {
                speed = 0f;
            }
            _rb.velocity = Direction * speed;
        }

        public void InvertMovementState(bool slowed)
        {
            _prevState =  slowed ? PlayerMovementState.Slowed : PlayerMovementState.Normal;
            if (movementState == PlayerMovementState.Paused) return;
            movementState = _prevState;
        }

        public void Paused(bool set)
        {
            var temp = movementState;
            movementState = set ? PlayerMovementState.Paused : _prevState;
            _prevState = temp;
        }
        

        
        public void Damage()
        {
            AudioManager.i.StopOnPlayerDeath();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject, 0.5f);
        }
    }
}
