using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    private Vector3 _move;
    private Coroutine _moveRoutine;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Vector2 _xRange = new Vector2(0, 500);
    [SerializeField] private Vector2 _yRange = new Vector2(0, 500);

    public void Move(Vector3 direction)
    {
        _move = direction;
        if(_moveRoutine == null)
        {
            _moveRoutine = StartCoroutine(MoveCameraCoroutine());
        }
    }

    IEnumerator MoveCameraCoroutine()
    {
        while (_move != Vector3.zero)
        {
            Vector3 temp = transform.position + (_move * Time.deltaTime * _speed);
            temp.x = Mathf.Clamp(temp.x, _xRange.x, _xRange.y);
            temp.y = Mathf.Clamp(temp.y, _yRange.x, _yRange.y);
            transform.position = temp;
            yield return null;
        }
        _moveRoutine = null;
    }

}
