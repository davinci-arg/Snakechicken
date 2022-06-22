
public class NextLevelButton : UIButton
{
    private void OnEnable()
    {
        Button.onClick.AddListener(LevelManager.Instance.LoadNextLevel);
        Button.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(LevelManager.Instance.LoadNextLevel);
        Button.onClick.RemoveListener(Hide);
    }
}
