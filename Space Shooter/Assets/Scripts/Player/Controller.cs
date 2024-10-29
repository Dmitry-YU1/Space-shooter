using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class controls player movement
/// </summary>
public class Controller : MonoBehaviour
{
    [Header("GameObject/Component References")]
    public RuntimeAnimatorController animator = null;
    public Rigidbody2D myRigidbody = null;

    [Header("Movement Variables")]
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 60f;

    private InputManager inputManager;

    public enum AimModes { AimTowardsMouse, AimForwards };
    public AimModes aimMode = AimModes.AimTowardsMouse;

    public enum MovementModes { MoveHorizontally, MoveVertically, FreeRoam, Astroids };
    public MovementModes movementMode = MovementModes.FreeRoam;

    private void Start()
    {
        SetupInput();
    }

    void Update()
    {
        HandleInput();
        SignalAnimator();
    }

    private void SetupInput()
    {
        if (inputManager == null)
        {
            inputManager = InputManager.instance;
        }
        if (inputManager == null)
        {
            Debug.LogWarning("No player input manager found!");
        }
    }

    private void HandleInput()
    {
        // Получаем ввод с клавиатуры
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(moveHorizontal, moveVertical, 0);

        // Двигаем игрока
        MovePlayer(movementVector);
        // Обновляем позицию для прицеливания
        Vector2 lookPosition = GetLookPosition();
        LookAtPoint(lookPosition);
    }

    private void SignalAnimator()
    {
        // Здесь можно добавить логику для анимации
    }

    public Vector2 GetLookPosition()
    {
        Vector2 result = transform.up;
        if (aimMode != AimModes.AimForwards)
        {
            result = new Vector2(inputManager.horizontalLookAxis, inputManager.verticalLookAxis);
        }
        return result;
    }

    private void MovePlayer(Vector3 movement)
    {
        if (movementMode == MovementModes.Astroids)
        {
            if (myRigidbody == null)
            {
                myRigidbody = GetComponent<Rigidbody2D>();
            }

            Vector2 force = transform.up * movement.y * Time.deltaTime * moveSpeed;
            myRigidbody.AddForce(force);

            float newZAxisRotation = transform.rotation.eulerAngles.z - rotationSpeed * movement.x * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, newZAxisRotation);
        }
        else
        {
            if (movementMode == MovementModes.MoveHorizontally)
            {
                movement.y = 0;
            }
            else if (movementMode == MovementModes.MoveVertically)
            {
                movement.x = 0;
            }

            transform.position += movement * Time.deltaTime * moveSpeed;
        }
    }



    private void LookAtPoint(Vector3 point)
    {
        if (Time.timeScale > 0)
        {
            Vector2 lookDirection = Camera.main.ScreenToWorldPoint(point) - transform.position;

            if (aimMode == AimModes.AimTowardsMouse)
            {
                transform.up = lookDirection;
            }
        }
    }
}
