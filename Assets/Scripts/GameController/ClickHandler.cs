using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent clicked;
    private MouseInputProvider mouse;
    private GameController gameController;
    private BoxCollider2D _collider;
    private MeleeMinionStats minionStats;

    private void Awake()
    {
        mouse = FindObjectOfType<MouseInputProvider>();
        mouse.Clicked += MouseOnClicked;
        _collider = GetComponent<BoxCollider2D>();
        gameController = FindObjectOfType<GameController>();
        minionStats = GetComponent<MeleeMinionStats>();
    }

    private void MouseOnClicked()
    {
        if (_collider.bounds.Contains(mouse.worldPosition))
        {
            Debug.Log("Clicked on: " + gameObject.name);
            clicked?.Invoke();
            
        }
        else
        {
            Debug.Log("Clicked outside: " + gameObject.name);
            
        }
        
    }

}
