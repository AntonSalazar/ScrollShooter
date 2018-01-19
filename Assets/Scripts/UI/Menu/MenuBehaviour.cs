using UniRx;
using UnityEngine;
using xPrefsTools;
using UnityEngine.UI;
public class MenuBehaviour : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _Changer;
    [SerializeField] private GameObject _Settings;

    [Header("Settings")]
    [SerializeField] private bool _Music;
    [SerializeField, Range(0.0f, 1.0f)] private float _MusicValue;
    [SerializeField] private Button _Btn_Music;
    [SerializeField] private Slider _Sld_Music;

    float MusicValue
    {
        get { return _MusicValue; }
        set
        {
            if (_MusicValue != value)
            {
                _MusicValue = Mathf.Clamp01(value);
                Music = (_MusicValue <= 0.0f) ? false : true;
                XPrefs.SetFloat("MusicValue", _MusicValue);
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
                XPrefs.SetBool("MusicToggle", _Music);
            }
        }
    }

    [SerializeField] private bool _Sound;
    [SerializeField, Range(0.0f, 1.0f)] private float _SoundValue;
    [SerializeField] private Button _Btn_Sound;
    [SerializeField] private Slider _Sld_Sound;

    float SoundValue
    {
        get { return _SoundValue; }
        set
        {
            if (_SoundValue != value)
            {
                _SoundValue = Mathf.Clamp01(value);
                Sound = (_SoundValue <= 0.0f) ? false : true;
                XPrefs.SetFloat("SoundValue", _SoundValue);
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
                XPrefs.SetBool("SoundToggle", _Sound);
            }
        }
    }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        Music = XPrefs.GetBool("MusicBool", 1);
        MusicValue = PlayerPrefs.GetFloat("MusicValue", 1.0f);
        Sound = XPrefs.GetBool("SoundBool", 1);
        SoundValue = PlayerPrefs.GetFloat("SoundValue", 1.0f);
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

    public void Quit()
    {
        Application.Quit();
    }

    public void PlaySingle()
    {

    }

    public void PlayMulty()
    {

    }
}
