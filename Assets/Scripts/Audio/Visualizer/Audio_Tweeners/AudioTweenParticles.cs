using UniRx;
using UnityEngine;

public class AudioTweenParticles : MonoBehaviour
{
    [SerializeField] bool _UseAudioVisualizer = true;
    [SerializeField] float _StartSpeed = 5.0f;
    [SerializeField] float _SpeedMultiplay = 2.0f;
    ParticleSystem _Particles;
    ParticleSystem.MainModule _Main;
    private void Start()
    {
        _Particles = GetComponent<ParticleSystem>();
        _Main = _Particles.main;

        if (_UseAudioVisualizer)
        {
            Observable.EveryUpdate().Subscribe(_ => FX()).AddTo(this);
        }
    }

    private void FX()
    {
        _Main.simulationSpeed = Mathf.Abs(_StartSpeed * _SpeedMultiplay * AudioVisualizer._CurrentRate);
    }
}
