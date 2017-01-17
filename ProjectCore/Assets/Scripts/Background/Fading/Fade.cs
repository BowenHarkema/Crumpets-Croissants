using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    public Texture2D _FadeTexture;
    public float _FadeSpeed;

    private int _DrawDepth = -1000;
    private float _Alpha = 1.0f;
    private int _FadeDir = -1;

    void OnGUI()
    {
        _Alpha += _FadeDir * _FadeSpeed * Time.deltaTime;
        _Alpha = Mathf.Clamp01(_Alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, _Alpha);
        GUI.depth = _DrawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),_FadeTexture);
    }

    public float BeginFade(int direction)
    {
        _FadeDir = direction;
        return (_FadeSpeed);
    }

    void OnLevelLoaded()
    {
        BeginFade(-1);
    }
}
