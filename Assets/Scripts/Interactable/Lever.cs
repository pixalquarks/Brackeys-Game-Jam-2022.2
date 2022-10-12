using System;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Lever: MonoBehaviour
    {
        [SerializeField] private int keyCount;
        [SerializeField] private GameObject attachedObject;
        [SerializeField] private Sprite leverOn;
        [SerializeField] private Sprite leverOff;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private bool _inRange;
        private bool _opened;

        private void Update()
        {
            if (_opened) return;
            if (!_inRange || !Input.GetKeyDown(KeyCode.E)) return;
            if (keyCount == 0)
            {
                _opened = true;
                spriteRenderer.sprite = leverOff;
                AudioManager.i.PlayOnce(Sfx.LeverPull);
                GameObject.FindWithTag("Player").GetComponent<Collector>().keyCount--;
                attachedObject.GetComponent<ILeverMechanism>().Open();
            }
            else
            {
                keyCount--;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _inRange = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _inRange = false;
            }
        }
    }
}