using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTargetting : MonoBehaviour
{
    [SerializeField] GameObjectSO _tempgoSO;
    private void Update()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(transform.position, transform.forward * 5);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (_tempgoSO.go && _tempgoSO.go != hit.collider.gameObject && _tempgoSO.go.GetComponent<Interactable>().highlighted)
            {
                _tempgoSO.go.GetComponent<Interactable>().Highlight();
                _tempgoSO.go = null;
            }
            Interactable tempInter = hit.collider.GetComponent<Interactable>();
            if (tempInter)
            {
                if (!tempInter.highlighted)
                {
                    tempInter.Highlight();
                    _tempgoSO.go = hit.collider.gameObject;
                }
            }

        }
        else
        if (_tempgoSO.go && _tempgoSO.go.GetComponent<Interactable>().highlighted)
        {
            _tempgoSO.go.GetComponent<Interactable>().Highlight();
            _tempgoSO.go = null;
        }
    }



}
