using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerControls playerControls;
    static PlayerInput playerInput;
    static ControlDeviceType currentControlDevice;
    public enum ControlDeviceType
    {
        KeyboardAndMouse,
        Gamepad,
    }


    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerInput = GetComponent<PlayerInput>();
        //      SwitchActionMap(playerControls.Player);
    }

    private void OnEnable()
    {
        //playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnDisable()
    {
        //playerInput.onControlsChanged -= OnControlsChanged;
    }


    private void OnControlsChanged(PlayerInput obj)
    {
        if (obj.currentControlScheme == "Gamepad")
        {
            if (currentControlDevice != ControlDeviceType.Gamepad)
            {
                currentControlDevice = ControlDeviceType.Gamepad;
                Debug.Log(currentControlDevice);
            }
        }
        else
        {
            if (currentControlDevice != ControlDeviceType.KeyboardAndMouse)
            {
                currentControlDevice = ControlDeviceType.KeyboardAndMouse;
                Debug.Log(currentControlDevice);
            }
        }
    }



    public static void SwitchActionMap(InputActionMap actionMap)
    {

        Debug.Log("Antes: " + actionMap + " " + playerInput.currentActionMap.name);
        //playerControls.Disable(); // Desactivo todos los actionMaps
        playerInput.SwitchCurrentActionMap(actionMap.name);
        //actionMap.Enable();
        Debug.Log(actionMap + " " + playerInput.currentActionMap.name);


    }

    public static ControlDeviceType GetControlDeviceType()
    {
        return currentControlDevice;
    }

}
