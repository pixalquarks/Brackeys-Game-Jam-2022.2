using System;
using DG.Tweening;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Key : MonoBehaviour, ICollectable
    {
        public bool CanCollect { get; set; }
        public float throwDistance;
        public float throwDuration;

        private void Awake()
        {
            CanCollect = true;
        }

        public void Collect()
        {
            if (!CanCollect) return;
            AudioManager.i.PlayOnce(Sfx.KeyPickup);
            print("Collected the key");
            Destroy(gameObject);
        }

        public async void Drop(Vector3 position, Vector3 direction)
        {
            print("Dropping");
            CanCollect = false;
            var endPos = transform.position - (direction.normalized * throwDistance);
            await gameObject.transform.DOMove(endPos, throwDuration).AsyncWaitForCompletion();
            CanCollect = true;
        }
    }
}
