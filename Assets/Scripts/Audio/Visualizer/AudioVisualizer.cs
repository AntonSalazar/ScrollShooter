using UniRx;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    [SerializeField, Range(64, 1024)] int _VisualizerSamples = 64;
    

    [SerializeField] private float _Smoothes = 2.0f;
    [SerializeField] private float _RefValue = 0.1f;

    [SerializeField, ReadOnly] private float _RMSValue;
    [SerializeField, ReadOnly] private float _DBValue;

    private static float[] _SpectrumData;
    public static float _CurrentRate;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void GetVolume()
    {
        float _Sum = 0.0f;
        _SpectrumData = AudioListener.GetOutputData(_VisualizerSamples, 0);
        for (int i = 0; i < _VisualizerSamples; i++)
        {
            _Sum += _SpectrumData[i] * _SpectrumData[i];
        }
        _RMSValue = Mathf.Sqrt(_Sum / _VisualizerSamples);
        _DBValue = 20 * Mathf.Log10(_RMSValue / _RefValue);
        if (_DBValue < -160.0f) _DBValue = -160.0f;

        _CurrentRate = _Smoothes * _RMSValue;
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(x =>
        {
            GetVolume();
        }).AddTo(this);
    }

    public static float GetSample(int _index)
    {
        return _SpectrumData[_index];
    }
}
