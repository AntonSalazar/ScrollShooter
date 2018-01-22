using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image _BackgroundLoading;
    private Color _ColorAlpha = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    private Color _CurrentColor;
    private AsyncOperation _LoaderAsync;


    private float _timer;
    private float _Timer
    {
        get { return _timer; }
        set
        {
            _Timer = value;
            if (_Timer > 1.0f)
            {
                _Timer = 1.0f;
                _LoaderAsync.allowSceneActivation = true;
            }
        }
    }

    public void LoadScene(int _index)
    {
        _LoaderAsync = SceneManager.LoadSceneAsync(_index);
        _LoaderAsync.allowSceneActivation = true;
        _LoaderAsync.AsAsyncOperationObservable().Do(x =>
        {
            _Timer += Time.deltaTime;
            _CurrentColor = Color.Lerp(_ColorAlpha, Color.black, _Timer);
            _BackgroundLoading.color = _CurrentColor;
            Debug.Log(_Timer);
        }).Subscribe( x => { Debug.Log(_Timer); }).AddTo(this);

    }

    /*IEnumerator Loading()
    {
        int _Minus = 1;
        float _Timer = 0.0f;

        _LoaderAsync = SceneManager.LoadSceneAsync(_IndexScene, LoadSceneMode.Single);
        _LoaderAsync.allowSceneActivation = false;
        while (true)
        {
            _Timer += Time.deltaTime * _Minus;
            if (_Timer > 1.0f && _Minus == 1)
            {
                _Timer = 1.0f;
                _Minus = -1;
            }
            _CurrentColor = Color.Lerp(_ColorAlpha, Color.black, _Timer);

            yield return null;
        }
        yield return null;
    }*/
    
}

