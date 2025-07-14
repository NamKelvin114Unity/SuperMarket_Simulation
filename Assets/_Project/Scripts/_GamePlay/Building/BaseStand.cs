using PrimeTween;
using UnityEngine;
using VirtueSky.Variables;

public class BaseStand : MonoBehaviour
{
    [SerializeField] private float offsetScale = 1.2f;
    [SerializeField] private FloatVariable timeScaleBaseStandVariable;

    Vector3 _defaultScale;
    bool _isScaled;

    void Start()
    {
        _defaultScale = transform.localScale;
    }

    void SetScale(bool scaleUp)
    {
        if (_isScaled == scaleUp) return;

        var targetScale = scaleUp ? _defaultScale * offsetScale : _defaultScale;
        transform.DOScale(targetScale, timeScaleBaseStandVariable.Value);
        _isScaled = scaleUp;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            SetScale(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            SetScale(false);
        }
    }
}