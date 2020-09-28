using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Converter : Interactable
{

    public TMP_Text text;
    [SerializeField] private GameObjectSO _selected;
    [SerializeField] private GameObjectSO _UIManager;
    [SerializeField] private IntSO _base;
    [SerializeField] private GameObject _sphere;
    private GameObject _block;
    private Coroutine particleRoutine = null;
    private void Start()
    {
        text.text = value.ToString();
    }
    public override void Activate()
    {
        if (_selected.go && particleRoutine == null && _selected.go.GetComponent<PickupBlock>().isConvertable)
        {
            _block = _selected.go;
            _selected.go.transform.position = transform.position + new Vector3(0f, 2f, 0f);
            _selected.go.transform.Rotate(0f, 180f, 0f);
            _selected.go = null;
            _UIManager.go.GetComponent<UIManager>().UpdateUI();
            particleRoutine = StartCoroutine(CreateSphere());
        }
        
    }
    IEnumerator CreateSphere()
    {
        int temp = (int)(_block.GetComponent<PickupBlock>().value * Mathf.Pow(_base.value, value));
        _block.GetComponent<PickupBlock>().isStuck = true;
        gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(4);
        gameObject.GetComponent<ParticleSystem>().Stop();
        Destroy(_block);
        if(temp > 0)
        {
            GameObject tempObj = Instantiate(_sphere, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            tempObj.GetComponent<PickupBlock>().value = temp;
        }
        particleRoutine = null;
    }
}
