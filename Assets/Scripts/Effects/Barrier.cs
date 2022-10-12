using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Barrier : MonoBehaviour, ILeverMechanism
    {
        public Collider2D cameraBoundingBox;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Barrier nextBarrier;
        [SerializeField] private GameObject portalEffect;
        [SerializeField] private GameObject sprite;
        [SerializeField] private Vector3 movePosition;
        [SerializeField] private float moveDuration;
        [SerializeField] private bool open;
        [SerializeField] private bool nextLevel;
        private bool _hasOpened;

        public event EventHandler OnLevelWin;

        private GameObject _player;
        private GameObject _virtualCam;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _virtualCam = GameObject.FindWithTag("VirtualCam");
        }

        private void Start()
        {
            if (open)
            {
                Open();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player") && (_hasOpened || open))
            {
                if (nextLevel)
                {
                    OnLevelWin?.Invoke(this, EventArgs.Empty);
                    return;
                }
                nextBarrier.SpawnPlayer();
                _virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = nextBarrier.cameraBoundingBox;
            }
        }

        public void SpawnPlayer()
        {
            print("Spawn Player at: " + spawnPoint.position);
            _player.transform.position = spawnPoint.position;
        }

        public async void Open()
        {
            if (_hasOpened) return;
            AudioManager.i.PlayOnce(Sfx.GateOpen);
            await sprite.transform.DOLocalMove(movePosition, moveDuration).AsyncWaitForCompletion();
            _hasOpened = true;
            OnOpened();
            Destroy(sprite);
        }

        private void OnOpened()
        {
            portalEffect.SetActive(true);
        }
    }
}
