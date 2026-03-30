using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameAssets.Scripts.DanceBattle
{
    public class ActionZone : MonoBehaviour

    {
        [SerializeField] private Image _ring;
        [SerializeField] private ActionZoneSettings _settings;
        private Tweener _tween;
        private Action _failed;
        private float _startTime;
        public bool IsFailed { get; private set; }


        public void Init(Action failed)
        {
            _failed = failed;
        }

        public bool CheckReady()
        {
           return (Time.time - _startTime > _settings.PlayThreshold) && !IsFailed;
        }

        [ContextMenu("PlayStartAnimation")]
        public void PlayStartAnimation()
        {
            ResetSate();
            _ring.gameObject.SetActive(true);
            _startTime = Time.time;
            _tween?.Kill();
            _tween = _ring.transform.DOScale(_settings.PlayScale.y, _settings.PlayDuration)
                .OnComplete(OnAnimationComplete);
        }

        public void PlayPressedAnimation()
        {
            _tween?.Kill();
            _tween = _ring.DOColor(Color.green, 0.3f)
                .OnComplete(ResetSate);
        }

        public void PlayFailedAnimation()
        {
            _tween.Kill();
            _tween = _ring.DOColor(_settings.FailColor, _settings.FailDuration)
                                          .OnComplete(ResetSate);
        }

        private void Start()
        {
            ResetSate();
        }

        private void ResetSate()
        {
            IsFailed = false;
            _ring.transform.localScale = new Vector3(1, 1, 1) * _settings.PlayScale.x;
            _ring.color = Color.white;
            _ring.gameObject.SetActive(false);
        }


        private void OnAnimationComplete()
        {
            IsFailed = true;
            _failed?.Invoke();
            
            
        }
    }
}