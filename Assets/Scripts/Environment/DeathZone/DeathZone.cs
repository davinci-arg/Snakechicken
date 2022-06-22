using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Chicken>(out Chicken chicken))
        {
            Instantiate(_deathEffect, chicken.transform.position, Quaternion.identity);
            Destroy(chicken.SnakeChickenParent.gameObject);
            UIManager.Instance.SetActiveButton<RestartLevelButoon>();
        }
    }
}
