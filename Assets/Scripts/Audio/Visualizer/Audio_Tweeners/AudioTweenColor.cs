using UniRx;
using UnityEngine;

public class AudioTweenColor : MonoBehaviour
{
    private enum ColorType
    {
        Surface,
        Skybox,
        Particle
    }

    [SerializeField] private Gradient _GradientColor;
    [SerializeField] private Material _Material;
    [SerializeField] private ColorType _ColorType = ColorType.Surface;
    [SerializeField] private float _Speed = 5.0f;
    private Color _Color = new Color(0.0f, 0.0f, 0.0f);

    private Color CurrentColor
    {
        get { return _GradientColor.Evaluate(AudioVisualizer._CurrentRate); }
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(x =>
        {
            _Color = Color.Lerp(_Color, CurrentColor, Time.deltaTime * _Speed);
            switch (_ColorType)
            {
                case ColorType.Surface:
                    _Material.SetColor("_EmissionColor", _Color);
                    _Material.SetColor("_Color", _Color);
                    break;
                case ColorType.Particle:
                    _Material.SetColor("_TintColor", _Color);
                    break;
                case ColorType.Skybox:
                    _Material.SetColor("_Tint", _Color);
                    break;
            }
        }).AddTo(this);
    }
}
