using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TimeCounter
{
    public class Timer : MonoBehaviour
    {
        Action timerCallBack;

        [SerializeField] float _timer;
        public float GetTimer { get { return _timer; } }

        public void CountDown()
        {
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;

                if (TimerIsComplete())
                {
                    timerCallBack?.Invoke();
                }
            }
        }

        public void SetTimer(float timer, Action timerCallBack)
        {
            this._timer = timer;
            this.timerCallBack = timerCallBack;
        }

        public bool TimerIsComplete()
        {
            return _timer <= 0f;
        }
    }
}
