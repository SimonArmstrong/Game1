using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Slime : Enemy
    {
        public override void FixedUpdate()
        {
            dirVec = (GameManager.instance.player.transform.position - transform.position).normalized;
            base.FixedUpdate();
        }
    }
}