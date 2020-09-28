using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KVelement : GridElement
{

    public int charge;
    public int setting;
    public TextMeshPro text;
    public bool highlighted;
    [SerializeField] private Sprite[] _sprites;



    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        return Vector2Int.zero;
    }
    public int Highlight(int setCharge, int negativeCharge)
    {

        if (((charge & setCharge) == setCharge || setCharge == 0) && ((charge & negativeCharge) ==0 || negativeCharge == 0))
        {
            if(setting == 1)
            {
                
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            highlighted = true;
            return setting;
        }
        return -1;
    }
    public void ResetHighlight()
    {
        highlighted = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void SetSetting()
    {
        if(setting == 1)
        {
            setting = 0;
        } else
        {
            setting = 1;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[setting];
        if (GUIManager.guiState)
        {
            text.text = setting.ToString();
        }
    }
}
