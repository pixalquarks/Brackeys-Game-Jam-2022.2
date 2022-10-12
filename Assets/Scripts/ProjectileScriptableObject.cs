using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    [CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "Projectile/FireSweep")]
    public class ProjectileScriptableObject : ScriptableObject
    {
        public GameObject prefab;
        public float speed;
        public float damage = 1;
    }
}
