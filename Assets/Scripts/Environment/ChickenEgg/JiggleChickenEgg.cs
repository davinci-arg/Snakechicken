using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JiggleChickenEgg : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxWaitTime = 6f;

    private int _jiggleID;

    public event UnityAction Jiggled;

    private void Awake()
    {
        _jiggleID = Animator.StringToHash("Jiggle");
    }

    public void StartJiggling() => StartCoroutine(Jiggling());

    private IEnumerator Jiggling()
    {
        WaitForSeconds waitForJiggle = new WaitForSeconds(Random.Range(0, _maxWaitTime));
        yield return waitForJiggle;
        _animator.SetTrigger(_jiggleID);
        Jiggled?.Invoke();
    }
}
