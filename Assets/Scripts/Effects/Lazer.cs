using System;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Lazer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<IDamageable>().Damage();
            }
        }
    }
}
