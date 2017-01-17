using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public void StartGame()
    {
        float fadetime = GameObject.FindGameObjectWithTag("_GM").GetComponent<Fade>().BeginFade(1);
        fadetime -= 1 / Time.deltaTime;

        if (fadetime <= 0)
        {
            SceneManager.LoadScene("Level_1");
        }
    }

    public void Load()
    {
        Application.LoadLevel(PlayerPrefs.GetInt("_Level"));
    }

    public void Exit()
    {
        Application.Quit();
    }
}
