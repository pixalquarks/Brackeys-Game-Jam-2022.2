using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Zoom : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCam;
        [SerializeField] private Camera enemyCamera;
        [SerializeField] private GameObject playerCurtain;
        [SerializeField] private float normalZoomOrtho;
        [SerializeField] private float maxZoomOrtho;

        private bool _isZoomedOut;
        private Movement _playerMovement;
        private bool _isPlayerDead;

        private void Awake()
        {
            _playerMovement = GameObject.FindWithTag("Player").GetComponent<Movement>();
        }

        private void OnEnable()
        {
            _playerMovement.OnPlayerDeath += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _playerMovement.OnPlayerDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath(object sender, EventArgs e)
        {
            _isPlayerDead = true;
        }

        private void Update()
        {
            if (_isPlayerDead) return;
            if (PauseMenu.State == PauseMenu.States.Paused) return;
            if (Input.GetKeyDown(KeyCode.X))
            {
                _isZoomedOut = !_isZoomedOut;
                
                virtualCam.m_Lens.OrthographicSize = _isZoomedOut ? maxZoomOrtho : normalZoomOrtho;
                enemyCamera.orthographicSize = _isZoomedOut ? maxZoomOrtho : normalZoomOrtho;
                _playerMovement.Paused(_isZoomedOut);
                playerCurtain.SetActive(!_isZoomedOut);
            }
        }
    }
}
