using UniRx;
using System.Collections;
using UnityEngine;

public class CameraSetPositions : MonoBehaviour
{
    private int _NextIndex, _PreviousIndex;
    [SerializeField] private Transform[] _Positions;

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

    bool _Timing;
    float _timer;
    float _Timer
    {
        get { return _timer; }
        set
        {
            if (_Timing)
            {
                _timer = value;
                if (_timer > 1.0f)
                {
                    _timer = 1.0f;
                    _Timing = false;
                }
            }
        }
    }

    public void Next()
    {
        _PreviousIndex = _NextIndex;
        _NextIndex++;
        _Timing = true;
        _Timer = 0.0f;
        Observable.FromCoroutine(SetPosition).Subscribe().AddTo(this);
    }

    public void Previous()
    {
        _PreviousIndex = _NextIndex;
        _NextIndex--;
        _Timing = true;
        _Timer = 0.0f;
        Observable.FromCoroutine(SetPosition).Subscribe().AddTo(this);
    }

    private void Start()
    {
        
    }

    IEnumerator SetPosition()
    {
        while (_Timing)
        {
            _Timer += Time.deltaTime;
            _Transoform.position = Vector3.Lerp(_Positions[_PreviousIndex].position, _Positions[_NextIndex].position, _Timer);
            yield return null;
        }
        yield return null;
    }

}
