using UnityEngine.SceneManagement;
using UnityEngine;

public class UIOverlay : MonoBehaviour
{
    private static bool gameIsPaused = false;
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        PauseMenu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }     
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void Home(int _index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_index);
        
    }
   
   
}
