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

    public Transform quadTransform;
    public Aspect aspectRatio = new Aspect(16, 9);
    public RenderTexture renderTexture;

    public int pixelsPerUnit = 32;
    
    public Camera renderCamera;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        float pixelScale = Mathf.Clamp(Mathf.Round(Screen.height / GetComponent<PixelPerfectCam>().zoom), 1, 8);
        //GetComponent<Camera>().orthographicSize = screenPixelSize;
        float pixelWidth = (pixelsPerUnit * aspectRatio.aspectWidth);
        float pixelHeight = (pixelsPerUnit * aspectRatio.aspectHeight);
        RenderTexture rTex = new RenderTexture((int)(pixelWidth), (int)(pixelHeight), 2);
        renderTexture = rTex;
        quadTransform.localScale = new Vector3(aspectRatio.aspectWidth , aspectRatio.aspectHeight * pixelScale, 1);
        //renderCamera.orthographicSize = Mathf.Clamp(renderCamera.orthographicSize, 1.05f, (height / (pixelScale * pixelsPerUnit)) * 0.5f);
    }
}
