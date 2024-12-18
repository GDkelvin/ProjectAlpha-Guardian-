﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputProvider : MonoBehaviour
{
    public Vector2 worldPosition { get; private set; }
    public event Action Clicked;
    

    private void OnLook(InputValue value)
    {
        worldPosition = (Vector2)Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        
    }

    private void OnAction(InputValue _)
    {
        Clicked?.Invoke();
    }
}
