using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    float yRotation = 0f;
    public FloatSO horizontalSensitivity;
    public void Rotate(float xAxis)
    {
        float mouseX = xAxis * horizontalSensitivity.value * Time.deltaTime;

        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
