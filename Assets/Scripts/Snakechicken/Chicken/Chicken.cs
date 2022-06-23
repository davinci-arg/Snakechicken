using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Chicken : MonoBehaviour
{
    [SerializeField] private Transform _ceneter;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Animator _animator;

    private Vector3 _startPosition;
    private SnakeChicken _snakeChicken;

    public SnakeChicken SnakeChickenParent => _snakeChicken;

    public event UnityAction<Vector3> Stopped;
    public event UnityAction<Chicken> Catched;

    private void Start()
    {
        _snakeChicken = GetComponentInParent<SnakeChicken>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ChickenEgg>(out ChickenEgg egg))
        {
            Catched?.Invoke(egg.Hatch(transform));
            Destroy(egg.gameObject);
        }
    }

    public void Move(Vector3 toPosition, float speedMove)
    {
        _startPosition = transform.position;
        Vector3 moveDirection = toPosition - transform.position;

        if (Vector3.Dot(transform.forward, moveDirection) < 0)
        {
            SwapDirection();
        }

        PlayAnimation(ChickenStates.Run);
        transform.DOMove(toPosition, speedMove).OnComplete(() => StoppedMove());
    }
    
    public void Fly()
    {
        PlayAnimation(ChickenStates.Flight);
    }

    public void Stay()
    {
        StopAnimation(ChickenStates.Flight);
    }

    public bool CheckCollisionWithGround(Vector3 direction, float lengthToGround)
    {
        return Physics.Raycast(_ceneter.position, direction, lengthToGround, _groundLayer);
    }

    public bool CheckCollisionWithChicken(Vector3 direction, float lengthToGround)
    {
        if (Physics.Raycast(_ceneter.position, direction, out RaycastHit hit, lengthToGround))
        {
            return hit.transform.TryGetComponent<Chicken>(out Chicken chicken);
        }

        return false;
    }

    private void SwapDirection() => transform.eulerAngles *= -1;

    private void StoppedMove()
    {
        StopAnimation(ChickenStates.Run);
        Stopped?.Invoke(_startPosition);
    }

    private void PlayAnimation(string animation) => _animator.SetBool(animation, true);

    private void StopAnimation(string animation) => _animator.SetBool(animation, false);
}