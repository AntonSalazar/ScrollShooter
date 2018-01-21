using UniRx;
using UnityEngine;

public class AudioTweenScale : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Vector3 _StartScale = Vector3.one;
    [SerializeField] private Vector3 _ScaleMin = Vector3.one;
    [SerializeField] private Vector3 _ScaleMax = Vector3.one;
    [SerializeField] private bool _UseRandomSeed;
    [SerializeField] private Vector2 _RandomSeed = Vector2.one;
    [SerializeField, ReadOnly] private Vector3 _CurrentScale = Vector3.one;
    [SerializeField] float _Speed = 5.0f;

    float _Random;

    [Header("Axis")]
    [SerializeField] bool _X;
    [SerializeField] bool _Y;
    [SerializeField] bool _Z;

    bool b_Transform;
    Transform _transform;
    Transform _Transoform
    {
        get
        {
            if (!b_Transform) if (_transform = transform) b_Transform = true;
            return _transform;
        }
    }

    private void Start()
    {
        _Random = (_UseRandomSeed) ? Random.Range(_RandomSeed.x, _RandomSeed.y) : 1.0f;
        Observable.EveryUpdate().Subscribe(x => TweenScale()).AddTo(this);
    }

    void TweenScale()
    {
        _CurrentScale.x = (_X) ? Mathf.Clamp(Mathf.Lerp(_CurrentScale.x, _StartScale.x + AudioVisualizer._CurrentRate, Time.deltaTime * _Speed * _Random), _ScaleMin.x, _ScaleMax.x) : _StartScale.x;
        _CurrentScale.y = (_Y) ? Mathf.Clamp(Mathf.Lerp(_CurrentScale.y, _StartScale.y + AudioVisualizer._CurrentRate, Time.deltaTime * _Speed * _Random), _ScaleMin.y, _ScaleMax.y) : _StartScale.y;
        _CurrentScale.z = (_Z) ? Mathf.Clamp(Mathf.Lerp(_CurrentScale.z, _StartScale.z + AudioVisualizer._CurrentRate, Time.deltaTime * _Speed * _Random), _ScaleMin.z, _ScaleMax.z) : _StartScale.z;

        _Transoform.localScale = _CurrentScale;
    }
}
