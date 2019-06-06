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
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            if (this.Target != null)
            {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = this.ManagedCamera.transform.position;
            cameraPosition = new Vector3(targetPosition.x + 5f, cameraPosition.y, cameraPosition.z);

            this.ManagedCamera.transform.position = cameraPosition;
        }
        }
    }

