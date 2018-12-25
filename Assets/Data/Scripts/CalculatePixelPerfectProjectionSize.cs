using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePixelPerfectProjectionSize : MonoBehaviour {

    [System.Serializable]
    public struct Aspect {
        public int aspectWidth;
        public int aspectHeight;

        public Aspect(int x, int y) {
            aspectWidth = x;
            aspectHeight = y;
        }
    }

    public Aspect aspectRatio = new Aspect(16, 9);
    public RenderTexture renderTexture;

    public int pixelsPerUnit = 32;
    public int pixelScale = 1;

    public float height = 288;
    public Camera renderCamera;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //height = pixelsPerUnit * aspectRatio.aspectHeight;
        float screenPixelSize = (height / (pixelScale * pixelsPerUnit)) * 0.5f;
        GetComponent<Camera>().orthographicSize = screenPixelSize;
        //renderTexture = new RenderTexture(pixelsPerUnit * aspectRatio.aspectWidth, height, 2);
        
        renderCamera.orthographicSize += (Input.GetAxis("Mouse ScrollWheel") * Time.smoothDeltaTime * 50);

        if (renderCamera.orthographicSize >= screenPixelSize) {
            renderCamera.orthographicSize = screenPixelSize;
        }
        if (renderCamera.orthographicSize <= 1.05f) {
            renderCamera.orthographicSize = 1.05f;
        }
        //renderCamera.orthographicSize = Mathf.Clamp(renderCamera.orthographicSize, 1.05f, (height / (pixelScale * pixelsPerUnit)) * 0.5f);
    }
}
