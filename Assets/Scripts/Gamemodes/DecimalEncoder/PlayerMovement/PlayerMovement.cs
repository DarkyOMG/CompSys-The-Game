using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _move;
    private Coroutine _moveRoutine;
    [SerializeField] private float _speed = 1;
    private CharacterController _charControl;

    private void Awake()
    {
        _charControl = GetComponent<CharacterController>();
    }
    public void Move(Vector2 direction)
    {
        _move = direction;
        if (_moveRoutine == null)
        {
            _moveRoutine = StartCoroutine(MoveCameraCoroutine());
        }
    }

    IEnumerator MoveCameraCoroutine()
    {
        while (_move != Vector2.zero)
        {
            Vector3 temp = transform.right * _move.x + transform.forward * _move.y;
            temp.y = -0.1f;
            _charControl.Move(temp * _speed * Time.deltaTime);
            yield return null;
        }
        _moveRoutine = null;
    }
}
