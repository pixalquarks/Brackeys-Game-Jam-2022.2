using System;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Boundaries : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            print("trigger");
            if (col.gameObject.CompareTag("projectile"))
            {
                Destroy(col.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            print("Collision");
        }
    }
}
