
public class RestartLevelButoon : UIButton
{
    private void OnEnable()
    {
        Button.onClick.AddListener(LevelManager.Instance.RestartLevel);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(LevelManager.Instance.RestartLevel);
    }
}
