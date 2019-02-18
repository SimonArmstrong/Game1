using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCam : MonoBehaviour {
    public float pixelsPerUnit = 32;
    public float zoom = 240f;
    public bool usePixelScale = false;
    public float pixelScale = 4f;

    public int screenXFactor = 16;
    public int screenYFactor = 9;

    float aspect;

    public LayerMask cameraBoundMask;

    Vector3 cameraPos = Vector3.zero;

    public void Move(Vector3 dir) {
        ApplyZoom(zoom);
        cameraPos += dir;
        AdjustCamera();
    }

    public void MoveTo(Vector3 pos) {
        ApplyZoom(zoom);
        cameraPos = pos;
        AdjustCamera();
    }

    public void AdjustCamera() {
        transform.position = new Vector3(
            RoundToNearestPixel(cameraPos.x),
            RoundToNearestPixel(cameraPos.y),
            -10f
        );
    }

    public float RoundToNearestPixel(float pos) {
        float screenPixelsPerUnit = Screen.height / (Camera.main.orthographicSize * 2f);
        float pixelValue = Mathf.Round(pos * screenPixelsPerUnit);

        return pixelValue / screenPixelsPerUnit;
    }

    public void ApplyZoom(float z) {
        zoom = z;
        if (!usePixelScale) {
            float smallestDimension = Screen.height < Screen.width ? Screen.height : Screen.width;
            pixelScale = Mathf.Clamp(Mathf.Round(smallestDimension / z), 1, 8);
        }

        Camera.main.orthographicSize = (Screen.height / (pixelsPerUnit * pixelScale)) * 0.5f;
    }

    public Vector3 GetCameraPos() {
        return cameraPos;
    }
}
