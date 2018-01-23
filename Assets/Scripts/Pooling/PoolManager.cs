using System;
using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    [SerializeField] PoolCathegory[] _PoolCathegories;

    static PoolCathegory[] _SPoolCathegories;

    void OnValidate()
    {
        if (_SPoolCathegories != _PoolCathegories) _SPoolCathegories = _PoolCathegories;

        for (short i = 0; i < _SPoolCathegories.Length; i++)
        {
            _SPoolCathegories[i].OnValueChange(i, transform);
        }
    }

    internal static T GetPoolObject<T>(int _indexCathegory, int _indexElement, Vector3 _position, Quaternion _rotation)
    {
        Transform _Object = _SPoolCathegories[_indexCathegory]._PoolElements[_indexElement].GetFreeObject();
        _Object.position = _position;
        _Object.rotation = _rotation;

        return _Object.GetComponent<T>();
    }

    internal static T GetPoolObject<T>(PoolComponent _pool, Vector3 _position, Quaternion _rotation)
    {
        Transform _Object = _SPoolCathegories[
			_pool._CathegoryIndex]._PoolElements[_pool._ElementIndex].GetFreeObject();
        _Object.position = _position;
        _Object.rotation = _rotation;

        return _Object.GetComponent<T>();
    }

    void Init()
    {
        _SPoolCathegories = _PoolCathegories;
        for (int i = 0; i < _SPoolCathegories.Length; i++)
        {
            _SPoolCathegories[i].Init();
        }
    }

    void Awake()
    {
        Init();
    }
}


[Serializable]
public struct PoolCathegory
{
    [SerializeField] internal string _CathegoryName;
    [SerializeField] short _Index;
    [SerializeField] Transform _Transform;
    [SerializeField] internal PoolElement[] _PoolElements;

    internal void OnValueChange(short _index, Transform _parent)
    {
        if (!_Transform || _Index != _index)
        {
            _Index = _index;
            GameObject _cathegory = new GameObject();
            _Transform = _cathegory.transform;
            _Transform.SetParent(_parent);
            _Transform.localPosition = Vector3.zero;
        }
        _Transform.name = _index.ToString("00") + " " + _CathegoryName + " Cathegory";

        for (int i = 0; i < _PoolElements.Length; i++)
        {
            _PoolElements[i].OnValueChange(_index, i, _Transform);
        }
    }

    internal void Init()
    {
        for (int i = 0; i < _PoolElements.Length; i++)
        {
            _PoolElements[i].Init();
        }
    }
}

[Serializable]
public class PoolElement
{
    [SerializeField] internal string _Name;
    [SerializeField] PoolComponent _PoolComponent;
    [SerializeField] Transform _Transform;
    [SerializeField] int _NeededCount;
    [SerializeField] PoolComponent[] _OnScene;

    [NonSerialized] List<PoolComponent> _List;
    List<PoolComponent> List
    {
        get
        {
            if (_List == null)
            {
                _List = new List<PoolComponent>();
                _OnScene = _List.ToArray();
            }
            return _List;
        }
    }

    internal void OnValueChange(int _CathegoryIndex, int _ElementIndex, Transform _cathegory)
    {
        if (_PoolComponent)
        {
            _Name = _PoolComponent.name;
            _PoolComponent._CathegoryIndex = _CathegoryIndex;
            _PoolComponent._ElementIndex = _ElementIndex;
            if (!_Transform)
            {
                GameObject _pool = new GameObject();
                _Transform = _pool.transform;
                _Transform.SetParent(_cathegory);
                _Transform.localPosition = Vector3.zero;
            }
            _Transform.name = _ElementIndex.ToString("00") + " " + _Name + " Parent";
        }
        else if(_Transform)
        {
            _Name = "";
            _Transform.name = "!EMPTY!";
        }
    }

    internal void Add(PoolComponent _component)
    {
        if (!List.Contains(_component))
        {
            List.Add(_component);
            _OnScene = _List.ToArray();
        }
    }

    internal void Remove(PoolComponent _component)
    {
        if (List.Contains(_component))
        {
            List.Remove(_component);
            _OnScene = _List.ToArray();
        }
    }

    internal Transform GetFreeObject()
    {
        foreach (PoolComponent _object in _OnScene)
        {
            if (!_object.gameObject.activeInHierarchy)
            {
                _object.SetActive(true);
                return _object.transform;
            }
        }
        Spawn();
        _OnScene[_OnScene.Length - 1].SetActive(true);
        return _OnScene[_OnScene.Length - 1].transform;
    }

    internal void Init()
    {
        if (_PoolComponent && _NeededCount > 0)
        {
            for (int i = 0; i < _NeededCount; i++)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        GameObject _currentPool = UnityEngine.Object.Instantiate(_PoolComponent.gameObject, _Transform.position, _Transform.rotation) as GameObject;

        _currentPool.name = _Name;
        _currentPool.transform.SetParent(_Transform);

        PoolComponent _component = _currentPool.GetComponent<PoolComponent>();
        Add(_component);
        _component.SetActive(false);
    }
}
