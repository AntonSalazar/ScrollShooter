using UniRx;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Vector3 _Direction = Vector3.up;
    [SerializeField] float _Speed = 10.0f;
    [SerializeField] bool _UseAudioVizualizer;
    [SerializeField] bool _UseRandomChanger;
    [SerializeField] Vector2 _RandomTimeChanger = new Vector2(1, 3);

    float _RandomTime;

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
        if (_UseRandomChanger)
        {
            _RandomTime = Random.Range(_RandomTimeChanger.x, _RandomTimeChanger.y);
            Observable.Timer(System.TimeSpan.FromSeconds(_RandomTime)).Repeat().Subscribe(x =>
            {
                _RandomTime = Random.Range(_RandomTimeChanger.x, _RandomTimeChanger.y);
                _Direction *= -1;
            }).AddTo(this);
        }
        
        Observable.EveryUpdate().Subscribe(x => Rotate()).AddTo(this);
    }

    private void Rotate()
    {
        _Transoform.Rotate(_Direction, _Speed * ((_UseAudioVizualizer) ? AudioVisualizer._CurrentRate : 1.0f));
    }
}
