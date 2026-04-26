using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //public static PlayerInput instance {get; private set;}
    Player_Actions inputActions;

    public bool attack_down {get; private set;}
    public bool dodge_down {get; private set;}

    private InputAction input_attack;
    private InputAction input_dodge;

    void Awake()
    {
        //instance = this;
        inputActions = new Player_Actions();
        inputActions.Enable();
        input_attack = inputActions.Player.Attack;
        input_dodge = inputActions.Player.Dodge;
    }

    void OnDestroy()
    {
        if(inputActions == null){ return; }
        inputActions.Disable();
        inputActions.Dispose();
    }

    void Update()
    {
        attack_down = input_attack.WasPressedThisFrame();
        dodge_down = input_dodge.WasPressedThisFrame();
    }
}
