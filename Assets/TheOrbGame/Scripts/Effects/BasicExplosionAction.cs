using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TheOrbGame.Scripts.Effects
{
    public class BasicExplosionAction : MonoBehaviour, IDestroyAction
    {
        public Transform ExplosionEffect;
        public Vector3 EffectOffset = Vector3.zero;
        public float EffectLifetimeSeconds = 10;

        public virtual void Apply()
        {
            var explosion = Instantiate(ExplosionEffect, transform.position + EffectOffset, Quaternion.identity);
            Destroy(explosion.gameObject, EffectLifetimeSeconds);
        }
    }
}
