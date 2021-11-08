using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 _followDistanceVector = new Vector3(0, 0, 0);
    //private Vector3 _followAngle;
    // Start is called before the first frame update
    void Start()
    {
        //_followAngle = new Vector3(Mathf.Atan(Mathf.Abs(_followDistanceVector.y) / Mathf.Abs(_followDistanceVector.z)) * Mathf.Rad2Deg, 0, 0);
        //gameObject.transform.rotation = Quaternion.Euler(_followAngle);
        gameObject.transform.position = player.transform.position + _followDistanceVector;
        gameObject.transform.SetParent(player.transform);
    }
}
