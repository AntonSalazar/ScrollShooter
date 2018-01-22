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

    bool _isLoading;

    private float _Timer;

    public void LoadScene(int _index)
    {
        _LoaderAsync = SceneManager.LoadSceneAsync(_index);
        _LoaderAsync.allowSceneActivation = false;
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        int _Minus = 1;
        _isLoading = true;
        while (_isLoading)
        {
            _Timer += Time.deltaTime * _Minus;
            _CurrentColor = Color.Lerp(_ColorAlpha, Color.black, _Timer);
            _BackgroundLoading.color = _CurrentColor;
            if (_Timer > 1.0f && _Minus == 1)
            {
                _Minus = -1;
                _Timer = 1.0f;
                _LoaderAsync.allowSceneActivation = true;
                yield return new WaitForSecondsRealtime(3.0f);
            }
            else if (_Timer < 0.0f && _Minus == -1)
            {
                _Timer = 0.0f;
                _Minus = 1;
                _isLoading = false;
            }
            yield return null;
        }
        yield return null;
    }
    
}

