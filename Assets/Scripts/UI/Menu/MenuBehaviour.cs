using UnityEngine;
using UnityEngine.UI;
public class MenuBehaviour : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _Main;
    [SerializeField] private GameObject _ChoosePlayer;
    [SerializeField] private GameObject _Changer;
    [SerializeField] private GameObject _Settings;
    [SerializeField] private GameObject _Loading;

    [Header("PlayerSettings")]
    [SerializeField, ReadOnly]
    int _PlayerPrefabIndex;
    [SerializeField] private GameObject[] _PlayerPrefabs;

    [Header("Music Settings")]
    [SerializeField] private bool _Music;
    [SerializeField, Range(0.0f, 1.0f)] private float _MusicValue;
    [SerializeField] private Button _Btn_Music;
    [SerializeField] private Slider _Sld_Music;
    [SerializeField] private Sprite _MusicOff;
    [SerializeField] private Sprite _MusicOn;

    float MusicValue
    {
        get { return _MusicValue; }
        set
        {
            if (_MusicValue != value)
            {
                _MusicValue = Mathf.Clamp01(value);
                _Sld_Music.value = _MusicValue;
                AudioManager.SetMusicVolume(_MusicValue);
                PlayerPrefs.SetFloat("MusicValue", _MusicValue);
            }
        }
    }

    bool Music
    {
        get { return _Music; }
        set
        {
            if (_Music != value)
            {
                _Music = value;
                AudioManager.SetMuteMusic(value);
                _Btn_Music.image.sprite = (value) ? _MusicOn : _MusicOff;
                _Sld_Music.interactable = value;
                PlayerPrefsHelper.SetBool("MusicBool", _Music);
            }
        }
    }
    [Header("Sound Settings")]
    [SerializeField] private bool _Sound;
    [SerializeField, Range(0.0f, 1.0f)] private float _SoundValue;
    [SerializeField] private Button _Btn_Sound;
    [SerializeField] private Slider _Sld_Sound;
    [SerializeField] private Sprite _SoundOff;
    [SerializeField] private Sprite _SoundOn;
    float SoundValue
    {
        get { return _SoundValue; }
        set
        {
            if (_SoundValue != value)
            {
                _SoundValue = Mathf.Clamp01(value);
                _Sld_Sound.value = _SoundValue;
                AudioManager.SetSoundVolume(_SoundValue);
                PlayerPrefs.SetFloat("SoundValue", _SoundValue);
            }
        }
    }

    bool Sound
    {
        get { return _Sound; }
        set
        {
            if (_Sound != value)
            {
                _Sound = value;
                AudioManager.SetMuteSound(value);
                _Btn_Sound.image.sprite = (value) ? _SoundOn : _SoundOff;
                _Sld_Sound.interactable = value;
                PlayerPrefsHelper.SetBool("SoundBool", _Sound);
            }
        }
    }


    SceneLoader _SceneLoader;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    void Init()
    {
        _SceneLoader = GetComponent<SceneLoader>();
        Music = PlayerPrefsHelper.GetBool("MusicBool", true);
        MusicValue = PlayerPrefs.GetFloat("MusicValue", 1.0f);

        Sound = PlayerPrefsHelper.GetBool("SoundBool", true);
        SoundValue = PlayerPrefs.GetFloat("SoundValue", 1.0f);

        if (_Settings.activeSelf) ShowSettings();
        if (_Changer.activeSelf) ShowChanger();
        if (_ChoosePlayer.activeSelf) ShowChoosenPlayer();
        if (_Loading.activeSelf) ShowLoading();

        _PlayerPrefabIndex = PlayerPrefs.GetInt("PlayerPrefab", 0);
        _PlayerPrefabs[_PlayerPrefabIndex].SetActive(true);
    }

    private void Start()
    {
        AudioManager.SetMusicClip();
    }

    public void SetMusicValue()
    {
        MusicValue = _Sld_Music.value;
    }

    public void SetOnOffMusic()
    {
        Music = !Music;
    }

    public void SetSoundValue()
    {
        SoundValue = _Sld_Sound.value;
    }

    public void SetOnOffSound()
    {
        Sound = !Sound;
    }

    public void ShowSettings()
    {
        _Settings.SetActive(!_Settings.activeSelf);
    }

    public void ShowChanger()
    {
        _Changer.SetActive(!_Changer.activeSelf);
    }

    public void ShowMainMenu()
    {
        _Main.SetActive(!_Main.activeSelf);
    }

    public void ShowChoosenPlayer()
    {
        _ChoosePlayer.SetActive(!_ChoosePlayer.activeSelf);
    }

    public void ShowLoading()
    {
        _Loading.SetActive(!_Loading.activeSelf);
    }

    public void NextPlayerPrefab()
    {
        _PlayerPrefabs[_PlayerPrefabIndex].SetActive(false);
        _PlayerPrefabIndex++;
        if (_PlayerPrefabIndex == _PlayerPrefabs.Length) _PlayerPrefabIndex = 0;
        PlayerPrefs.SetInt("PlayerPrefab", _PlayerPrefabIndex);
        _PlayerPrefabs[_PlayerPrefabIndex].SetActive(true);
    }

    public void PreviouslyPlayerPrefab()
    {
        _PlayerPrefabs[_PlayerPrefabIndex].SetActive(false);
        _PlayerPrefabIndex--;
        if (_PlayerPrefabIndex == -1) _PlayerPrefabIndex = (_PlayerPrefabs.Length - 1);
        PlayerPrefs.SetInt("PlayerPrefab", _PlayerPrefabIndex);
        _PlayerPrefabs[_PlayerPrefabIndex].SetActive(true);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlaySingle()
    {
        _SceneLoader.LoadScene(1);
    }

    public void PlayMulty()
    {

    }
}
