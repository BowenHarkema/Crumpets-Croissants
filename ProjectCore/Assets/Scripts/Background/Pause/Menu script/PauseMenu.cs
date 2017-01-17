using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _PauseMenu;

    private bool _IsPaused = false;

    void Update()
    {
        if (!_IsPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            _PauseMenu.SetActive(true);
            Time.timeScale = 0;
            _IsPaused = !_IsPaused;
        }
    }

    public void Continue()
    {
        _IsPaused = !_IsPaused;
        Time.timeScale = 1;
        _PauseMenu.SetActive(false);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("_Level", Application.loadedLevel);
    }

    public void Exit()
    {
        _IsPaused = !_IsPaused;
        Time.timeScale = 1;

        float fadetime = GameObject.FindGameObjectWithTag("_GM").GetComponent<Fade>().BeginFade(1);
        fadetime -= 1 / Time.deltaTime;

        if (fadetime <= 0)
        {
            SceneManager.LoadScene("Start Menu");
        }
    }
}
