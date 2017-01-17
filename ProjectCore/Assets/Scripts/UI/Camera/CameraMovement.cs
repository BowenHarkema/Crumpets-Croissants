using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private int _Boundary, _CameraMoveSpeed, _LvlMin, _LvlMax;

    private int _ScreenWidth;

    void Start()
    {
        _ScreenWidth = Screen.width;
    }

    void Update()
    {
        if (Input.mousePosition.x >= _ScreenWidth - _Boundary && transform.position.x <= _LvlMax)
        {
            transform.position += new Vector3(_CameraMoveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.mousePosition.x <= 0 + _Boundary && transform.position.x >= _LvlMin)
        {
            transform.position -= new Vector3(_CameraMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
