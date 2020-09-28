using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupBlock : Interactable
{
    public TMP_Text text;
    public GameObjectSO selected;
    public GameObjectSO _mainCam;
    [SerializeField] private GameObject _pickupPrefab;
    private bool firstTime = true;
    public bool isConvertable = false;
    public bool isStuck = false;
    private void Start()
    {
        if (text)
        {

            text.text = DecimalEncoder.encode(value,20);
        }
    }

    public override void Activate()
    {
        if (isStuck)
        {
            return;
        }
        if (!selected.go)
        {
            selected.go = gameObject;
            if (firstTime && isConvertable)
            {
                _mainCam.go.GetComponent<DecimalManager>().SpawnCube(value, transform.position + new Vector3(0f, 2f, 0f));
                firstTime = false;
            }
            transform.position = new Vector3(-100, -100, -100);
        }
        
    }
}
