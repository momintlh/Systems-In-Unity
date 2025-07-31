using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnityEvents : MonoBehaviour
{
    //Summary://
    /*
        The goal is to replicate working of Observer Pattern by using UnityEvents and delegates, 
        first learning unity events.


        Action = C#
        UnityAction = unitis is used with unity events


    */

    public static event Action MyAction;

    // Start is called before the first frame update
    private void Start()
    {


    }

    // Update is called once per frame
    private void Update()
    {
       
    }


    public static void SpaceDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if (MyAction != null)        
            // {
            //     MyAction.Invoke();
            // }

            MyAction?.Invoke();     // same as the above null check!
        }
    }
}
