using UniRx;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _Damage;
    [SerializeField] private float _Speed = 10.0f;

    Transform _Transform;

    private void Awake()
    {
        _Transform = transform;
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => Move()).AddTo(this);
    }

    private void Move()
    {
        _Transform.Translate(Vector3.right * _Speed);
    }
}
