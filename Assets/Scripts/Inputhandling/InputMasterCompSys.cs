// GENERATED AUTOMATICALLY FROM 'Assets/InputMasterCompSys.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMasterCompSys : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMasterCompSys()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMasterCompSys"",
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
                },
                {
                    ""name"": ""ChangeCharge"",
                    ""type"": ""Button"",
                    ""id"": ""c2db6660-da35-4fdc-bfcd-9344cf8b3d7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""afd0f769-c6cd-4630-bc74-ae93fbb632db"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""a7b78fc9-8559-4c8e-b757-3dd033b08af7"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5aebda11-8436-412a-b7f5-854d6a4e12be"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3581b864-4976-4f78-863b-0b4f4deef066"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""190d791b-0275-45db-bbe6-907a447541fb"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65240943-5974-4556-955b-3598d096b448"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d824dad2-7435-431d-b332-512694b5fbfc"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e26381a5-aaf8-4ad4-bd71-81e607e6ff4e"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1696082f-0185-41e0-9b30-95dcae900c71"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec089ebc-9220-44da-a49e-c3b93c6cac1a"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8de18271-8ab6-41d7-82bc-8058a0ecc950"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""KV"",
            ""id"": ""f894d65d-9d6f-4196-a78b-9516f4fe1e82"",
            ""actions"": [
                {
                    ""name"": ""SetElement"",
                    ""type"": ""Button"",
                    ""id"": ""823e313c-7e56-4515-ad63-182fc0c00156"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""71f95ed9-978a-4cf8-8374-abc36db78cf3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DeleteExpression"",
                    ""type"": ""Button"",
                    ""id"": ""ad5467bc-e0e8-4ae8-a0ec-707653fa0de0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6f6557ab-9dd1-42d4-8d3f-9955219097e7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SetElement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02a353e1-6706-4f35-9cea-6165605b7e43"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40154c16-8a86-40bd-8a08-aa758d5dca50"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeleteExpression"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Schaltnetz"",
            ""id"": ""0f71d72f-a192-4ffd-8f53-f327fa20c2dc"",
            ""actions"": [
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5bfd5cb2-6294-48aa-a98b-8c30d3e17915"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""c6fc845e-c719-4acf-853c-85607e9d014c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ClickAction"",
                    ""type"": ""Button"",
                    ""id"": ""2b1d61eb-467f-4a42-bca9-5c9e491c6719"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""b12e0216-4de6-44a2-82ba-39c0cf36ba44"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PreviewBuilding"",
                    ""type"": ""Value"",
                    ""id"": ""50a26398-d0b6-4b6d-af30-79d325628536"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Remove"",
                    ""type"": ""Button"",
                    ""id"": ""09bd2361-6f26-41be-b827-bcdf63051625"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""StopClickAction"",
                    ""type"": ""Button"",
                    ""id"": ""6da0bc6b-0ae6-4942-be68-086168692f1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""StopRemove"",
                    ""type"": ""Button"",
                    ""id"": ""0b9f4449-6f4a-44ba-9ad4-4c1df343f62a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f6e2e21b-fb1f-4259-a1d9-c3e0dc3e1d94"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""178933eb-8971-4b20-9108-334a74e8195f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MoveKeys"",
                    ""id"": ""20d4c0a5-5f85-489d-8589-99f91c052dbe"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f8cb474c-7a51-48dd-a43b-36ec438b2317"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""46fb00d2-1e4c-4615-9b16-5cfaa3edc544"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""07ebd631-6cfd-41de-926f-b52b45a6886c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""64df29ff-d73e-43be-bc6a-3c7917635475"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4ca5d879-9244-4de1-b931-6cfe05a8fab8"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""340c295f-5c62-4fc1-a17d-dc0209dac75b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviewBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""daac564f-05a2-44cd-95bd-e65fb52e3041"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Remove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f1d9ab2-6a62-4840-b5ef-9274f67898cf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopClickAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a255490-f6fd-4d1c-8cec-eb31fe379218"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopRemove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DecimalEncoder"",
            ""id"": ""fdf41eb7-9f96-436a-b521-06e31b690fc9"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""efdc593f-0b9b-4fb0-b0df-1663e00392c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""237a7d96-4d65-4753-8993-a93575b97e65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""6436456b-a145-4f1c-ace9-1934147d116d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Activate"",
                    ""type"": ""Button"",
                    ""id"": ""d2ae62e3-83cb-484b-b24e-eb53a5f4b2b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotatePlayer"",
                    ""type"": ""Value"",
                    ""id"": ""ed7ccbf3-8d9a-495b-abc0-ea2f5c79bfb1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7f32361e-5a1a-43ed-8b90-41d5ab30f3bc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MovementKeys"",
                    ""id"": ""f84502ec-92b9-43fd-a181-88b60f69b741"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9d6ecdca-e1de-4a67-a34b-68db6c0d4166"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e9464698-4d74-460e-b113-c468f5948c47"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8f16f0f1-b45f-4434-b429-d6be0660291e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dcc8855a-05dd-4177-882e-e452a0b214c5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""32dbc00b-ed00-4b84-8c4e-7f672d263807"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseAndKeyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e576a4e2-2807-4421-9428-172ce4acc62e"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseAndKeyboard"",
                    ""action"": ""RotatePlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c126ba7d-dde3-4c16-8140-4c2cb0779880"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
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
        m_Gatebuilder_ChangeCharge = m_Gatebuilder.FindAction("ChangeCharge", throwIfNotFound: true);
        m_Gatebuilder_Cancel = m_Gatebuilder.FindAction("Cancel", throwIfNotFound: true);
        // KV
        m_KV = asset.FindActionMap("KV", throwIfNotFound: true);
        m_KV_SetElement = m_KV.FindAction("SetElement", throwIfNotFound: true);
        m_KV_Cancel = m_KV.FindAction("Cancel", throwIfNotFound: true);
        m_KV_DeleteExpression = m_KV.FindAction("DeleteExpression", throwIfNotFound: true);
        // Schaltnetz
        m_Schaltnetz = asset.FindActionMap("Schaltnetz", throwIfNotFound: true);
        m_Schaltnetz_MoveCamera = m_Schaltnetz.FindAction("MoveCamera", throwIfNotFound: true);
        m_Schaltnetz_Cancel = m_Schaltnetz.FindAction("Cancel", throwIfNotFound: true);
        m_Schaltnetz_ClickAction = m_Schaltnetz.FindAction("ClickAction", throwIfNotFound: true);
        m_Schaltnetz_Scroll = m_Schaltnetz.FindAction("Scroll", throwIfNotFound: true);
        m_Schaltnetz_PreviewBuilding = m_Schaltnetz.FindAction("PreviewBuilding", throwIfNotFound: true);
        m_Schaltnetz_Remove = m_Schaltnetz.FindAction("Remove", throwIfNotFound: true);
        m_Schaltnetz_StopClickAction = m_Schaltnetz.FindAction("StopClickAction", throwIfNotFound: true);
        m_Schaltnetz_StopRemove = m_Schaltnetz.FindAction("StopRemove", throwIfNotFound: true);
        // DecimalEncoder
        m_DecimalEncoder = asset.FindActionMap("DecimalEncoder", throwIfNotFound: true);
        m_DecimalEncoder_Movement = m_DecimalEncoder.FindAction("Movement", throwIfNotFound: true);
        m_DecimalEncoder_Cancel = m_DecimalEncoder.FindAction("Cancel", throwIfNotFound: true);
        m_DecimalEncoder_MoveCamera = m_DecimalEncoder.FindAction("MoveCamera", throwIfNotFound: true);
        m_DecimalEncoder_Activate = m_DecimalEncoder.FindAction("Activate", throwIfNotFound: true);
        m_DecimalEncoder_RotatePlayer = m_DecimalEncoder.FindAction("RotatePlayer", throwIfNotFound: true);
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
    private readonly InputAction m_Gatebuilder_ChangeCharge;
    private readonly InputAction m_Gatebuilder_Cancel;
    public struct GatebuilderActions
    {
        private @InputMasterCompSys m_Wrapper;
        public GatebuilderActions(@InputMasterCompSys wrapper) { m_Wrapper = wrapper; }
        public InputAction @ClickAction => m_Wrapper.m_Gatebuilder_ClickAction;
        public InputAction @Rotate => m_Wrapper.m_Gatebuilder_Rotate;
        public InputAction @ChangeCharge => m_Wrapper.m_Gatebuilder_ChangeCharge;
        public InputAction @Cancel => m_Wrapper.m_Gatebuilder_Cancel;
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
                @ChangeCharge.started -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnChangeCharge;
                @ChangeCharge.performed -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnChangeCharge;
                @ChangeCharge.canceled -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnChangeCharge;
                @Cancel.started -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_GatebuilderActionsCallbackInterface.OnCancel;
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
                @ChangeCharge.started += instance.OnChangeCharge;
                @ChangeCharge.performed += instance.OnChangeCharge;
                @ChangeCharge.canceled += instance.OnChangeCharge;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public GatebuilderActions @Gatebuilder => new GatebuilderActions(this);

    // KV
    private readonly InputActionMap m_KV;
    private IKVActions m_KVActionsCallbackInterface;
    private readonly InputAction m_KV_SetElement;
    private readonly InputAction m_KV_Cancel;
    private readonly InputAction m_KV_DeleteExpression;
    public struct KVActions
    {
        private @InputMasterCompSys m_Wrapper;
        public KVActions(@InputMasterCompSys wrapper) { m_Wrapper = wrapper; }
        public InputAction @SetElement => m_Wrapper.m_KV_SetElement;
        public InputAction @Cancel => m_Wrapper.m_KV_Cancel;
        public InputAction @DeleteExpression => m_Wrapper.m_KV_DeleteExpression;
        public InputActionMap Get() { return m_Wrapper.m_KV; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KVActions set) { return set.Get(); }
        public void SetCallbacks(IKVActions instance)
        {
            if (m_Wrapper.m_KVActionsCallbackInterface != null)
            {
                @SetElement.started -= m_Wrapper.m_KVActionsCallbackInterface.OnSetElement;
                @SetElement.performed -= m_Wrapper.m_KVActionsCallbackInterface.OnSetElement;
                @SetElement.canceled -= m_Wrapper.m_KVActionsCallbackInterface.OnSetElement;
                @Cancel.started -= m_Wrapper.m_KVActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_KVActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_KVActionsCallbackInterface.OnCancel;
                @DeleteExpression.started -= m_Wrapper.m_KVActionsCallbackInterface.OnDeleteExpression;
                @DeleteExpression.performed -= m_Wrapper.m_KVActionsCallbackInterface.OnDeleteExpression;
                @DeleteExpression.canceled -= m_Wrapper.m_KVActionsCallbackInterface.OnDeleteExpression;
            }
            m_Wrapper.m_KVActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SetElement.started += instance.OnSetElement;
                @SetElement.performed += instance.OnSetElement;
                @SetElement.canceled += instance.OnSetElement;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @DeleteExpression.started += instance.OnDeleteExpression;
                @DeleteExpression.performed += instance.OnDeleteExpression;
                @DeleteExpression.canceled += instance.OnDeleteExpression;
            }
        }
    }
    public KVActions @KV => new KVActions(this);

    // Schaltnetz
    private readonly InputActionMap m_Schaltnetz;
    private ISchaltnetzActions m_SchaltnetzActionsCallbackInterface;
    private readonly InputAction m_Schaltnetz_MoveCamera;
    private readonly InputAction m_Schaltnetz_Cancel;
    private readonly InputAction m_Schaltnetz_ClickAction;
    private readonly InputAction m_Schaltnetz_Scroll;
    private readonly InputAction m_Schaltnetz_PreviewBuilding;
    private readonly InputAction m_Schaltnetz_Remove;
    private readonly InputAction m_Schaltnetz_StopClickAction;
    private readonly InputAction m_Schaltnetz_StopRemove;
    public struct SchaltnetzActions
    {
        private @InputMasterCompSys m_Wrapper;
        public SchaltnetzActions(@InputMasterCompSys wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCamera => m_Wrapper.m_Schaltnetz_MoveCamera;
        public InputAction @Cancel => m_Wrapper.m_Schaltnetz_Cancel;
        public InputAction @ClickAction => m_Wrapper.m_Schaltnetz_ClickAction;
        public InputAction @Scroll => m_Wrapper.m_Schaltnetz_Scroll;
        public InputAction @PreviewBuilding => m_Wrapper.m_Schaltnetz_PreviewBuilding;
        public InputAction @Remove => m_Wrapper.m_Schaltnetz_Remove;
        public InputAction @StopClickAction => m_Wrapper.m_Schaltnetz_StopClickAction;
        public InputAction @StopRemove => m_Wrapper.m_Schaltnetz_StopRemove;
        public InputActionMap Get() { return m_Wrapper.m_Schaltnetz; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SchaltnetzActions set) { return set.Get(); }
        public void SetCallbacks(ISchaltnetzActions instance)
        {
            if (m_Wrapper.m_SchaltnetzActionsCallbackInterface != null)
            {
                @MoveCamera.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnMoveCamera;
                @Cancel.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnCancel;
                @ClickAction.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnClickAction;
                @ClickAction.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnClickAction;
                @ClickAction.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnClickAction;
                @Scroll.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnScroll;
                @PreviewBuilding.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnPreviewBuilding;
                @PreviewBuilding.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnPreviewBuilding;
                @PreviewBuilding.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnPreviewBuilding;
                @Remove.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnRemove;
                @Remove.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnRemove;
                @Remove.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnRemove;
                @StopClickAction.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnStopClickAction;
                @StopClickAction.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnStopClickAction;
                @StopClickAction.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnStopClickAction;
                @StopRemove.started -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnStopRemove;
                @StopRemove.performed -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnStopRemove;
                @StopRemove.canceled -= m_Wrapper.m_SchaltnetzActionsCallbackInterface.OnStopRemove;
            }
            m_Wrapper.m_SchaltnetzActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @ClickAction.started += instance.OnClickAction;
                @ClickAction.performed += instance.OnClickAction;
                @ClickAction.canceled += instance.OnClickAction;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @PreviewBuilding.started += instance.OnPreviewBuilding;
                @PreviewBuilding.performed += instance.OnPreviewBuilding;
                @PreviewBuilding.canceled += instance.OnPreviewBuilding;
                @Remove.started += instance.OnRemove;
                @Remove.performed += instance.OnRemove;
                @Remove.canceled += instance.OnRemove;
                @StopClickAction.started += instance.OnStopClickAction;
                @StopClickAction.performed += instance.OnStopClickAction;
                @StopClickAction.canceled += instance.OnStopClickAction;
                @StopRemove.started += instance.OnStopRemove;
                @StopRemove.performed += instance.OnStopRemove;
                @StopRemove.canceled += instance.OnStopRemove;
            }
        }
    }
    public SchaltnetzActions @Schaltnetz => new SchaltnetzActions(this);

    // DecimalEncoder
    private readonly InputActionMap m_DecimalEncoder;
    private IDecimalEncoderActions m_DecimalEncoderActionsCallbackInterface;
    private readonly InputAction m_DecimalEncoder_Movement;
    private readonly InputAction m_DecimalEncoder_Cancel;
    private readonly InputAction m_DecimalEncoder_MoveCamera;
    private readonly InputAction m_DecimalEncoder_Activate;
    private readonly InputAction m_DecimalEncoder_RotatePlayer;
    public struct DecimalEncoderActions
    {
        private @InputMasterCompSys m_Wrapper;
        public DecimalEncoderActions(@InputMasterCompSys wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_DecimalEncoder_Movement;
        public InputAction @Cancel => m_Wrapper.m_DecimalEncoder_Cancel;
        public InputAction @MoveCamera => m_Wrapper.m_DecimalEncoder_MoveCamera;
        public InputAction @Activate => m_Wrapper.m_DecimalEncoder_Activate;
        public InputAction @RotatePlayer => m_Wrapper.m_DecimalEncoder_RotatePlayer;
        public InputActionMap Get() { return m_Wrapper.m_DecimalEncoder; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DecimalEncoderActions set) { return set.Get(); }
        public void SetCallbacks(IDecimalEncoderActions instance)
        {
            if (m_Wrapper.m_DecimalEncoderActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnMovement;
                @Cancel.started -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnCancel;
                @MoveCamera.started -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnMoveCamera;
                @Activate.started -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnActivate;
                @Activate.performed -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnActivate;
                @Activate.canceled -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnActivate;
                @RotatePlayer.started -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnRotatePlayer;
                @RotatePlayer.performed -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnRotatePlayer;
                @RotatePlayer.canceled -= m_Wrapper.m_DecimalEncoderActionsCallbackInterface.OnRotatePlayer;
            }
            m_Wrapper.m_DecimalEncoderActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @Activate.started += instance.OnActivate;
                @Activate.performed += instance.OnActivate;
                @Activate.canceled += instance.OnActivate;
                @RotatePlayer.started += instance.OnRotatePlayer;
                @RotatePlayer.performed += instance.OnRotatePlayer;
                @RotatePlayer.canceled += instance.OnRotatePlayer;
            }
        }
    }
    public DecimalEncoderActions @DecimalEncoder => new DecimalEncoderActions(this);
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
        void OnChangeCharge(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IKVActions
    {
        void OnSetElement(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnDeleteExpression(InputAction.CallbackContext context);
    }
    public interface ISchaltnetzActions
    {
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnClickAction(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnPreviewBuilding(InputAction.CallbackContext context);
        void OnRemove(InputAction.CallbackContext context);
        void OnStopClickAction(InputAction.CallbackContext context);
        void OnStopRemove(InputAction.CallbackContext context);
    }
    public interface IDecimalEncoderActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnActivate(InputAction.CallbackContext context);
        void OnRotatePlayer(InputAction.CallbackContext context);
    }
}
