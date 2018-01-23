using UniRx;
using UnityEngine;

public class PlayerOptions : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _Durability = 100.0f;
    [SerializeField] private float _Shield = 100.0f;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] _ModelPrefabs;
    [SerializeField] private GameObject[] _ExplosionPrefabs;
    [SerializeField] private GameObject[] _BlastPrefabs;

    private void Start()
    {
        _ModelPrefabs[PlayerPrefs.GetInt("PlayerPrefab")].SetActive(true);
    }
}
