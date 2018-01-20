using UniRx;
using UnityEngine;

public class AudioTweenScale : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Vector3 _StartScale = Vector3.one;
    [SerializeField, ReadOnly] private Vector3 _CurrentScale = Vector3.one;
    [SerializeField] float _Speed = 5.0f;
    [SerializeField] Material _Material;

    [Header("Axis")]
    [SerializeField] bool _X;
    [SerializeField] bool _Y;
    [SerializeField] bool _Z;

    Color _Color = new Color (0.0f, 0.0f, 0.0f);

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
        Observable.EveryUpdate().Subscribe(x => TweenScale());
    }

    void TweenScale()
    {
        _CurrentScale.x = (_X) ? Mathf.Lerp(_CurrentScale.x, _StartScale.x + AudioVisualizer._CurrentRate, Time.deltaTime * _Speed) : _StartScale.x;
        _CurrentScale.y = (_Y) ? Mathf.Lerp(_CurrentScale.y, _StartScale.y + AudioVisualizer._CurrentRate, Time.deltaTime * _Speed) : _StartScale.y;
        _CurrentScale.z = (_Z) ? Mathf.Lerp(_CurrentScale.z, _StartScale.z + AudioVisualizer._CurrentRate, Time.deltaTime * _Speed) : _StartScale.z;

        _Transoform.localScale = _CurrentScale;
        _Color = Color.Lerp(_Color, AudioVisualizer.CurrentColor, Time.deltaTime * _Speed);
        _Material.SetColor("_EmissionColor", _Color);
        _Material.SetColor("_Color", _Color);
    }
}
