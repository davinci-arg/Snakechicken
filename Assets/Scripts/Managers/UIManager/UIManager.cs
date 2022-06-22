using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIButton[] _buttons;
    
    public void SetActiveButton<T>() where T: UIButton
    {
        var button = _buttons.FirstOrDefault(uiButton => uiButton as T);
        button.Show();
    }
}
