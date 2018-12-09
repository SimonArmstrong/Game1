using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTCondition : StatusCondition {
    public float tick;

    private float tickTimer;

    public override void Start() {
        base.Start();
        tickTimer = tick;
    }

    public override void Calculate() {
        base.Calculate();
        //do damage to entity's stats
        
    }

    public override void Update() {
        base.Update();
        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0) {
            tickTimer = tick;
            Calculate();
        }
       
    }
}
