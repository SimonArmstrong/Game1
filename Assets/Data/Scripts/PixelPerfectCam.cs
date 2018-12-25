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
        ApplyZoom();
        cameraPos += dir;
        AdjustCamera();
    }

    public void MoveTo(Vector3 pos) {
        ApplyZoom();
        cameraPos = pos;
        /*
        aspect = (float)Screen.width / (float)Screen.height;
        float w = Camera.main.orthographicSize * aspect * 2;
        float h = Camera.main.orthographicSize * 2;
        Vector3 x = transform.position + (new Vector3(-(w / 2), 0, 0));
        Vector3 y = transform.position + (new Vector3(0, -(h / 2), 0));
        RaycastHit2D xHit = Physics2D.Raycast(x, Vector3.right, w, cameraBoundMask);
        RaycastHit2D yHit = Physics2D.Raycast(y, Vector3.up, h, cameraBoundMask);

        Debug.DrawRay(x, Vector3.right * w);
        Debug.DrawRay(y, Vector3.up * h);

        if (xHit)
        {
            Vector3 hitPoint = xHit.point;
            Vector3 dir = hitPoint - transform.position;
            // Move(Vector3.ClampMagnitude(dir, dir.magnitude));
            // Debug.Log("Hit by X Effector");
        }
        if (yHit)
        {
            Vector3 hitPoint = yHit.point;
            Vector3 dir = hitPoint - transform.position;
            // Move(Vector3.ClampMagnitude(dir, dir.magnitude));
            // Debug.Log("Hit by Y Effector");
        }

        //if(xHit && yHit) 
        */
        AdjustCamera();
    }

    public void AdjustCamera() {
        Camera.main.transform.position = new Vector3(
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

    public void ApplyZoom() {
        if (!usePixelScale) {
            float smallestDimension = Screen.height < Screen.width ? Screen.height : Screen.width;
            pixelScale = Mathf.Clamp(Mathf.Round(smallestDimension / zoom), 1, 8);
        }

        Camera.main.orthographicSize = (Screen.height / (pixelsPerUnit * pixelScale)) * 0.5f;
    }

    public Vector3 GetCameraPos() {
        return cameraPos;
    }
}
