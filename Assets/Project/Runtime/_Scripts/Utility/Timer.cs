using System;

namespace Project.Utility
{
    public class Timer
    {
        public Action OnTimerDone;

        private float duration;
        private float startTime;

        public Timer(float duration, float startTime)
        {
            this.duration = duration;
            this.startTime = startTime;
        }

        public void Tick(float currentTime)
        {
            if (currentTime - startTime <= duration) OnTimerDone?.Invoke();
        }
    }
}
