using UniRx;
using UnityEngine;

public class AIMoving : MonoBehaviour
{
    enum TypeMove
    {
        Simple,
        PingPong
    }

    [SerializeField] private TypeMove _TypeMove = TypeMove.Simple;
    [SerializeField] private Vector3 _Direction;
    [SerializeField] private float _Speed = 10.0f;

    Rigidbody _Rigidbody;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        Observable.EveryFixedUpdate().Subscribe(_ => Moving()).AddTo(this);
    }

    void Moving()
    {
        _Rigidbody.velocity = _Direction * _Speed;
    }
}
