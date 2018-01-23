using UniRx;
using UnityEngine;

[System.Serializable]
public class Weapons
{
    [SerializeField] private string _Name;
    [SerializeField] private PoolComponent _BlastPrefab;
    [SerializeField] private float _Damage = 1.0f;
    [SerializeField] private Transform[] _FireStart;

    byte _Index;
    internal void SetIndex(byte _index)
    {
        _Index = _index;
    }

    internal void Shoot(byte _index)
    {
        if (_Index == _index)
        {
            for (int i = 0; i < _FireStart.Length; i++)
            {
                BulletController _Bullet = PoolManager.GetPoolObject<BulletController>(_BlastPrefab, _FireStart[i].position, _FireStart[i].rotation);
            }
        }
    }
}

public class ShootController : MonoBehaviour
{
    enum ShootType
    {
        Single      = 0,
        Double      = 1,
        Triple      = 2,
        Quadruple   = 3
    }

    [SerializeField] private ShootType _ShootType = ShootType.Single;
    [SerializeField] private float _FireDelay = 0.3f;
    [SerializeField] private Weapons[] _Weapons;
    
    internal delegate void ShootHandler(byte _index);
    internal event ShootHandler _OnShootHandler = (_index) => { };

    private float _FireTiming;

    bool _Fire
    {
        get
        {
            return Input.GetButton("Fire1");
        }
    }

    private void OnEnable()
    {
        for (byte i = 0; i < _Weapons.Length; i++)
        {
            _Weapons[i].SetIndex(i);
            _OnShootHandler += _Weapons[i].Shoot;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _Weapons.Length; i++)
        {
            _OnShootHandler -= _Weapons[i].Shoot;
        }
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => { FireControl(); }).AddTo(this);
    }

    private void FireControl()
    {
        _FireTiming += Time.deltaTime;

        if (_FireTiming > _FireDelay) _FireTiming = _FireDelay;

        if (_Fire && _FireTiming == _FireDelay)
        {
            _FireTiming = 0.0f;
            _OnShootHandler((byte)_ShootType);
        }
    }
}
