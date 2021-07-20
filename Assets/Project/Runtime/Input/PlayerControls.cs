// GENERATED AUTOMATICALLY FROM 'Assets/Project/Runtime/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gura"",
            ""id"": ""59f707e4-48a8-4444-aaef-5b25cd70d322"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c9f58870-adeb-4bea-b427-26358e03120e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9a0692e7-75e0-4f7c-869a-1a890fca3d3c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""9630bee7-ab4f-45e4-a22a-4c759f8c5173"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""e7ef6e88-270c-4f3e-818a-5b6425e8b880"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""a2cff26e-0000-487d-b5cb-84c77dc78ab2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbilityOne"",
                    ""type"": ""Button"",
                    ""id"": ""d65e5736-8a9b-43b5-8bc9-2910503434b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbilityTwo"",
                    ""type"": ""Button"",
                    ""id"": ""abf27b98-c293-4637-a344-8fb8d803e004"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""12f28e52-f4be-46ce-9c3d-11f308ee80ea"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""c29a4cbd-e74f-48c8-a114-f5e67f7dc07d"",
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
                    ""id"": ""d17aeee9-dd9d-470f-a896-e7ff1517b193"",
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
                    ""id"": ""aff309c1-87bd-47d3-b0f8-1b3256e83b79"",
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
                    ""id"": ""c1628e12-b1ef-4966-b0d7-348a8113f492"",
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
                    ""id"": ""eaf24289-3f9a-447e-9e96-9367b83e7785"",
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
                    ""id"": ""9a648ebe-6d9e-41a1-96ae-998bd759c0dd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16251501-5a32-4762-b2cb-02be4a16461f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2788a0f3-b1f8-4e82-96f6-1f6e943314ea"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""388b13b0-859b-469c-84da-a7ae847ecb99"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44bc6ec1-e306-48ad-a872-d0924793414f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e886b33f-b058-483c-a713-65170fbc43cb"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c008cd7-b052-4d2b-acd6-349289f6ad42"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8142112-efd6-4e5e-8139-b3d9ca0c7ee8"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3467765-a5bd-4357-ac75-e0181b5181fd"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bba5a62-1935-4624-bd9e-194eb44992e1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""378c3a4e-2da4-4a4f-b48e-7163200eb042"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f51f936c-bfd0-4252-b772-4a720b858fb4"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Bread"",
            ""id"": ""b159d4ee-9a61-47f0-9919-17b0fdae31ed"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""d5ad28c2-7d99-49f9-843b-a5e0a897e401"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1dda8056-2056-4927-a930-86aae4c3d745"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""24dfeae2-dc86-40b0-a1cb-0c1b686272ea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbilityTwo"",
                    ""type"": ""Button"",
                    ""id"": ""4167979e-a8e4-4660-bac9-d9e455655d04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbilityOne"",
                    ""type"": ""Button"",
                    ""id"": ""8fa446a1-c1b2-4a7f-8b86-0bbc979a6cd6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""13ec39a1-ff1f-4c3a-95c3-9ace02aa993e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""565a1e8f-74e8-4555-90bc-5fdfd9874108"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01d68c99-1f7c-4ae1-b4cc-333006f0e15f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""0b56b747-9919-4452-afc0-2bf835f0a015"",
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
                    ""id"": ""490f5ba5-b1ed-4d5f-a8fa-318ddf50fe52"",
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
                    ""id"": ""74e993b4-57eb-45f6-be5b-9d7327e9dcfe"",
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
                    ""id"": ""5e692d57-31bb-411a-8caa-15ce074e7405"",
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
                    ""id"": ""3941f816-de8b-4c3a-a592-884672084af0"",
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
                    ""id"": ""fec5d691-a87a-4a13-9155-f26214ee7872"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57c3756c-95c8-4bda-96f6-1c7716fef2c1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bd37969-5f28-427b-89e9-7e47ab747d35"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db083781-e91d-4e82-9a69-17cef5914cd2"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73905673-8c25-4838-b9c0-918c449b5242"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30b44de9-981f-4728-a14b-7f0fdebde570"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gura
        m_Gura = asset.FindActionMap("Gura", throwIfNotFound: true);
        m_Gura_Movement = m_Gura.FindAction("Movement", throwIfNotFound: true);
        m_Gura_Jump = m_Gura.FindAction("Jump", throwIfNotFound: true);
        m_Gura_Look = m_Gura.FindAction("Look", throwIfNotFound: true);
        m_Gura_Aim = m_Gura.FindAction("Aim", throwIfNotFound: true);
        m_Gura_Throw = m_Gura.FindAction("Throw", throwIfNotFound: true);
        m_Gura_AbilityOne = m_Gura.FindAction("AbilityOne", throwIfNotFound: true);
        m_Gura_AbilityTwo = m_Gura.FindAction("AbilityTwo", throwIfNotFound: true);
        // Bread
        m_Bread = asset.FindActionMap("Bread", throwIfNotFound: true);
        m_Bread_Movement = m_Bread.FindAction("Movement", throwIfNotFound: true);
        m_Bread_Jump = m_Bread.FindAction("Jump", throwIfNotFound: true);
        m_Bread_Look = m_Bread.FindAction("Look", throwIfNotFound: true);
        m_Bread_AbilityTwo = m_Bread.FindAction("AbilityTwo", throwIfNotFound: true);
        m_Bread_AbilityOne = m_Bread.FindAction("AbilityOne", throwIfNotFound: true);
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

    // Gura
    private readonly InputActionMap m_Gura;
    private IGuraActions m_GuraActionsCallbackInterface;
    private readonly InputAction m_Gura_Movement;
    private readonly InputAction m_Gura_Jump;
    private readonly InputAction m_Gura_Look;
    private readonly InputAction m_Gura_Aim;
    private readonly InputAction m_Gura_Throw;
    private readonly InputAction m_Gura_AbilityOne;
    private readonly InputAction m_Gura_AbilityTwo;
    public struct GuraActions
    {
        private @PlayerControls m_Wrapper;
        public GuraActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gura_Movement;
        public InputAction @Jump => m_Wrapper.m_Gura_Jump;
        public InputAction @Look => m_Wrapper.m_Gura_Look;
        public InputAction @Aim => m_Wrapper.m_Gura_Aim;
        public InputAction @Throw => m_Wrapper.m_Gura_Throw;
        public InputAction @AbilityOne => m_Wrapper.m_Gura_AbilityOne;
        public InputAction @AbilityTwo => m_Wrapper.m_Gura_AbilityTwo;
        public InputActionMap Get() { return m_Wrapper.m_Gura; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GuraActions set) { return set.Get(); }
        public void SetCallbacks(IGuraActions instance)
        {
            if (m_Wrapper.m_GuraActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GuraActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GuraActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GuraActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_GuraActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GuraActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GuraActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_GuraActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GuraActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GuraActionsCallbackInterface.OnLook;
                @Aim.started -= m_Wrapper.m_GuraActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GuraActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GuraActionsCallbackInterface.OnAim;
                @Throw.started -= m_Wrapper.m_GuraActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_GuraActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_GuraActionsCallbackInterface.OnThrow;
                @AbilityOne.started -= m_Wrapper.m_GuraActionsCallbackInterface.OnAbilityOne;
                @AbilityOne.performed -= m_Wrapper.m_GuraActionsCallbackInterface.OnAbilityOne;
                @AbilityOne.canceled -= m_Wrapper.m_GuraActionsCallbackInterface.OnAbilityOne;
                @AbilityTwo.started -= m_Wrapper.m_GuraActionsCallbackInterface.OnAbilityTwo;
                @AbilityTwo.performed -= m_Wrapper.m_GuraActionsCallbackInterface.OnAbilityTwo;
                @AbilityTwo.canceled -= m_Wrapper.m_GuraActionsCallbackInterface.OnAbilityTwo;
            }
            m_Wrapper.m_GuraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @AbilityOne.started += instance.OnAbilityOne;
                @AbilityOne.performed += instance.OnAbilityOne;
                @AbilityOne.canceled += instance.OnAbilityOne;
                @AbilityTwo.started += instance.OnAbilityTwo;
                @AbilityTwo.performed += instance.OnAbilityTwo;
                @AbilityTwo.canceled += instance.OnAbilityTwo;
            }
        }
    }
    public GuraActions @Gura => new GuraActions(this);

    // Bread
    private readonly InputActionMap m_Bread;
    private IBreadActions m_BreadActionsCallbackInterface;
    private readonly InputAction m_Bread_Movement;
    private readonly InputAction m_Bread_Jump;
    private readonly InputAction m_Bread_Look;
    private readonly InputAction m_Bread_AbilityTwo;
    private readonly InputAction m_Bread_AbilityOne;
    public struct BreadActions
    {
        private @PlayerControls m_Wrapper;
        public BreadActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Bread_Movement;
        public InputAction @Jump => m_Wrapper.m_Bread_Jump;
        public InputAction @Look => m_Wrapper.m_Bread_Look;
        public InputAction @AbilityTwo => m_Wrapper.m_Bread_AbilityTwo;
        public InputAction @AbilityOne => m_Wrapper.m_Bread_AbilityOne;
        public InputActionMap Get() { return m_Wrapper.m_Bread; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BreadActions set) { return set.Get(); }
        public void SetCallbacks(IBreadActions instance)
        {
            if (m_Wrapper.m_BreadActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_BreadActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_BreadActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_BreadActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_BreadActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_BreadActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_BreadActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_BreadActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_BreadActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_BreadActionsCallbackInterface.OnLook;
                @AbilityTwo.started -= m_Wrapper.m_BreadActionsCallbackInterface.OnAbilityTwo;
                @AbilityTwo.performed -= m_Wrapper.m_BreadActionsCallbackInterface.OnAbilityTwo;
                @AbilityTwo.canceled -= m_Wrapper.m_BreadActionsCallbackInterface.OnAbilityTwo;
                @AbilityOne.started -= m_Wrapper.m_BreadActionsCallbackInterface.OnAbilityOne;
                @AbilityOne.performed -= m_Wrapper.m_BreadActionsCallbackInterface.OnAbilityOne;
                @AbilityOne.canceled -= m_Wrapper.m_BreadActionsCallbackInterface.OnAbilityOne;
            }
            m_Wrapper.m_BreadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @AbilityTwo.started += instance.OnAbilityTwo;
                @AbilityTwo.performed += instance.OnAbilityTwo;
                @AbilityTwo.canceled += instance.OnAbilityTwo;
                @AbilityOne.started += instance.OnAbilityOne;
                @AbilityOne.performed += instance.OnAbilityOne;
                @AbilityOne.canceled += instance.OnAbilityOne;
            }
        }
    }
    public BreadActions @Bread => new BreadActions(this);
    public interface IGuraActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnAbilityOne(InputAction.CallbackContext context);
        void OnAbilityTwo(InputAction.CallbackContext context);
    }
    public interface IBreadActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnAbilityTwo(InputAction.CallbackContext context);
        void OnAbilityOne(InputAction.CallbackContext context);
    }
}
