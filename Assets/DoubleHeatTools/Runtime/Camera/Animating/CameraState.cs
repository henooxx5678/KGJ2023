using UnityEngine;

namespace DoubleHeat.Cameras.Animating {

    public struct CameraState {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public float fov;
        public float nearClipPlane;
        public float farClipPlane;


        public CameraState (Camera cam) {
            localPosition = cam.transform.localPosition;
            localRotation = cam.transform.localRotation;
            fov = cam.fieldOfView;
            nearClipPlane = cam.nearClipPlane;
            farClipPlane = cam.farClipPlane;
        }

        public CameraState (Vector3 localPos, Quaternion localRot, float fov, float nearClipPlane, float farClipPlane) {
            localPosition = localPos;
            localRotation = localRot;
            this.fov = fov;
            this.nearClipPlane = nearClipPlane;
            this.farClipPlane = farClipPlane;
        }


        public static CameraState Lerp (CameraState a, CameraState b, float t) {
            return new CameraState(
                Vector3.Lerp(a.localPosition, b.localPosition, t),
                Quaternion.Lerp(a.localRotation, b.localRotation, t),
                Mathf.Lerp(a.fov, b.fov, t),
                Mathf.Lerp(a.nearClipPlane, b.nearClipPlane, t),
                Mathf.Lerp(a.farClipPlane, b.farClipPlane, t)
            );
        }

        public void ApplyToCamera (Camera cam) {
            if (cam) {
                cam.transform.localPosition = localPosition;
                cam.transform.localRotation = localRotation;
                cam.fieldOfView = fov;
                cam.nearClipPlane = nearClipPlane;
                cam.farClipPlane = farClipPlane;
            }
        }

    }

}