using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ChickenEgg : MonoBehaviour
{
    [SerializeField] private Chicken _template;
    [SerializeField] private JiggleChickenEgg _jiggleChickenEgg;

    private void Start()
    {
        _jiggleChickenEgg.Jiggled += Jiggle;
        Jiggle();
    }

    public Chicken Hatch(Transform bornPlace) => Instantiate(_template, bornPlace.position, bornPlace.rotation);

    private void Jiggle() => _jiggleChickenEgg.StartJiggling();

}
