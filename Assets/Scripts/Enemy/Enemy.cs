using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public enum MovementState
    {
        Moving,
        Following
    }
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        public float speed;
        public MovementState moveState;
        public GameObject sprite;
        public bool rotateSprite;
        [HideInInspector] public Transform followTransform;
        public bool IsAlive { get; set; }

        private Rigidbody2D _rb;
        private Vector2 _direction;

        private void Awake()
        {
            moveState = MovementState.Moving;
            _rb = GetComponent<Rigidbody2D>();
            IsAlive = true;
        }

        private void FixedUpdate()
        {
            if (moveState == MovementState.Following)
            {
                var position = _rb.position;
                _direction = ((Vector2) followTransform.position - position).normalized;
                if (rotateSprite && _direction.magnitude > 0)
                {
                    sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);
                }
                _rb.MovePosition(position + _direction * (speed * Time.fixedDeltaTime));
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<IDamageable>().Damage();
            }
        }

        public void Damage()
        {
            Destroy(gameObject);
        }
    }
}
