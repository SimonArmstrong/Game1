using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Direction {
    forward,
    right,
    back,
    left
}

public class Model : NetworkBehaviour {
    //public TAnim animation;
    public TinkerAnimator baseModel;
    public TinkerAnimator hairModel;
    public TinkerAnimator eyesModel;
    public TinkerAnimator feetModel;
    public TinkerAnimator torsoModel;
    public TinkerAnimator bottomsModel;
    public TinkerAnimator weapon1Model;
    public TinkerAnimator weapon2Model;

    public Direction direction;
    public int frameIndex;
    public float timer;
    public float timeBetweenFrames = 0.2f;

    public float animSpeedModifier = 1;

    public int sortingOrder;

    public void Attack() {

    }

    public void CalculateDirection(Vector3 dir) {
        float dot1 = Vector3.Dot(dir, Vector3.up);      // 0 if either left or right, 1 if up, -1 if down
        float dot2 = Vector3.Dot(dir, Vector3.right);   // 1 if right, -1 if left, 0 if up or down

        Vector2 dot = new Vector2();

        if (Mathf.Abs(dot1) > Mathf.Abs(dot2)) dot = new Vector2(0, Mathf.RoundToInt(dot1));
        if (Mathf.Abs(dot1) <= Mathf.Abs(dot2)) dot = new Vector2(Mathf.RoundToInt(dot2), 0);

        if (dot == Vector2.left)
        {
            SwitchFrames();
            direction = Direction.left;
        }
        if (dot == Vector2.right)
        {
            SwitchFrames();
            direction = Direction.right;
        }
        if (dot == Vector2.down)
        {
            SwitchFrames();
            direction = Direction.forward;
        }
        if (dot == Vector2.up)
        {
            SwitchFrames();
            direction = Direction.back;
        }

        UpdateDirection();
    }

    public void UpdateDirection() {
        if (baseModel != null) { baseModel.dir = (int)direction;
            baseModel.GetComponent<SpriteRenderer>().sortingOrder = 0 + sortingOrder;
        }
        if (hairModel != null) { hairModel.dir = (int)direction;
            hairModel.GetComponent<SpriteRenderer>().sortingOrder = 2 + sortingOrder;
        }
        if (eyesModel != null) { eyesModel.dir = (int)direction;
            eyesModel.GetComponent<SpriteRenderer>().sortingOrder = 2 + sortingOrder;
        }
        if (feetModel != null) { feetModel.dir = (int)direction;
            feetModel.GetComponent<SpriteRenderer>().sortingOrder = 1 + sortingOrder;
        }
        if (torsoModel != null) { torsoModel.dir = (int)direction;
            torsoModel.GetComponent<SpriteRenderer>().sortingOrder = 3 + sortingOrder;
        }
        if (bottomsModel != null) { bottomsModel.dir = (int)direction;
            bottomsModel.GetComponent<SpriteRenderer>().sortingOrder = 2 + sortingOrder;
        }
        if (weapon1Model != null) { weapon1Model.dir = (int)direction; }
        if (weapon2Model != null) { weapon2Model.dir = (int)direction; }

    }

    public void ChangeAnimation(int animID) {
        if (baseModel != null) baseModel.currentAnimation = animID;
        if (hairModel != null) hairModel.currentAnimation = animID;
        if (eyesModel != null) eyesModel.currentAnimation = animID;
        if (feetModel != null) feetModel.currentAnimation = animID;
        if (torsoModel != null) torsoModel.currentAnimation = animID;
        if (bottomsModel != null) bottomsModel.currentAnimation = animID;
        if (weapon1Model != null) weapon1Model.currentAnimation = animID;
        if (weapon2Model != null) weapon2Model.currentAnimation = animID;
    }

    public void SwitchFrames() {
        if (baseModel != null) baseModel.SwitchFrames(ref frameIndex);
        //if (hairModel != null) hairModel.SwitchFrames(ref frameIndex);
        if (eyesModel != null) eyesModel.SwitchFrames(ref frameIndex);
        if (feetModel != null) feetModel.SwitchFrames(ref frameIndex);
        if (torsoModel != null) torsoModel.SwitchFrames(ref frameIndex);
        if (bottomsModel != null) bottomsModel.SwitchFrames(ref frameIndex);
        if (weapon1Model != null) weapon1Model.SwitchFrames(ref frameIndex);
        //if (weapon2Model != null) weapon2Model.SwitchFrames(ref frameIndex);
    }

    public void UpdateFrames() {
        timer -= Time.deltaTime * animSpeedModifier;
        if (timer <= 0)
        {
            frameIndex++;
            SwitchFrames();
            timer = timeBetweenFrames;
        }
    }
}
