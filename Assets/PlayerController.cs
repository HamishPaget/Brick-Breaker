using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputActionAsset actionAsset;

    InputAction moveAction;

    private void Awake()
    {
        moveAction = actionAsset.FindAction("Move");
    }

    private void Update()
    {
        if (moveAction.triggered)
        {
            Move(moveAction.ReadValue<Vector2>());
            Debug.Log("Test");
        }
    }

    
    public void Move(Vector2 val) {
        Vector3 pos = transform.position;

        pos.x += val.x * Time.deltaTime;

        transform.position = pos;
        
    }

}
