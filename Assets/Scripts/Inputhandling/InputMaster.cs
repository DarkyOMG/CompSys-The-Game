// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Gatebuilder"",
            ""id"": ""2995f77b-7a0c-4895-8f32-116d5d38533f"",
            ""actions"": [
                {
                    ""name"": ""ClickAction"",
                    ""type"": ""Button"",
                    ""id"": ""2a39e7fc-34d1-4bdd-a407-244a95d4d79e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""13890079-2d66-4001-a26b-3147719ea03e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2aa330f9-6374-4ccf-af29-e5c75060e817"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c09f088-8cbb-4748-8092-0d321689eab8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseAndKeyboard"",
            ""bindingGroup"": ""MouseAndKeyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gatebuilder
        m_Gatebuilder = asset.FindActionMap("Gatebuilder", throwIfNotFound: true);
        m_Gatebuilder_ClickAction = m_Gatebuilder.FindAction("ClickAction", throwIfNotFound: true);
        m_Gatebuilder_Rotate = m_Gatebuilder.FindAction("Rotate", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gatebuilder
    private readonly InputActionMap m_Gatebuilder;
    private IGatebuilderActions m_GatebuilderActionsCallbackInterface;
    private readonly InputAction m_Gatebuilder_ClickAction;
    private readonly InputAction m_Gatebuilder_Rotate;
    public struct GatebuilderActions
    {
        private @InputMaster m_Wrapper;
        public GatebuilderActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ClickAction => m_Wrapper.m_Gatebuilder_ClickAction;
        public InputAction @Rotate => m_Wrapper.m_Gatebuilder_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_Gatebuilder; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GatebuilderActions set) { return set.Get(); }
        public void SetCallbacks(IGatebuilderActions instance)
        {
            if (m_Wrapper.m_GatebuilderActionsCallbackInterface != null)
            {
                @ClickAction.started -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnClickAction;
                @ClickAction.performed -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnClickAction;
                @ClickAction.canceled -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnClickAction;
                @Rotate.started -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_GatebuilderActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ClickAction.started += instance.OnClickAction;
                @ClickAction.performed += instance.OnClickAction;
                @ClickAction.canceled += instance.OnClickAction;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public GatebuilderActions @Gatebuilder => new GatebuilderActions(this);
    private int m_MouseAndKeyboardSchemeIndex = -1;
    public InputControlScheme MouseAndKeyboardScheme
    {
        get
        {
            if (m_MouseAndKeyboardSchemeIndex == -1) m_MouseAndKeyboardSchemeIndex = asset.FindControlSchemeIndex("MouseAndKeyboard");
            return asset.controlSchemes[m_MouseAndKeyboardSchemeIndex];
        }
    }
    public interface IGatebuilderActions
    {
        void OnClickAction(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
}
