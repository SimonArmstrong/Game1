using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    public Transform target;
    public float lerpSpeed = 10;
    public LayerMask cameraBoundLayer;

    private Vector3 offsetFromTarget;

    public bool shootRays = true;

    float minX, maxX;
    float minY, maxY;

    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(gameObject);
    }
    Vector3 targetPos;
    Vector3 desiredPos;
    Vector3 clampPos;
    float aspect;
    float w;
    float h;

    private void ShootRays() {

        Vector3 t = targetPos;

        float u = h / 2;
        float d = h / 2;
        float l = w / 2;
        float r = w / 2;

        RaycastHit2D pre_hitUp    = Physics2D.Raycast(transform.position, Vector2.up,    h / 2-0.2f, cameraBoundLayer);
        RaycastHit2D pre_hitDown  = Physics2D.Raycast(transform.position, Vector2.down,  h / 2-0.2f, cameraBoundLayer);
        RaycastHit2D pre_hitLeft  = Physics2D.Raycast(transform.position, Vector2.left,  w / 2-0.2f, cameraBoundLayer);
        RaycastHit2D pre_hitRight = Physics2D.Raycast(transform.position, Vector2.right, w / 2-0.2f, cameraBoundLayer);

        u = pre_hitUp    ? (t - (Vector3)pre_hitUp.point).magnitude    : (h / 2);
        d = pre_hitDown  ? (t - (Vector3)pre_hitDown.point).magnitude  : (h / 2);
        l = pre_hitLeft  ? (t - (Vector3)pre_hitLeft.point).magnitude  : (w / 2);
        r = pre_hitRight ? (t - (Vector3)pre_hitRight.point).magnitude : (w / 2);

        RaycastHit2D hitUp    = Physics2D.Raycast(t, Vector2.up,    u, cameraBoundLayer);
        RaycastHit2D hitDown  = Physics2D.Raycast(t, Vector2.down,  d, cameraBoundLayer);
        RaycastHit2D hitLeft  = Physics2D.Raycast(t, Vector2.left,  l, cameraBoundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(t, Vector2.right, r, cameraBoundLayer);

        maxY = hitUp    ? hitUp.point.y - t.y    :  h / 2;
        minY = hitDown  ? hitDown.point.y - t.y  : -h / 2;
        minX = hitLeft  ? hitLeft.point.x - t.x  : -w / 2;
        maxX = hitRight ? hitRight.point.x - t.x :  w / 2;

        //Debug.Log("maxY: " + maxY.ToString("0.00") + ", minY: " + minY.ToString("0.00") + ", h / 2 == " + h / 2);

        if ((hitLeft && hitRight))
        {
            maxX = (hitLeft.point.x - t.x) / 2;
            minX = (hitRight.point.x - t.x) / 2;
        }
        if ((hitUp && hitDown))
        {
            maxY = (hitUp.point.y - t.y) / 2;
            minY = (hitDown.point.y - t.y) / 2;
        }
    }

    float muteTime = 2;
    float muteTimer = 2;
    private void FixedUpdate()
    {
        if (target == null) return;
        targetPos = new Vector3(target.position.x, target.position.y, -10);
        //desiredPos = targetPos;

        aspect = (float)Screen.width / (float)Screen.height;
        w = Camera.main.orthographicSize * aspect * 2;
        h = Camera.main.orthographicSize * 2;

        //Debug.DrawRay(targetPos,          Vector3.up    * h / 2, Color.green);
        //Debug.DrawRay(targetPos,          Vector3.down  * h / 2, Color.green);
        //Debug.DrawRay(targetPos,          Vector3.left  * w / 2, Color.blue);
        //Debug.DrawRay(targetPos,          Vector3.right * w / 2, Color.blue);
        //Debug.DrawRay(transform.position, Vector3.up    * h / 2, Color.magenta);
        //Debug.DrawRay(transform.position, Vector3.down  * h / 2, Color.magenta);
        //Debug.DrawRay(transform.position, Vector3.left  * w / 2, Color.red);
        //Debug.DrawRay(transform.position, Vector3.right * w / 2, Color.red);
        shootRays = true;
        if (shootRays)
        {
            ShootRays();
            //targetPos += (transform.position - targetPos);
            Vector3 offsetPos = new Vector3((maxX + minX), (maxY + minY), 0);

            clampPos = targetPos + offsetPos;

            //Debug.DrawRay(clampPos, Vector3.up, Color.yellow);
            //Debug.DrawRay(clampPos, Vector3.down, Color.yellow);
            //Debug.DrawRay(clampPos, Vector3.left, Color.yellow);
            //Debug.DrawRay(clampPos, Vector3.right, Color.yellow);

            Vector3 totalPos = Vector3.Lerp(transform.position, clampPos, Time.deltaTime * lerpSpeed);

            //totalPos.x = targetPos + new Vector3((maxX + minX) / 2, (maxY + minY) / 2, 0);
            //totalPos = targetPos + new Vector3((maxX + minX) / 2, (maxY + minY) / 2, 0);
            GetComponent<PixelPerfectCam>().MoveTo(totalPos);
            //transform.position = totalPos;
        }
        else {
            GetComponent<PixelPerfectCam>().MoveTo(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpSpeed));
            muteTimer -= Time.deltaTime;
            if (muteTimer <= 0) {
                shootRays = true;
                muteTimer = muteTime;
            }
        }
    }
}
