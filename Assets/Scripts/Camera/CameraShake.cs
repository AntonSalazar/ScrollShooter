using UniRx;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Transform _CameraTransform;
    [SerializeField] private bool _UseAudioVisualizer;
    [SerializeField] private float _Speed = 1.0f;

    [SerializeField] private Vector3 _Min;
    [SerializeField, ReadOnly] private Vector3 _Current;
    [SerializeField] private Vector3 _Max;

    [SerializeField] private Vector3 _Threshold = Vector3.one;
    [SerializeField, ReadOnly] private Vector3 _Timing;
    [SerializeField, ReadOnly] private Vector3 _Reverse = Vector3.one;
    

    private void Timing()
    {
        SetTiming(ref _Timing.x, ref _Reverse.x, _Threshold.x);
        SetTiming(ref _Timing.y, ref _Reverse.y, _Threshold.y);
        SetTiming(ref _Timing.z, ref _Reverse.z, _Threshold.z);
    }

    private void SetTiming(ref float _currentTiming, ref float _reverse, float _threshold)
    {
        _currentTiming += (!_UseAudioVisualizer) ? _reverse * Time.deltaTime * _Speed * _threshold :
            _reverse * Time.deltaTime * _Speed * _threshold * AudioVisualizer._CurrentRate;
        if (_currentTiming >= 1.0f && _reverse != -1) _reverse *= -1;
        else if (_currentTiming <= 0.0f && _reverse != 1) _reverse *= -1;
    }

    private void Shake()
    {
        _Current.x = Mathf.Lerp(_Min.x, _Max.x, _Timing.x);
        _Current.y = Mathf.Lerp(_Min.y, _Max.y, _Timing.y);
        _Current.z = Mathf.Lerp(_Min.z, _Max.z, _Timing.z);

        _CameraTransform.localEulerAngles = _Current;
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(x =>
        {
            Timing();
            Shake();
        }).AddTo(this);
    }
}
