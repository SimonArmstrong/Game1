using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    public Transform target;
    public float lerpSpeed = 10;
    public bool outOfBounds = false;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
    Vector3 targetPos;
    // Update is called once per frame
    void Update () {
        if (target == null) return;

        targetPos = outOfBounds ? transform.position : new Vector3(target.position.x, target.position.y, -10);
        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpSpeed)
        for (int x = 0; x < 2; x++) {
            for (int y = 0; y < 2; y++)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2((Screen.width / 2) * x, (Screen.height / 2) * y));
                Debug.DrawRay(ray.origin, ray.direction);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    if (hit.collider.tag == "CameraBound")
                    {
                        outOfBounds = true;
                        return;
                    }
                }
            }
        }
        outOfBounds = false;
    }

    private void FixedUpdate()
    {
        GetComponent<PixelPerfectCam>().MoveTo(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpSpeed));
    }
}
