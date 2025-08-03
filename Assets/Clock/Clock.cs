using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minHand;
    public Transform secHand;

    DateTime currentTime;

    void Start()
    {

    }

    void Update()
    {
        currentTime = DateTime.Now;
        Debug.Log("Current Time: " + currentTime.ToString("hh:mm:ss"));

        // Calculate rotation angles for each hand
        float hourAngle = (currentTime.Hour % 12 + currentTime.Minute / 60f) * 360f / 12f;
        float minuteAngle = (currentTime.Minute + currentTime.Second / 60f) * 360f / 60f;
        float secondAngle = currentTime.Second * 360f / 60f;

        // Apply rotations
        hourHand.localRotation = Quaternion.Euler(0, 0, -hourAngle + 90);
        minHand.localRotation = Quaternion.Euler(0, 0, -minuteAngle + 90);
        secHand.localRotation = Quaternion.Euler(0, 0, -secondAngle + 90);
    }
}
