using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    public void SetStopParticle()
    {
        particle.Stop();
    }

    public void SetSpeedParticle(float speed)
    {
        var main = particle.main;
        main.startSpeed = speed;
        if (particle.isStopped)
        {
            particle.Play();
        }
    }
}