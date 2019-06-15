using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEIAR {
    public class ParticlePaused : MonoBehaviour {

        private ParticleSystem ParticleObject;

        void Start() {

            ParticleObject = GetComponent<ParticleSystem>();

        }
               
        void Update() {

            if (Time.timeScale <= 0.0f)
            {
                ParticleObject.Simulate(Time.unscaledDeltaTime, true, false);
            }
        }
    }
}
