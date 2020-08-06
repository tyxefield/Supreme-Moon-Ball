using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickSystem
{
    public GameObject handler;
    public TickControl control;
    public float ticks;


    public TickSystem()
    {
        handler = new GameObject("TickControl", typeof(TickControl));
        control = handler.GetComponent<TickControl>();
        control.script = this;
    }

    public class TickControl : MonoBehaviour
    {
        public TickSystem script;
        private float ticks;
        private float stepTicks;
        public float LIMIT_STEPTICKS;
        public bool atick;
        public bool aStepTick;

        private void Update()
        {
            atick = false;
            aStepTick = false;
            ticks += Time.deltaTime;
            stepTicks += Time.deltaTime;

            if (ticks > 1)
            {
                atick = true;
                ticks = 0;
            }

            if (stepTicks > LIMIT_STEPTICKS)
            {
                aStepTick = true;
                stepTicks -= LIMIT_STEPTICKS;
            }
        }
    }
}