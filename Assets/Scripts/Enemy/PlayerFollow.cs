using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class PlayerFollow : MonoBehaviour
    {
        
        private Enemy _enemy;
        private GameObject _player;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _player = GameObject.FindWithTag("Player");
            _player.GetComponent<Movement>().OnPlayerDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath(object sender, EventArgs e)
        {
            _enemy.moveState = MovementState.Moving;
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            print("Enter collider");
            if (col.gameObject.CompareTag("Player"))
            {
                print("Player found");
                _enemy.moveState = MovementState.Following;
                _enemy.followTransform = col.gameObject.transform;
                AudioManager.i.PlayEnemyDetect();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _enemy.moveState = MovementState.Moving;
                _enemy.followTransform = null;
                AudioManager.i.StopEnemyDetect();
            }
        }
    }
}
