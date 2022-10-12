using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public class Projectile : MonoBehaviour
    {
        private float _speed;

        public void Setup(ProjectileScriptableObject projectileScriptableObject)
        {
            _speed = projectileScriptableObject.speed;
        }

        private void Update()
        {
            transform.Translate(Vector2.up * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var damageable = col.GetComponent<IDamageable>();
            damageable?.Damage();
        }
    }
}
