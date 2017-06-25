using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 4;
        public float radius = 50;

        private IEnumerator Start()
        {
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            for (int i = 0; i < 2; i++)
            {
                yield return null;
            }

            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

            float r = 10*multiplier;
            var cols = Physics.OverlapSphere(transform.position, radius);
            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
                    rigidbodies.Add(col.attachedRigidbody);
                }
            }
            foreach (var rb in rigidbodies)
            {
                rb.AddExplosionForce(explosionForce*multiplier, transform.position, radius, 1*multiplier, ForceMode.Impulse);
                
            }
        }
    }
}
