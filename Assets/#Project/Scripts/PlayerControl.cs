using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputActionAsset actionAsset;
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float turnSpeed = 180;
    [SerializeField] float sprintSpeed = 5;
    InputActionMap actions;
    InputAction move, sprint;
    LayerMask ground;
    
    void Awake()
    {
        actions = actionAsset.FindActionMap("Gameplay");
        move = actions.FindAction("Move");
        sprint = actions.FindAction("Sprint");
        ground = LayerMask.GetMask("Ground");
    }


    private void Update() {
        Move();
        Turn();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    private void Move() {
        Vector2 mouvement = move.ReadValue<Vector2>();
        transform.position += Time.deltaTime * (sprint.IsPressed() ?  sprintSpeed : walkSpeed) * new Vector3(mouvement.x, 0, mouvement.y);
    }
    private void Turn() { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ground)) {
            Vector3 targetPosition = hit.point - transform.position;
            Vector2 targetDirection = new Vector2(targetPosition.x, targetPosition.z);
            //targetDirection = (targetDirection - new Vector2(transform.position.x, transform.position.z)).normalized;
            Vector2 currentDirection = new Vector2(transform.forward.x, transform.forward.z).normalized;
            float angle = Vector2.SignedAngle(targetDirection, currentDirection);
            if (angle < 2 * turnSpeed * Time.deltaTime && angle > -2 * turnSpeed * Time.deltaTime) {
                transform.forward = new Vector3(targetDirection.x, 0, targetDirection.y).normalized;
            } else {

                transform.forward = new Vector3(currentDirection.x, 0, currentDirection.y).normalized;
                transform.Rotate((angle > 0 ? 1 : -1) * new Vector3(0, turnSpeed * Time.deltaTime, 0), Space.Self);
            }
        }
    }

    private void OnEnable() {
        actions?.Enable();
    }
    private void OnDisable() {
        actions?.Disable();
    }
}
