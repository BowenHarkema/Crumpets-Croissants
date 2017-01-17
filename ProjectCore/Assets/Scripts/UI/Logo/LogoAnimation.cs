using UnityEngine;
using System.Collections;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField]
    private float _Amp = 1.0f;

    [SerializeField]
    private float _Frequency = 1.0f;

    private RectTransform _RectTransform;
    private float _Timer = 0.0f;
    private float _DefaultYPos = 0.0f;

    private void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();
        _DefaultYPos = _RectTransform.anchoredPosition.y;
    }

    private void Update()
    {
        float sinValue = Mathf.Sin(_Timer * _Frequency) * _Amp;
        Vector3 anchoredpos = _RectTransform.anchoredPosition;
        anchoredpos.y = _DefaultYPos + sinValue;

        _RectTransform.anchoredPosition = anchoredpos;

        _Timer += Time.deltaTime;
    }
}

