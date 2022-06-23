using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeChicken : MonoBehaviour
{
    [SerializeField] private ScriptablSkillSnakechiken _skill;
    [SerializeField] private List<Chicken> _chickens;
    [SerializeField] private ParticleSystem _growEffect;

    private Queue<Chicken> _bodyParts;
    private Chicken _head;
    private bool _canMove;

    private void OnEnable()
    {
        foreach (Chicken chicken in _chickens)
        {
            chicken.Stopped += OnMove;
            chicken.Catched += OnGrow;
        }
    }

    private void OnDisable()
    {
        foreach (Chicken chicken in _chickens)
        {
            chicken.Stopped -= OnMove;
            chicken.Catched -= OnGrow;
        }
    }

    private void Start()
    {
        _head = _chickens[0];
        _bodyParts = new Queue<Chicken>(_chickens);
        _canMove = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && _canMove)
        {
            StartMove(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.A) && _canMove)
        {
            StartMove(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.W) && _canMove)
        {
            StartMove(Vector3.up);
        }

        if (Input.GetKeyDown(KeyCode.S) && _canMove)
        {
            StartMove(Vector3.down);
        }
    }

    private Chicken GetTailSnake() => _chickens[_chickens.Count - 1];

    private void StartMove(Vector3 direction)
    {
        bool isCollisionChicken = _head.CheckCollisionWithChicken(direction, _skill.MoveDistance);
        bool isCollisionGround = _head.CheckCollisionWithGround(direction, _skill.MoveDistance);

        if (isCollisionChicken || isCollisionGround)
        {
            return;
        }
        else
        {
            Vector3 toPosition = _head.transform.position + direction * _skill.MoveDistance;
            _canMove = false;
            OnMove(toPosition);
        }
    }

    private void OnMove(Vector3 toPosition)
    {
        if (_bodyParts.TryDequeue(out Chicken chicken))
        {
            chicken.Move(toPosition, _skill.SpeedMovement);
        }
        else
        {
            CheckGravity();
            _bodyParts = new Queue<Chicken>(_chickens);
        }
    }

    private void OnGrow(Chicken chicken)
    {
        chicken.Stopped += OnMove;
        chicken.Catched += OnGrow;
        Transform chickenTransform = chicken.GetComponent<Transform>();
        chickenTransform.parent = transform;
        chickenTransform.position = GetTailSnake().transform.position;
        _chickens.Add(chicken);
        ParticleSystem growEffect = Instantiate(_growEffect, chickenTransform.position, Quaternion.identity);
        growEffect.Play();
    }

    private void CheckGravity()
    {
        int chickensInAir = 0;

        foreach (Chicken chiken in _chickens)
        {
            if (chiken.CheckCollisionWithGround(Vector3.down, _skill.MoveDistance))
            {
                _canMove = true;
                chiken.Stay();
                continue;
            }
            else
            {
                chickensInAir++;
                chiken.Fly();

                if (_chickens.Count == chickensInAir)
                {
                    StartCoroutine(Falling());
                }              
            }
        }      
    }

    private IEnumerator Falling()
    {
        Vector3 toPosition = transform.position + Vector3.down * _skill.MoveDistance;
        float minFloatNumber = .00001f;

        while (Vector3.Distance(transform.position, toPosition) > minFloatNumber)
        {
            transform.position = Vector3.MoveTowards(transform.position, toPosition, _skill.SpeedFalling * Time.deltaTime);
            yield return null;
        }

        CheckGravity();
    }
}
