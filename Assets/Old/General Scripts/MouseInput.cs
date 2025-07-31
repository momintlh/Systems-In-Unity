using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseInput : MonoBehaviour
{

    Camera mainCamera;

    Transform player;

    // public delegate EvenHand

    public delegate void MouseButton(bool isDown);

    public static event MouseButton OnLmbDown;
    public static event MouseButton OnLmbUp;

    public bool isMouseDown;

    void Awake()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Start()
    {
        isMouseDown = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isMouseDown = true;
            DetectObject();
        }
        else
        {
            isMouseDown = false;
            OnLmbUp?.Invoke(isMouseDown); 
        }
    }

    void DetectObject()
    {

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);


        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            OnLmbDown?.Invoke(isMouseDown);
            // invoke a event here, this time using UnityEvents instead of C#

        }
    }


}


