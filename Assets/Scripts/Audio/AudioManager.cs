using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioListener))]
public class AudioManager : MonoBehaviour
{
    private static bool _isMusic;
    private static AudioSource _MusicSource;
    [SerializeField] private AudioMixerGroup _MusicMixer;

    public static AudioSource MusicSource
    {
        get
        {
            if (!_isMusic)
            {
                _MusicSource = new GameObject("_MusicSource").AddComponent<AudioSource>();
                _MusicSource.transform.SetParent(GameObject.FindWithTag("AudioManager").transform);
                _MusicSource.loop = true;
                _isMusic = true;
            }
            return _MusicSource;
        }
    }

    private static bool _isSound;
    private static AudioSource _SoundSource;
    [SerializeField] private AudioMixerGroup _SoundMixer;

    public static AudioSource SoundSource
    {
        get
        {
            if (!_isSound)
            {
                _SoundSource = new GameObject("_SoundSource").AddComponent<AudioSource>();
                _SoundSource.transform.SetParent(GameObject.FindWithTag("AudioManager").transform);
                _isSound = true;
            }
            return _SoundSource;
        }
    }
    public static AudioListener _Listener;

    private static bool _GettedMusic;
    private static AudioClip[] _MusicClips;
    private static AudioClip[] MusicClips
    {
        get
        {
            if (!_GettedMusic)
            {
                _MusicClips = Resources.LoadAll("Audio/Musics", typeof(AudioClip)).Cast<AudioClip>().ToArray();
                _GettedMusic = true;
            }
            return _MusicClips;
        }
    }

    private void Awake()
    {
        MusicSource.outputAudioMixerGroup = _MusicMixer;
        SoundSource.outputAudioMixerGroup = _SoundMixer;

        DontDestroyOnLoad(gameObject);
    }


    public static void SetSoundVolume(float _value)
    {
        SoundSource.volume = _value;
    }

    public static void SetMuteSound(bool _value)
    {
        SoundSource.mute = !_value;
    }

    public static void SetMusicVolume(float _value)
    {
        MusicSource.volume = _value;
    }

    public static void SetMuteMusic(bool _value)
    {
        MusicSource.mute = !_value;
    }

    public static void SetMusicClip(int _index = -1)
    {
        _index = (_index == -1) ? Random.Range(0, MusicClips.Length) : _index;
        MusicSource.clip = MusicClips[_index];
        MusicSource.Play();
    }
}
