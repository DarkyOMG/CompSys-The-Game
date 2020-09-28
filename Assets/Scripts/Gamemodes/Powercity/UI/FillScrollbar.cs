using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillScrollbar : MonoBehaviour
{
    Object[] prefabs;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _contentGo;

    private void Start()
    {
        prefabs = Resources.LoadAll("Prefabs\\Schaltnetze", typeof(GameObject));
        foreach (GameObject temp in prefabs)
        {
            MultiCellElement tempObj = temp.GetComponent<MultiCellElement>();

            if(tempObj && !tempObj.Locked)
            {
                GameObject tempButton = Instantiate(_button, _contentGo.transform);
                tempButton.GetComponent<Button>().onClick.AddListener(delegate { InputHandler.instance.SelectNewElement(temp); });
                tempButton.GetComponent<Button>().onClick.AddListener(delegate { InputHandler.instance.FillSelectionResultTable(); });
                TMP_Text text = tempButton.GetComponentInChildren<TMP_Text>();
                text.text = temp.name;
            }
        }
    }
}
