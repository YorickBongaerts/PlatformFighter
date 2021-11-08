using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFocusCamera : MonoBehaviour
{
    #region Variables
    
    public LevelFocus LevelFocus;

    public List<GameObject> players = new List<GameObject>();

    public float DepthUpdateSpeed = 5;
    public float AngleUpdateSpeed = 7;
    public float PositionUpdateSpeed = 5;

    public float DepthMax = -10;
    public float DepthMin = -22;

    public float AngleMax = 11;
    public float AngleMin = 3;

    private float CameraEulerX;
    private Vector3 CameraPosition;

    #endregion Variables

    #region Unity lifecycle
    
    void Start()
    {
        players.Add(LevelFocus.gameObject);    
    }

    void LateUpdate()
    {
        CalcCameraLocations();
        MoveCamera();
    }

    #endregion Unity lifecycle

    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if (position != CameraPosition)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, CameraPosition.x, PositionUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
            targetPosition.z = Mathf.MoveTowards(position.z, CameraPosition.z, DepthUpdateSpeed * Time.deltaTime);
            gameObject.transform.position = targetPosition;
        }

        Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
        if (localEulerAngles.x != CameraEulerX)
        {
            Vector3 targetEulerAngles = new Vector3(CameraEulerX, localEulerAngles.y, localEulerAngles.z);
            gameObject.transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, AngleUpdateSpeed * Time.deltaTime);
        }
    }
    private void CalcCameraLocations()
    {
        Vector3 averageCenter = Vector3.zero;
        Vector3 totalPositions = Vector3.zero;
        Bounds playerBounds = new Bounds();
        for (int i = 0; i < players.Count; i++)
        {
            Vector3 playerPosition = players[i].transform.position;

            if (!LevelFocus.focusBounds.Contains(playerPosition))
            {
                float playerX = Mathf.Clamp(playerPosition.x, LevelFocus.focusBounds.min.x, LevelFocus.focusBounds.max.x);
                float playerY = Mathf.Clamp(playerPosition.y, LevelFocus.focusBounds.min.y, LevelFocus.focusBounds.max.y);
                float playerZ = Mathf.Clamp(playerPosition.z, LevelFocus.focusBounds.min.z, LevelFocus.focusBounds.max.z);

                playerPosition = new Vector3(playerX, playerY, playerZ);
            }

            totalPositions += playerPosition;
            playerBounds.Encapsulate(playerPosition);
        }
        averageCenter = totalPositions / players.Count;

        float extents = (playerBounds.extents.x + playerBounds.extents.y);
        float lerpPercent = Mathf.InverseLerp(0, (LevelFocus.halfXBounds + LevelFocus.halfYBounds) / 2, extents);

        float depth = Mathf.Lerp(DepthMax, DepthMin, lerpPercent);
        float angle = Mathf.Lerp(AngleMax, AngleMin, lerpPercent);

        CameraEulerX = angle;
        CameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
    }
}
