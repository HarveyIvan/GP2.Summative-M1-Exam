using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    void Start()
    {
        // Unlock and show cursor when the Start Scene opens
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Just in case the game was paused before returning to menu
        Time.timeScale = 1f;
    }

    public void OnStartClick()
    {
        // Hide and lock the cursor when entering gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("SampleScene");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
