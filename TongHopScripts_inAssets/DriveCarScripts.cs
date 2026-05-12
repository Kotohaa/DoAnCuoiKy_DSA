using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DriveCarScripts : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _frontTireRB; //input: Rigidbody2D của bánh trước
    [SerializeField] private Rigidbody2D _backTireRB; //input: Rigidbody2D của bánh sau
    [SerializeField] private Rigidbody2D _carRb; //input: Rigidbody2D của thân xe
    [SerializeField] private float _speed = 150f;
    [SerializeField] private float _rotationSpeed = 150f;

    private float _moveInput;

    private void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
    }


    private void FixedUpdate()
    {
        _frontTireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
        _backTireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
        _carRb.AddTorque(-_moveInput * _rotationSpeed * Time.fixedDeltaTime);
    }
}
