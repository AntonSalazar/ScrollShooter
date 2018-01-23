using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveShip : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _Speed = 10.0f;
    [SerializeField] private float _Depth = 5.0f;

    [Header("Bounce")]
    [SerializeField] private Vector2 _MinBounce;
    [SerializeField] private Vector2 _MaxBounce;

    [Header("Rotation")]
    [SerializeField] private Vector2 _RotationAngleMin;
    [SerializeField] private Vector2 _RotationAngleMax;

    private Rigidbody _Rigidbody;

    private Vector3 _Direction;
    private Vector3 _Position;
    private Vector3 _Rotation;
    private Vector2 _MoveDirection;

    private float _XInput
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    private float _YInput
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _Position = new Vector3(0.0f, 0.0f, _Depth);
        
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            Moving();
            Rotation();
        }).AddTo(this);
    }

    void Moving()
    {
        _Direction = new Vector3(_XInput, _YInput);
        _Rigidbody.velocity = _Direction * _Speed;

        _Position.x = Mathf.Clamp(_Rigidbody.position.x, _MinBounce.x, _MaxBounce.x);
        _Position.y = Mathf.Clamp(_Rigidbody.position.y, _MinBounce.y, _MaxBounce.y);
        _Rigidbody.position = _Position;
    }

    void Rotation()
    {
        _MoveDirection.x = Mathf.Lerp(_MoveDirection.x, _YInput, Time.deltaTime * _Speed);
        _MoveDirection.y = Mathf.Lerp(_MoveDirection.y, _XInput, Time.deltaTime * _Speed);
        
        _Rotation.x = Mathf.Lerp(_RotationAngleMin.x, _RotationAngleMax.x, _MoveDirection.x + 0.5f);
        _Rotation.z = Mathf.Lerp(_RotationAngleMin.y, _RotationAngleMax.y, _MoveDirection.y + 0.5f);
        _Rigidbody.rotation = Quaternion.Euler(_Rotation);
    }
}
