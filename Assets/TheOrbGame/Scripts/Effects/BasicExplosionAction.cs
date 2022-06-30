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
        public bool UseConfiguredLifetime = true;
        public float EffectScale = 1.5f;

        public virtual void Apply()
        {
            var explosion = Instantiate(ExplosionEffect, transform.position + EffectOffset, Quaternion.identity.Random());
            explosion.localScale = new Vector3(EffectScale, EffectScale, EffectScale);

           // if(UseConfiguredLifetime)
                Destroy(explosion.gameObject, 10);
        }
    }

    public static class Helpers
    {
        public static Quaternion Random(this Quaternion q)
        {
            return new Quaternion(UnityEngine.Random.Range(0, 360f),
                UnityEngine.Random.Range(0, 360f),
                UnityEngine.Random.Range(0, 360f),
                UnityEngine.Random.Range(0, 360f));
        }
    }
}
