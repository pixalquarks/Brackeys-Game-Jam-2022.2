using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Shooter : MonoBehaviour, ILeverMechanism
    {
        public bool loop = true;
        public float shootSpeed = 3f;

        public Transform shooterTransform;
        public ProjectileScriptableObject projectile;

        private bool _canShoot = true; 
        
        

        private float _shootTimer = 0f;

        private void Awake()
        {
            _shootTimer = shootSpeed;
        }
        
        private void Update()
        {
            if (!_canShoot) return;
            _shootTimer -= Time.deltaTime;
            if (_shootTimer <= 0f)
            {
                _shootTimer = shootSpeed;
                Shoot();
            }
        }

        private void Shoot()
        {
            // var rotation = Quaternion.Euler(shooterTransform.up);
            var projectileInstance = Instantiate(projectile.prefab, shooterTransform);
            projectileInstance.transform.parent = shooterTransform;
            projectileInstance.GetComponent<Projectile>().Setup(projectile);
        }

        public void Open()
        {
            _canShoot = false;
        }
    }
}
