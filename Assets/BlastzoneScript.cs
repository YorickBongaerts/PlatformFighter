using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastzoneScript : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<BaseMovement>().ResetGravity();
            other.gameObject.transform.position = spawnPoint.transform.position;
        }
    }
}