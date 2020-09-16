﻿using System;
using UnityEngine;
using System.Collections;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    [SerializeField] private Rigidbody2D playerRigidbody;

    private IMoveInput moveInput;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        if (playerRigidbody == null) {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }
        
        moveInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine) {
            VelocityMove(moveInput.MoveVector, moveSpeed);
            //Move(transform, moveInput.MoveVector, moveSpeed * Time.deltaTime);
        }
    }
    
    private void VelocityMove(Vector2 vec, float speed)
    {
        playerRigidbody.velocity = vec * speed;
    }
    private void Move(Transform player, Vector2 vec, float speed)
    {
        playerRigidbody.MovePosition((Vector2)player.position + vec * speed);
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = Mathf.Clamp(speed, 0.0f, float.PositiveInfinity);
    }
}