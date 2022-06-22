using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Egg : MonoBehaviour
{
    [SerializeField] private Chicken _template;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxWaitTime = 6f;

    private int _jiggleID;

    public event UnityAction Jiggled;

    private void Start()
    {
        _jiggleID = Animator.StringToHash("Jiggle");
        Jiggled += StartJiggle;
        StartJiggle();
    }

    public Chicken CreateChicken(Transform bornPlace) => Instantiate(_template, bornPlace.position, bornPlace.rotation);

    private void StartJiggle() => StartCoroutine(Jiggling());
    
    private IEnumerator Jiggling()
    {        
        WaitForSeconds waitForJiggle = new WaitForSeconds(Random.Range(0, _maxWaitTime));
        yield return waitForJiggle;
        _animator.SetTrigger(_jiggleID);
        Jiggled?.Invoke();
    }
}
