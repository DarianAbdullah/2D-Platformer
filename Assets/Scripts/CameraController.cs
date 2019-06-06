using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CameraController : AbstractCameraController
    {
        public Vector3 TopLeft;
        public Vector3 BottomRight;
        private Camera ManagedCamera;

        private void Awake()
        {
            this.ManagedCamera = this.gameObject.GetComponent<Camera>();
            TopLeft = new Vector3(-5, 5, 0);
            BottomRight = new Vector3(5, -5, 0);
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            if (this.Target != null)
            {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = this.ManagedCamera.transform.position;
            if (targetPosition.y >= cameraPosition.y + TopLeft.y)
            {
                cameraPosition = new Vector3(cameraPosition.x, targetPosition.y - TopLeft.y, cameraPosition.z);
            }
            if (targetPosition.y <= cameraPosition.y + BottomRight.y)
            {
                cameraPosition = new Vector3(cameraPosition.x, targetPosition.y - BottomRight.y, cameraPosition.z);
            }
            if (targetPosition.x >= cameraPosition.x + BottomRight.x)
            {
                cameraPosition = new Vector3(targetPosition.x - BottomRight.x, cameraPosition.y, cameraPosition.z);
            }
            if (targetPosition.x <= cameraPosition.x + TopLeft.x)
            {
                cameraPosition = new Vector3(targetPosition.x - TopLeft.x, cameraPosition.y, cameraPosition.z);
            }

            this.ManagedCamera.transform.position = cameraPosition;
        }
        }
    }

