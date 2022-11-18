using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            pauseMenuUI.SetActive(true);

            if (!GameIsPaused)
            {
                Pause();
            }
        }
    }

    public static void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("FYPGame");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
