using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //! Component
    public static InputManager Instance { get; private set; } // To ensure that only one instance of this class exists and provide a global point (Singleton Instance)
    private PlayerAction playerInputAction;
    public event EventHandler OnJump;
    public PlayerAction PlayerInputAction => playerInputAction;

    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerAction();
    }

    private void Start()
    {
        playerInputAction.Player.Move.Enable();
        playerInputAction.Player.Climb.Enable();
    }

    private void OnEnable()
    {
        playerInputAction.Player.Jump.Enable();
        playerInputAction.Player.Jump.performed += Jump; // Subscribe event
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    public bool IsJumping()
    {
        return playerInputAction.Player.Jump.IsPressed();
    }

    public Vector2 GetInputVectorMove()
    {
        Vector2 inputVectorMove = playerInputAction.Player.Move.ReadValue<Vector2>();
        return inputVectorMove.normalized;
    }

    public Vector2 GetInputVectorClimb()
    {
        Vector2 inputVectorClimb = playerInputAction.Player.Climb.ReadValue<Vector2>();
        return inputVectorClimb.normalized;
    }
}
