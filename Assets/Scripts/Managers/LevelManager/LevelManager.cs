using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void LoadNextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
}
