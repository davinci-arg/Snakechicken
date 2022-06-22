using UnityEngine;
using UnityEngine.UI;

public abstract class UIButton : MonoBehaviour
{
    [SerializeField] protected Button Button;

    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}
