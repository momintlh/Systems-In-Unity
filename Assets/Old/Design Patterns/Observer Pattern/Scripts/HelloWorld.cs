using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelloWorld : MonoBehaviour
{

    public TextMeshProUGUI defaultText;
    private int counter;

    void OnEnable()
    {
        UnityEvents.MyAction += IncrementCounter;
    }

    void OnDisable()
    {
        UnityEvents.MyAction -= IncrementCounter;
    }

    void Update()
    {
        UnityEvents.SpaceDown();
    }


    private void IncrementCounter()
    {
        counter++;
        defaultText.text = $"Count: {counter}";
    }

}
