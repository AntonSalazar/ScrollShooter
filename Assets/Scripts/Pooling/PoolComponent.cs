using UnityEngine;
using System.Collections;

public class PoolComponent : MonoBehaviour
{
    [SerializeField] internal int _CathegoryIndex, _ElementIndex;
    [SerializeField] float _TimeReturner = 1.0f;
    [SerializeField] bool _AutoDisable;

    internal delegate void OnPoolHandler();
    
    internal event OnPoolHandler OnEnableEvent = () => {}, OnDisableEvent = () => {}, OnPrepareEnable = () => {}, OnPrepareDisable = () => {};


    internal void SetActive(bool _value)
    {
        if(_value) OnPrepareEnable();
        else OnPrepareDisable();
        gameObject.SetActive(_value);
    }

    void OnEnable()
    {
        OnEnableEvent();
        if (_AutoDisable) StartCoroutine(AutoDisable());
    }

    void OnDisable()
    {
        OnDisableEvent();
        StopCoroutine(AutoDisable());
    }

    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(_TimeReturner);
        SetActive(false);
    }
}
