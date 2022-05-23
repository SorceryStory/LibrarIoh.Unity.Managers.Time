using UnityEngine;

namespace SorceressSpell.LibrarIoh.Unity.Managers.Time
{
    public class TimeManager : MonoBehaviour
    {
        #region Fields

        public OnTimeScaleChanged OnTimeScaleChanged;

        private float _baseFixedDeltaTime;
        private int _framesToFreeze;
        private float _timeScaleBeforeFreeze;

        #endregion Fields

        #region Methods

        public void FreezeFrames(int frames)
        {
            if (frames > 0)
            {
                _framesToFreeze += frames;

                if (UnityEngine.Time.timeScale > 0f)
                {
                    _timeScaleBeforeFreeze = UnityEngine.Time.timeScale;

                    SetTimeScale(0f);
                }
            }
        }

        public void SetTimeScale(float timeScale)
        {
            UnityEngine.Time.timeScale = timeScale;
            UnityEngine.Time.fixedDeltaTime = _baseFixedDeltaTime * timeScale;

            OnTimeScaleChanged?.Invoke(timeScale);
        }

        private void Awake()
        {
            _baseFixedDeltaTime = UnityEngine.Time.fixedDeltaTime;

            _framesToFreeze = 0;
        }

        private void LateUpdate()
        {
            if (_framesToFreeze > 0)
            {
                --_framesToFreeze;

                if (_framesToFreeze == 0 && UnityEngine.Time.timeScale <= 0f)
                {
                    SetTimeScale(_timeScaleBeforeFreeze);
                }
            }
        }

        #endregion Methods
    }
}
