using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : AbstractCameraController
{
    private float StartTime;
    private float StartTime2;
    private bool PrevDirection;
    private Camera ManagedCamera;
    private bool FirstTime;
    private bool FinishedLerp = false;
    private bool RestartLerp = false;
    [SerializeField] public float LerpDuration;
    [SerializeField] public float LeadSpeed;

    private void Awake()
    {
        StartTime = Time.time;
        StartTime2 = 0;
        PrevDirection = false;
        this.ManagedCamera = this.gameObject.GetComponent<Camera>();
        FirstTime = true;
    }

    private void Update()
    {
        // if it's the first update, we need the camera to be at the target's default value
        if (FirstTime)
        {
            this.ManagedCamera.transform.position = new Vector3(this.Target.transform.position.x,
                this.Target.transform.position.y, this.ManagedCamera.transform.position.z);
            FirstTime = false;
        }
    }

    //Use the LateUpdate message to avoid setting the camera's position before
    //GameObject locations are finalized.
    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }
        var targetPosition = this.Target.transform.position;
        var player = this.Target.gameObject.GetComponent<MoveCharacter>();
        var playerMovementDirection = player.GetXDirection();
        var playerSpeed = player.GetSpeed();
        var cameraPosition = this.ManagedCamera.transform.position;

        // camera needs to be ahead of the player, so multiply where player's going to be with leadspeed
        var distx = Time.deltaTime * playerSpeed * LeadSpeed;

        // if player is going right, make the camera go to 5 ahead of player
        if (playerMovementDirection == false)
        {
            targetPosition = new Vector3(targetPosition.x + 5.0f, targetPosition.y, targetPosition.z);
            if (PrevDirection)
            {
                StartTime2 = Time.time;
            }
            PrevDirection = false;
        }
        else // if player is going left, negate distance X and make camera go to 5 behind player
        {
            targetPosition = new Vector3(targetPosition.x - 5.0f, targetPosition.y, targetPosition.z);
            distx = -distx;
            // if last direction was right, start the second timer
            if (!PrevDirection)
            {
                StartTime2 = Time.time;
            }
            PrevDirection = true;
        }

        var camPositionNext = new Vector3(cameraPosition.x + distx, cameraPosition.y, cameraPosition.z);

        //Debug.Log("Player speed: " + playerSpeed);

        // lerp finished and the player has stopped, so restart lerp when movement begins again
        if (FinishedLerp && playerSpeed == 0)
        {
            RestartLerp = true; // need to restart when player starts moving again
        }
        // movement has begun again, start lerping again
        else if (RestartLerp && playerSpeed > 0)
        {
            StartTime = Time.time;
            RestartLerp = false;
            FinishedLerp = false;
        }

        // lerp hasn't finished, so 'target' needs to 'catch up' to camera's position
        if (!FinishedLerp)
        {
            // finished lerping when player stops moving + lerpduration has passed
            if (Time.time - StartTime > LerpDuration && playerSpeed == 0)
            {
                //Debug.Log("Lerp just finished");
                this.ManagedCamera.transform.position = Vector3.Lerp(camPositionNext,
                    new Vector3(targetPosition.x, targetPosition.y, camPositionNext.z), 1);
                FinishedLerp = true;
                StartTime2 = 0;
            }
            // according to piazza, lerp should never quite catch up until player stops moving
            else if (Time.time - StartTime >= LerpDuration)
            {
                //Debug.Log("LerpDuration passed");
                this.ManagedCamera.transform.position = Vector3.Lerp(camPositionNext,
                    new Vector3(targetPosition.x, targetPosition.y, camPositionNext.z), 0.95f);
            }
            else
            {
                //Debug.Log("LerpDuration NOT passed");
                if (StartTime2 != 0)
                {
                    this.ManagedCamera.transform.position = Vector3.Lerp(camPositionNext,
                        new Vector3(targetPosition.x, targetPosition.y, camPositionNext.z),
                        (Time.time - StartTime2) / LerpDuration);
                }
                else
                {
                    this.ManagedCamera.transform.position = Vector3.Lerp(camPositionNext,
                        new Vector3(targetPosition.x, targetPosition.y, camPositionNext.z),
                        (Time.time - StartTime) / LerpDuration);
                }
            }
        }
        // lerp has finished, so player has caught up with camera's position
        else
        {
            this.ManagedCamera.transform.position = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);
        }

    }
}
