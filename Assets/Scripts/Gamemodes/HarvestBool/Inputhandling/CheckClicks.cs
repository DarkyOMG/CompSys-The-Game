using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CheckClicks : MonoBehaviour
{
    // Normal raycasts do not work on UI elements, they require a special kind
    GraphicRaycaster raycaster;

    private InputMasterCompSys _inputs;
    [SerializeField] private GameObject _kVPanel;

    [SerializeField] private BooleanTermListSO _kVbooleanTerms;

    void Awake()
    {
        // Get both of the components we need to do this
        raycaster = GetComponent<GraphicRaycaster>();
        _inputs = new InputMasterCompSys();
        _inputs.KV.Enable();
        _inputs.KV.DeleteExpression.performed += Testitest;
    }
    private void OnDisable()
    {

        _inputs.KV.DeleteExpression.performed -= Testitest;
    }


    private void Testitest(InputAction.CallbackContext context)
    {

            //Set up the new Pointer Event
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Mouse.current.position.ReadValue();
            raycaster.Raycast(pointerData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {

            UIClickable tempClickable = result.gameObject.GetComponent<UIClickable>();
            if (tempClickable)
            {
                _kVbooleanTerms.booleanTerms.Remove(tempClickable._booleanTerm);
                UITableFiller.FillTable(_kVPanel, _kVbooleanTerms.booleanTerms);
            }
        }
        }
    
}