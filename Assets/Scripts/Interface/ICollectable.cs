using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixalquarks.bgj2022_2
{
    public interface ICollectable
    {
        bool CanCollect { get; set; }
        void Collect();
        void Drop(Vector3 position, Vector3 direction);
    }
}
