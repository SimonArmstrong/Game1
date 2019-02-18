using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DunGen
{
    public class Door : MonoBehaviour
    {
        public RoomObject to;
        public RoomObject from;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Player>() != null)
            {
                GameObject cutscene = GameManager.instance.genericCutsceneObject;
                Cutscene go = Instantiate(cutscene).GetComponent<Cutscene>();
                go.cam.actor = Camera.main.gameObject;
                go.cam.positions.Add(transform);
                go.cam.transitionTimes.Add(1);
                go.cam.holdTimes.Add(.1f);
                go.cam.screenColors.Add(new Color(0, 0, 0, 1));
                go.fadeIn = true;

                go.handle.Add(from.OnRoomExit);
                go.handle.Add(to.OnRoomEnter);
            }
        }

    }
}