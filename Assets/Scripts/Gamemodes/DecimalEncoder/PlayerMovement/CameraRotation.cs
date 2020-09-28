using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    private Vector3 _rotation;
    private Coroutine _moveRoutine;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Vector2 _xRange = new Vector2(-90f,90f );

    public FloatSO verticalSensitivity;
    float xRotation = 0f;



    public void Rotate(float direction)
    {
        
        float mouseY = direction * verticalSensitivity.value * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _xRange.x, _xRange.y);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
}
