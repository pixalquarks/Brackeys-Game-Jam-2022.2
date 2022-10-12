using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private Transform player;

        private void Awake()
        {
            AlignTransform();
        }

        public void AlignTransform()
        {
            transform.position = player.position;
        }
    }
}
