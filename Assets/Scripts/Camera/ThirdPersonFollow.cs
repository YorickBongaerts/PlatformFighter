using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 _followDistanceVector = new Vector3(0,5,-5);
    void Start()
    {
        gameObject.transform.position = player.transform.position + _followDistanceVector;
        gameObject.transform.LookAt(player.transform);
        gameObject.transform.SetParent(player.transform);
    }
}
