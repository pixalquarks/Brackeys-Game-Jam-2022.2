using System;
using Unity.VisualScripting;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Collector : MonoBehaviour
    {
        public GameObject keyPrefab;
        [HideInInspector] public int keyCount;

        private Movement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<Movement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (keyCount <= 0) return;
                keyCount--;
                var position = transform.position;
                var keyGO = Instantiate(keyPrefab, position, Quaternion.identity);
                keyGO.GetComponent<ICollectable>().Drop(position, _playerMovement.Direction);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("key"))
            {
                var collectable = col.gameObject.GetComponent<ICollectable>();
                if (!collectable.CanCollect) return;
                collectable.Collect();
                keyCount += 1;
            }
        }
    }
}