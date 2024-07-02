using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFlying : MonoBehaviour
{
    public float FlyTime = 1.0f;
    public float JumpForce = 800.0f;
    public void SendHeroFlying(GameObject gameObject)
    {
        StartCoroutine(gameObject.GetComponent<Hero>().SendFlyingUp(FlyTime, JumpForce));
    }
}
