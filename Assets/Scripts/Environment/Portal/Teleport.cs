using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Teleport : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Chicken>(out Chicken chicken))
        {
            UIManager.Instance.SetActiveButton<NextLevelButton>();
            Destroy(chicken.SnakeChickenParent.gameObject);
        }
    }
}
