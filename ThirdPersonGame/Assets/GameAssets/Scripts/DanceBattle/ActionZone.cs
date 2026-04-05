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
        [SerializeField] private GameObject _sucessParticles;
        [SerializeField] private GameObject _failParticles;

        public ActionZoneSettings Settings => _settings;
        public bool IsFailed { get; private set; }

        private Tweener _tween;
        private Action _onFailedCallback;

        public bool IsFree() => !_ring.gameObject.activeInHierarchy;

        public void Init(Action onFailed) => _onFailedCallback = onFailed;

        public void PlayStartAnimation(float targetAudioTime)
        {
            StopCurrentTween();
            ResetState();

            _ring.gameObject.SetActive(true);
            _tween = _ring.transform.DOScale(_settings.PlayScale.y, _settings.PlayDuration)
                .SetEase(Ease.Linear)
                .OnComplete(OnAnimationComplete);
        }

        public bool CheckReady()
        {
            if (IsFree() || IsFailed) return false;

            float precision = Mathf.Abs(_ring.transform.localScale.x - _settings.PlayScale.y);
            return precision < 0.25f;
        }

        public void PlayPressedAnimation()
        {
            StopCurrentTween();
            _sucessParticles.SetActive(true);
            _ring.DOColor(Color.green, 0.3f).OnComplete(ResetState);
        }

        public void PlayFailedAnimation()
        {
            StopCurrentTween();
            _failParticles.SetActive(true);
            _ring.DOColor(_settings.FailColor, _settings.FailDuration).OnComplete(ResetState);
        }

        private void Start() => ResetState();

        private void ResetState()
        {
            IsFailed = false;
            _ring.gameObject.SetActive(false);
            _ring.transform.localScale = Vector3.one * _settings.PlayScale.x;
            _ring.color = Color.white;
            _sucessParticles.SetActive(false);
            _failParticles.SetActive(false);
        }

        private void StopCurrentTween() => _tween?.Kill();

        private void OnAnimationComplete()
        {
            IsFailed = true;
            _failParticles.SetActive(true);
            _onFailedCallback?.Invoke();
            DOVirtual.DelayedCall(0.2f, () => _ring.gameObject.SetActive(false));
        }
    }
}