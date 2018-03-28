using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFade : MonoBehaviour
{
    private ParticleSystem system;
    private ParticleSystem.Particle[] particles;

    public float duration = 2.0f;
    private float time;

    void Awake()
    {
        system = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        time += Time.deltaTime;

        particles = new ParticleSystem.Particle[system.particleCount];
        system.GetParticles(particles);

        if (particles != null)
        {
            for (int p = 0; p < particles.Length; p++)
            {
                Color color = particles[p].startColor;
                color.a = ((duration - time) / duration);

                particles[p].startColor = color;
            }

            system.SetParticles(particles, particles.Length);
        }
    }
}
