using System;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Dimension : MonoBehaviour
    {
        public Camera enemyCamera;
        public float timer;
        public Movement playerMovement;

        private bool _currentActiveState;
        private float _currentTimer;
        private GameObject _player;
        private bool _isPlayerDead;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _player.GetComponent<Movement>().OnPlayerDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath(object sender, EventArgs e)
        {
            _isPlayerDead = true;
        }

        private void Update()
        {
            if (_isPlayerDead) return;
            if (PauseMenu.State == PauseMenu.States.Paused) return;
            _currentTimer -= Time.deltaTime;
            if (!Input.GetMouseButtonDown(1) || !(_currentTimer <= 0)) return;
            _currentActiveState = !_currentActiveState;
            enemyCamera.enabled = _currentActiveState;
            playerMovement.InvertMovementState(_currentActiveState);
            _currentTimer = timer;
        }
    }
}
