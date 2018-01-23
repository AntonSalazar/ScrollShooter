using UniRx;
using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    [SerializeField] private Vector2 _RandomTime;
    [SerializeField] private PoolComponent[] _Bonuses;
    [SerializeField] private Transform[] _SpawnPoints;


    private void Start()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(Random.Range(_RandomTime.x, _RandomTime.y))).
            Repeat().Subscribe(_ => Spawn()).AddTo(this);
    }

    private void Spawn()
    {
        int _randomPoint = Random.Range(0, _SpawnPoints.Length);
        int _randomBonus = Random.Range(0, _Bonuses.Length);

        Bonuse _bonuse = PoolManager.GetPoolObject<Bonuse>(_Bonuses[_randomBonus], _SpawnPoints[_randomPoint].position, _SpawnPoints[_randomPoint].rotation);
    }
}
