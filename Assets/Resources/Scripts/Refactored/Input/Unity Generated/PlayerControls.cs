// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Scripts/Refactored/Input/Unity Generated/PlayerControls.inputactions'

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
            ""name"": ""Controller"",
            ""id"": ""7c4bb40d-014f-4e13-b42a-09695b899aba"",
            ""actions"": [
                {
                    ""name"": ""FreeCamMove"",
                    ""type"": ""Value"",
                    ""id"": ""1b9ece1f-b437-4b37-8865-52c345fc7b42"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CamRotate"",
                    ""type"": ""Value"",
                    ""id"": ""bdf14d27-4926-4ffb-839f-a7081d452d6c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CheckTile"",
                    ""type"": ""Button"",
                    ""id"": ""ccc308a8-3d25-4d14-9edb-9c284e4b16c1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ToggleFreeCam"",
                    ""type"": ""Button"",
                    ""id"": ""04b3aa08-d740-468b-a2b8-9b84beea2709"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ZoomIn"",
                    ""type"": ""Value"",
                    ""id"": ""00f8823c-c4f8-404b-a687-cdc89c1f974e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomOut"",
                    ""type"": ""Value"",
                    ""id"": ""448cdd3d-dad3-4edd-8bc6-9a3b58df5eef"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AcceptTurn"",
                    ""type"": ""Button"",
                    ""id"": ""5b18b95a-e2bd-448c-825a-164931df99a6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CancelTurn"",
                    ""type"": ""Button"",
                    ""id"": ""cd5e09a9-f25f-4f5b-8dc4-41f6f1751806"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CursorMove"",
                    ""type"": ""Value"",
                    ""id"": ""0fb4f359-b83f-4da5-8b43-0b0cbea2651b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""04b9e58b-5ef8-4822-b46f-6bd818c7b7d5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""09585489-137f-425e-8986-7ae79bd92fa5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCamMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""51e30afb-ec78-48c8-a8a0-ceca1379c3fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a5edad3a-bd44-40c1-bd9b-fb9a3a992714"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a0ba24c4-444d-450f-89bc-c566af6c9a5a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1466e396-d2d3-4203-a0c0-f7b6a1f37c0e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeCamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d6b0379b-6ef3-4c41-a0f8-628b6fc23e5b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""409dae2a-7686-401c-8e1c-c51faeaae2b8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamRotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5b91e633-3baf-4f68-a762-19651865db0f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c59de204-530e-4f0f-8683-bf9eadf33911"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fb559ea9-e27c-4cfa-99a2-09ef50f8dfb8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""59595a5d-4d2b-48f4-b767-18bdc851553f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""412825f5-6722-4354-9327-aecde395623e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CheckTile"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60c2fb5e-74e2-4ffa-a6d0-bdfd8ad9d7aa"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CheckTile"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b50ac988-132b-403f-9b08-af1ed90d212c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFreeCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97f56d54-251a-430b-9286-919a102cf6f7"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFreeCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""837b56a9-601d-48a7-a657-06fc9c7f14d6"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55ed51f0-f3bd-4de9-9e1b-8c8aa80a6a6a"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3749bd4-41cd-419b-9057-498bd07d5d1e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ac0765c-6af2-41e8-bc50-3a036ab394f2"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e188ec2-4232-4d55-b44e-13a39f5b0cef"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AcceptTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fb315ad-75a1-42e2-9ee0-6ffe2aea986e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AcceptTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""baecd984-bd42-4afd-99ca-8efde63a347f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ed6f0fc-e028-4873-9eb0-dc2b836c9858"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""9f6e6a9e-8652-4548-ab3f-df667ec7a6c6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""39d7ab11-d5a4-4fff-976e-e6368ddb1cd5"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8f7ec29d-a453-4526-b6f9-31d1a1662940"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fa4aa0a2-87e7-4ab2-b05e-a841a585bf3e"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""835cb2fe-fd15-4d14-a103-312552a5eb44"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""ed8f9a15-0722-42f1-81df-176c5317827e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b4e6672b-51f8-4092-b08a-b52be25fbb10"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""03c25f49-35c9-4e07-a6b8-239fe0703221"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c84af8b-2ac8-454d-8970-0ef10f799402"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0beaaefa-d539-4e3c-9f70-d25e4b1c9a6d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""2b992d94-8eb6-478c-a6d9-1e0c1ee1d5fd"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""4296b025-0071-42cd-b21b-dc45127b3407"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""7899fcff-81da-4dd5-a9ae-06712ac2ff8f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""478a039a-a55d-49c8-b2ff-466f9eac5cf0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Value"",
                    ""id"": ""ba90efed-45f4-4bd2-a2a9-0a8afa67efbe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""9d9453c4-5c90-4ed5-ba44-dedc876c2fd2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""8df0d79f-32d4-4596-affe-0096c61d7530"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""Value"",
                    ""id"": ""dcbea701-30e3-4c7a-99f4-a889edd7384f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Value"",
                    ""id"": ""83ae37d5-8798-40da-bcb5-de7986965818"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ddec41bb-fad6-412f-b448-3dfd177372c3"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""689f7fc1-8b14-4ca7-a494-0981f0e32f02"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceSelect"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3f972377-5ee1-4e2a-9995-78776ec310ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Stick"",
                    ""id"": ""3d4577ee-78a5-4c56-bb5c-8d3df3210967"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""50b8a55d-d21a-4c9c-970a-2fced6a55f70"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e1ea37cf-1794-4c0a-813f-3ad454b117d3"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""49e54099-baa6-42d3-a5c8-3da2788aea58"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""92c85bf5-7d92-4583-b3c7-1557a3c3c27d"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""76bacc21-692c-466e-b4fb-c0c0f0faeac7"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""1c9df8dd-e1d5-4724-8465-a00590e5a0bd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2f07b7cd-f2a0-4bd4-9022-1acf0180f57a"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a222dbbb-6b09-470b-9b56-8a1fe4d63877"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""92a7022b-e933-41e1-85e8-09185ef9c791"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4d1cb833-e498-4f35-bdd0-9701cf99f1f3"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7ea2d97c-cde0-49eb-a9df-82a5b5dfb9f9"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44f58db0-e181-469c-823a-97ac1bb7a661"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31aedcc7-a53a-4e2c-b472-04e0725d5887"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d05a327b-693a-40a9-84cc-c94c941b5e96"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""780c7a10-2a4f-479f-b786-34d5fb397eb9"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c21a221d-7c64-4d6d-b049-beae254c9998"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a0e42d7-1c8b-477b-a8f2-edd813b60876"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f03731b-432c-4a49-b68b-cd08086f0cd7"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf379523-86bf-4d1b-95b3-8ef2cc513699"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94a8c404-fbe1-4ca0-a120-58f381a6c38a"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2708fec-effd-48c6-9cf9-948a9087d9cd"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""701abadd-de6e-4cc0-aefc-ef40800209dc"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TrackedDeviceSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""88a2a868-d341-4c27-8fd6-d1ef27796c00"",
            ""actions"": [
                {
                    ""name"": ""Move Cursor"",
                    ""type"": ""Value"",
                    ""id"": ""548a5d91-4c25-4c3a-8058-f5c7c1c46e91"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Free Camera"",
                    ""type"": ""Value"",
                    ""id"": ""37046751-943d-4758-99df-ece70435bd5e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate Camera"",
                    ""type"": ""Value"",
                    ""id"": ""746bc51a-ba10-4921-b1b4-d6c5e219145c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle Free Camera"",
                    ""type"": ""Button"",
                    ""id"": ""24e7ba10-b3cc-4d11-8fe1-3655015628cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom In"",
                    ""type"": ""Value"",
                    ""id"": ""efd2570b-20f5-4fca-936e-416301c8fda4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom Out"",
                    ""type"": ""Value"",
                    ""id"": ""55def2c8-5169-4afb-903c-c86d952bb180"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause Menu"",
                    ""type"": ""Button"",
                    ""id"": ""96007b1a-d33e-4ae5-b31f-06da451ad700"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Check Unit Info"",
                    ""type"": ""Button"",
                    ""id"": ""e54342ff-f091-4bd0-8ec2-c27dd6dc8d94"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""93d51602-a520-492e-963d-21b3a4342c41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""aad2404a-263a-462b-a92f-beea43fc6894"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""13da5d94-46fc-48e0-849a-8cb9623a4198"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Cursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ec244697-95a7-4125-b4ef-d1d31c554bd6"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move Cursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dbce2fc4-147e-4d1b-9020-d390aa2ead44"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move Cursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d56f8bd2-9691-41d7-8a2d-09f70bd31a35"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move Cursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ea79c5ff-f964-4428-b7af-fc9027dc0850"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move Cursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c38c3709-d69c-48c2-9147-8a5b5777a181"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move Free Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf9e785d-a4dd-497b-a240-857675e5748f"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Rotate Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e0dbf18-6b19-4f6b-be2f-da8c235d1409"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Pause Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46bbee62-da9c-4afe-9a83-cda0fdd1a0a6"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Toggle Free Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c221ce3d-ac50-40b4-af69-660e81a44d88"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1eb0ecba-742d-4461-9aac-e32e3d1aa8d8"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Zoom In"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38b73d9c-975b-4879-9533-38486d7c9b95"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Zoom Out"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""448bbd1f-46fe-4f3b-82b6-56f63a83ed01"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b442e20-9ce8-4ab3-b78e-b7d6b309c88a"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Check Unit Info"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Empty Map"",
            ""id"": ""76d4db71-81f9-41bb-aeba-ccdc182916d7"",
            ""actions"": [
                {
                    ""name"": ""Empty Action"",
                    ""type"": ""Button"",
                    ""id"": ""2a26fe45-2c36-4e32-8e2b-47f33393da41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_FreeCamMove = m_Controller.FindAction("FreeCamMove", throwIfNotFound: true);
        m_Controller_CamRotate = m_Controller.FindAction("CamRotate", throwIfNotFound: true);
        m_Controller_CheckTile = m_Controller.FindAction("CheckTile", throwIfNotFound: true);
        m_Controller_ToggleFreeCam = m_Controller.FindAction("ToggleFreeCam", throwIfNotFound: true);
        m_Controller_ZoomIn = m_Controller.FindAction("ZoomIn", throwIfNotFound: true);
        m_Controller_ZoomOut = m_Controller.FindAction("ZoomOut", throwIfNotFound: true);
        m_Controller_AcceptTurn = m_Controller.FindAction("AcceptTurn", throwIfNotFound: true);
        m_Controller_CancelTurn = m_Controller.FindAction("CancelTurn", throwIfNotFound: true);
        m_Controller_CursorMove = m_Controller.FindAction("CursorMove", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
        m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
        m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
        m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
        m_UI_TrackedDevicePosition = m_UI.FindAction("TrackedDevicePosition", throwIfNotFound: true);
        m_UI_TrackedDeviceOrientation = m_UI.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
        m_UI_TrackedDeviceSelect = m_UI.FindAction("TrackedDeviceSelect", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_MoveCursor = m_Combat.FindAction("Move Cursor", throwIfNotFound: true);
        m_Combat_MoveFreeCamera = m_Combat.FindAction("Move Free Camera", throwIfNotFound: true);
        m_Combat_RotateCamera = m_Combat.FindAction("Rotate Camera", throwIfNotFound: true);
        m_Combat_ToggleFreeCamera = m_Combat.FindAction("Toggle Free Camera", throwIfNotFound: true);
        m_Combat_ZoomIn = m_Combat.FindAction("Zoom In", throwIfNotFound: true);
        m_Combat_ZoomOut = m_Combat.FindAction("Zoom Out", throwIfNotFound: true);
        m_Combat_PauseMenu = m_Combat.FindAction("Pause Menu", throwIfNotFound: true);
        m_Combat_CheckUnitInfo = m_Combat.FindAction("Check Unit Info", throwIfNotFound: true);
        m_Combat_Accept = m_Combat.FindAction("Accept", throwIfNotFound: true);
        m_Combat_Cancel = m_Combat.FindAction("Cancel", throwIfNotFound: true);
        // Empty Map
        m_EmptyMap = asset.FindActionMap("Empty Map", throwIfNotFound: true);
        m_EmptyMap_EmptyAction = m_EmptyMap.FindAction("Empty Action", throwIfNotFound: true);
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

    // Controller
    private readonly InputActionMap m_Controller;
    private IControllerActions m_ControllerActionsCallbackInterface;
    private readonly InputAction m_Controller_FreeCamMove;
    private readonly InputAction m_Controller_CamRotate;
    private readonly InputAction m_Controller_CheckTile;
    private readonly InputAction m_Controller_ToggleFreeCam;
    private readonly InputAction m_Controller_ZoomIn;
    private readonly InputAction m_Controller_ZoomOut;
    private readonly InputAction m_Controller_AcceptTurn;
    private readonly InputAction m_Controller_CancelTurn;
    private readonly InputAction m_Controller_CursorMove;
    public struct ControllerActions
    {
        private @PlayerControls m_Wrapper;
        public ControllerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @FreeCamMove => m_Wrapper.m_Controller_FreeCamMove;
        public InputAction @CamRotate => m_Wrapper.m_Controller_CamRotate;
        public InputAction @CheckTile => m_Wrapper.m_Controller_CheckTile;
        public InputAction @ToggleFreeCam => m_Wrapper.m_Controller_ToggleFreeCam;
        public InputAction @ZoomIn => m_Wrapper.m_Controller_ZoomIn;
        public InputAction @ZoomOut => m_Wrapper.m_Controller_ZoomOut;
        public InputAction @AcceptTurn => m_Wrapper.m_Controller_AcceptTurn;
        public InputAction @CancelTurn => m_Wrapper.m_Controller_CancelTurn;
        public InputAction @CursorMove => m_Wrapper.m_Controller_CursorMove;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void SetCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterface != null)
            {
                @FreeCamMove.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnFreeCamMove;
                @FreeCamMove.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnFreeCamMove;
                @FreeCamMove.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnFreeCamMove;
                @CamRotate.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCamRotate;
                @CamRotate.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCamRotate;
                @CamRotate.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCamRotate;
                @CheckTile.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCheckTile;
                @CheckTile.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCheckTile;
                @CheckTile.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCheckTile;
                @ToggleFreeCam.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnToggleFreeCam;
                @ToggleFreeCam.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnToggleFreeCam;
                @ToggleFreeCam.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnToggleFreeCam;
                @ZoomIn.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnZoomIn;
                @ZoomIn.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnZoomIn;
                @ZoomIn.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnZoomIn;
                @ZoomOut.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnZoomOut;
                @ZoomOut.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnZoomOut;
                @ZoomOut.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnZoomOut;
                @AcceptTurn.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAcceptTurn;
                @AcceptTurn.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAcceptTurn;
                @AcceptTurn.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAcceptTurn;
                @CancelTurn.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCancelTurn;
                @CancelTurn.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCancelTurn;
                @CancelTurn.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCancelTurn;
                @CursorMove.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCursorMove;
                @CursorMove.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCursorMove;
                @CursorMove.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnCursorMove;
            }
            m_Wrapper.m_ControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FreeCamMove.started += instance.OnFreeCamMove;
                @FreeCamMove.performed += instance.OnFreeCamMove;
                @FreeCamMove.canceled += instance.OnFreeCamMove;
                @CamRotate.started += instance.OnCamRotate;
                @CamRotate.performed += instance.OnCamRotate;
                @CamRotate.canceled += instance.OnCamRotate;
                @CheckTile.started += instance.OnCheckTile;
                @CheckTile.performed += instance.OnCheckTile;
                @CheckTile.canceled += instance.OnCheckTile;
                @ToggleFreeCam.started += instance.OnToggleFreeCam;
                @ToggleFreeCam.performed += instance.OnToggleFreeCam;
                @ToggleFreeCam.canceled += instance.OnToggleFreeCam;
                @ZoomIn.started += instance.OnZoomIn;
                @ZoomIn.performed += instance.OnZoomIn;
                @ZoomIn.canceled += instance.OnZoomIn;
                @ZoomOut.started += instance.OnZoomOut;
                @ZoomOut.performed += instance.OnZoomOut;
                @ZoomOut.canceled += instance.OnZoomOut;
                @AcceptTurn.started += instance.OnAcceptTurn;
                @AcceptTurn.performed += instance.OnAcceptTurn;
                @AcceptTurn.canceled += instance.OnAcceptTurn;
                @CancelTurn.started += instance.OnCancelTurn;
                @CancelTurn.performed += instance.OnCancelTurn;
                @CancelTurn.canceled += instance.OnCancelTurn;
                @CursorMove.started += instance.OnCursorMove;
                @CursorMove.performed += instance.OnCursorMove;
                @CursorMove.canceled += instance.OnCursorMove;
            }
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Navigate;
    private readonly InputAction m_UI_Submit;
    private readonly InputAction m_UI_Cancel;
    private readonly InputAction m_UI_Point;
    private readonly InputAction m_UI_Click;
    private readonly InputAction m_UI_ScrollWheel;
    private readonly InputAction m_UI_MiddleClick;
    private readonly InputAction m_UI_RightClick;
    private readonly InputAction m_UI_TrackedDevicePosition;
    private readonly InputAction m_UI_TrackedDeviceOrientation;
    private readonly InputAction m_UI_TrackedDeviceSelect;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputAction @Point => m_Wrapper.m_UI_Point;
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_UI_ScrollWheel;
        public InputAction @MiddleClick => m_Wrapper.m_UI_MiddleClick;
        public InputAction @RightClick => m_Wrapper.m_UI_RightClick;
        public InputAction @TrackedDevicePosition => m_Wrapper.m_UI_TrackedDevicePosition;
        public InputAction @TrackedDeviceOrientation => m_Wrapper.m_UI_TrackedDeviceOrientation;
        public InputAction @TrackedDeviceSelect => m_Wrapper.m_UI_TrackedDeviceSelect;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @ScrollWheel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @MiddleClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @RightClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @TrackedDevicePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceSelect.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceSelect;
                @TrackedDeviceSelect.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceSelect;
                @TrackedDeviceSelect.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceSelect;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceSelect.started += instance.OnTrackedDeviceSelect;
                @TrackedDeviceSelect.performed += instance.OnTrackedDeviceSelect;
                @TrackedDeviceSelect.canceled += instance.OnTrackedDeviceSelect;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_MoveCursor;
    private readonly InputAction m_Combat_MoveFreeCamera;
    private readonly InputAction m_Combat_RotateCamera;
    private readonly InputAction m_Combat_ToggleFreeCamera;
    private readonly InputAction m_Combat_ZoomIn;
    private readonly InputAction m_Combat_ZoomOut;
    private readonly InputAction m_Combat_PauseMenu;
    private readonly InputAction m_Combat_CheckUnitInfo;
    private readonly InputAction m_Combat_Accept;
    private readonly InputAction m_Combat_Cancel;
    public struct CombatActions
    {
        private @PlayerControls m_Wrapper;
        public CombatActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCursor => m_Wrapper.m_Combat_MoveCursor;
        public InputAction @MoveFreeCamera => m_Wrapper.m_Combat_MoveFreeCamera;
        public InputAction @RotateCamera => m_Wrapper.m_Combat_RotateCamera;
        public InputAction @ToggleFreeCamera => m_Wrapper.m_Combat_ToggleFreeCamera;
        public InputAction @ZoomIn => m_Wrapper.m_Combat_ZoomIn;
        public InputAction @ZoomOut => m_Wrapper.m_Combat_ZoomOut;
        public InputAction @PauseMenu => m_Wrapper.m_Combat_PauseMenu;
        public InputAction @CheckUnitInfo => m_Wrapper.m_Combat_CheckUnitInfo;
        public InputAction @Accept => m_Wrapper.m_Combat_Accept;
        public InputAction @Cancel => m_Wrapper.m_Combat_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @MoveCursor.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveCursor;
                @MoveFreeCamera.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveFreeCamera;
                @MoveFreeCamera.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveFreeCamera;
                @MoveFreeCamera.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveFreeCamera;
                @RotateCamera.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnRotateCamera;
                @ToggleFreeCamera.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnToggleFreeCamera;
                @ToggleFreeCamera.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnToggleFreeCamera;
                @ToggleFreeCamera.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnToggleFreeCamera;
                @ZoomIn.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoomIn;
                @ZoomIn.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoomIn;
                @ZoomIn.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoomIn;
                @ZoomOut.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoomOut;
                @ZoomOut.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoomOut;
                @ZoomOut.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoomOut;
                @PauseMenu.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPauseMenu;
                @CheckUnitInfo.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnCheckUnitInfo;
                @CheckUnitInfo.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnCheckUnitInfo;
                @CheckUnitInfo.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnCheckUnitInfo;
                @Accept.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAccept;
                @Cancel.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCursor.started += instance.OnMoveCursor;
                @MoveCursor.performed += instance.OnMoveCursor;
                @MoveCursor.canceled += instance.OnMoveCursor;
                @MoveFreeCamera.started += instance.OnMoveFreeCamera;
                @MoveFreeCamera.performed += instance.OnMoveFreeCamera;
                @MoveFreeCamera.canceled += instance.OnMoveFreeCamera;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @ToggleFreeCamera.started += instance.OnToggleFreeCamera;
                @ToggleFreeCamera.performed += instance.OnToggleFreeCamera;
                @ToggleFreeCamera.canceled += instance.OnToggleFreeCamera;
                @ZoomIn.started += instance.OnZoomIn;
                @ZoomIn.performed += instance.OnZoomIn;
                @ZoomIn.canceled += instance.OnZoomIn;
                @ZoomOut.started += instance.OnZoomOut;
                @ZoomOut.performed += instance.OnZoomOut;
                @ZoomOut.canceled += instance.OnZoomOut;
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
                @CheckUnitInfo.started += instance.OnCheckUnitInfo;
                @CheckUnitInfo.performed += instance.OnCheckUnitInfo;
                @CheckUnitInfo.canceled += instance.OnCheckUnitInfo;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);

    // Empty Map
    private readonly InputActionMap m_EmptyMap;
    private IEmptyMapActions m_EmptyMapActionsCallbackInterface;
    private readonly InputAction m_EmptyMap_EmptyAction;
    public struct EmptyMapActions
    {
        private @PlayerControls m_Wrapper;
        public EmptyMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @EmptyAction => m_Wrapper.m_EmptyMap_EmptyAction;
        public InputActionMap Get() { return m_Wrapper.m_EmptyMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EmptyMapActions set) { return set.Get(); }
        public void SetCallbacks(IEmptyMapActions instance)
        {
            if (m_Wrapper.m_EmptyMapActionsCallbackInterface != null)
            {
                @EmptyAction.started -= m_Wrapper.m_EmptyMapActionsCallbackInterface.OnEmptyAction;
                @EmptyAction.performed -= m_Wrapper.m_EmptyMapActionsCallbackInterface.OnEmptyAction;
                @EmptyAction.canceled -= m_Wrapper.m_EmptyMapActionsCallbackInterface.OnEmptyAction;
            }
            m_Wrapper.m_EmptyMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EmptyAction.started += instance.OnEmptyAction;
                @EmptyAction.performed += instance.OnEmptyAction;
                @EmptyAction.canceled += instance.OnEmptyAction;
            }
        }
    }
    public EmptyMapActions @EmptyMap => new EmptyMapActions(this);
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    public interface IControllerActions
    {
        void OnFreeCamMove(InputAction.CallbackContext context);
        void OnCamRotate(InputAction.CallbackContext context);
        void OnCheckTile(InputAction.CallbackContext context);
        void OnToggleFreeCam(InputAction.CallbackContext context);
        void OnZoomIn(InputAction.CallbackContext context);
        void OnZoomOut(InputAction.CallbackContext context);
        void OnAcceptTurn(InputAction.CallbackContext context);
        void OnCancelTurn(InputAction.CallbackContext context);
        void OnCursorMove(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnTrackedDevicePosition(InputAction.CallbackContext context);
        void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
        void OnTrackedDeviceSelect(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnMoveCursor(InputAction.CallbackContext context);
        void OnMoveFreeCamera(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnToggleFreeCamera(InputAction.CallbackContext context);
        void OnZoomIn(InputAction.CallbackContext context);
        void OnZoomOut(InputAction.CallbackContext context);
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnCheckUnitInfo(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IEmptyMapActions
    {
        void OnEmptyAction(InputAction.CallbackContext context);
    }
}
