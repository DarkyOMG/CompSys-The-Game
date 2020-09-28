using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guest : Interactable
{
    [SerializeField] private GameObjectSO _mainCam;
    [SerializeField] private GameObjectSO _selected;
    [SerializeField] private GameObjectSO _uiManager;
    [SerializeField] private IntSO _currentbase;
    [SerializeField] private IntSO _currentNumber;
    [SerializeField] private GameObjectSO _audioSFX;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private TMP_Text _numberText;
    public int numberBase;

    private void Start()
    {
        _mainCam.go.GetComponent<DecimalManager>().AddGuest(this.gameObject);
        value =_mainCam.go.GetComponent<DecimalManager>().GetNumber();
        numberBase = _mainCam.go.GetComponent<DecimalManager>().GetBase();
        _numberText.text = DecimalEncoder.encode(value, numberBase)+" ("+numberBase+")";
    }
    public override void Activate()
    {
        if (_selected.go)
        {
            if(_selected.go.GetComponent<PickupBlock>().value == this.value)
            {
                _audioSFX.go.GetComponent<AudioSource>().clip = _clips[0];

                _audioSFX.go.GetComponent<AudioSource>().Stop();
                _audioSFX.go.GetComponent<AudioSource>().Play();
                _mainCam.go.GetComponent<DecimalManager>().RemoveGuest(this.gameObject);
                Destroy(this.gameObject, _clips[0].length);

            }else
            {

                _audioSFX.go.GetComponent<AudioSource>().clip = _clips[1];

                _audioSFX.go.GetComponent<AudioSource>().Stop();
                _audioSFX.go.GetComponent<AudioSource>().Play();
            }
            _selected.go = null;

            _uiManager.go.GetComponent<UIManager>().UpdateUI();
        }
        if(_currentNumber.value != value || _currentbase.value != numberBase)
        {
            _currentNumber.value = value;
            _currentbase.value = numberBase;
            _uiManager.go.GetComponent<UIManager>().UpdateUI();
        }
    }
}
