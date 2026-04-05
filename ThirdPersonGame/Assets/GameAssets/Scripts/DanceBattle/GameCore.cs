using System.Linq;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace GameAssets.Scripts.DanceBattle
{
    public class GameCore : MonoBehaviour
    {
        private const int ADD_SCORE_VALUE = 10;
        private const int SPECIAL_SCORE_VALUE = 50;
        private const float SPAWN_COOLDOWN = 0.3f;

        [SerializeField] private GameUI _ui;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private InputService _inputService;
        [SerializeField] private ActionZone[] _zones;
        [SerializeField] private GameObject _particles;
        [SerializeField] private TrackConfig _currentTrack;
        [SerializeField] private AudioSource _audioSource;

        private int _intervalIndex;
        private float _lastSpawnTime;
        private Statistics _stats;
        private bool _isPaused;
        private bool _isGameEnded;

        public event Action<int> ScoreChanged;

        private void OnEnable() => _inputService.SpecialClick += OnSpecialClick;
        private void OnDisable() => _inputService.SpecialClick -= OnSpecialClick;

        private void Start()
        {
            if (SelectedTrackManager.Instance?.SelectedTrack != null)
                _currentTrack = SelectedTrackManager.Instance.SelectedTrack;

            _intervalIndex = 0;
            _ui.Init(this);

            if (_currentTrack != null && _audioSource != null)
            {
                _stats = new Statistics(_currentTrack.Intervals.Count);
                _audioSource.clip = _currentTrack.Audio;
                _audioSource.Play();
            }

            foreach (var zone in _zones)
                zone.Init(() => HandleFail(zone));
        }

        private void Update()
        {
            // For tests
            if (Keyboard.current?.kKey.wasPressedThisFrame == true) EndGame();

            if (_isGameEnded || _isPaused || _currentTrack == null) return;


            if (_audioSource.time >= _audioSource.clip.length - 0.1f)
            {
                EndGame();
                return;
            }

            if (_intervalIndex < _currentTrack.Intervals.Count)
            {
                float nextBeatTime = _currentTrack.Intervals[_intervalIndex];
                float leadTime = _zones[0].Settings.PlayDuration;

                if (_audioSource.time >= nextBeatTime - leadTime)
                {
                    if (Time.time - _lastSpawnTime > SPAWN_COOLDOWN)
                    {
                        TryActivateZone(nextBeatTime);
                        _lastSpawnTime = Time.time;
                    }

                    _intervalIndex++;
                }
            }
        }

        private void TryActivateZone(float beatTime)
        {
            var freeZone = _zones.OrderBy(x => Random.value).FirstOrDefault(z => z.IsFree());
            freeZone?.PlayStartAnimation(beatTime);
        }

        public void SetPauseState(bool paused)
        {
            _isPaused = paused;
            if (paused) _audioSource.Pause();
            else _audioSource.UnPause();
        }

        private void HandleFail(ActionZone zone)
        {
            _stats.RemovePoints(ADD_SCORE_VALUE);
            zone.PlayFailedAnimation();
            ScoreChanged?.Invoke(_stats.Score);
        }

        private void OnSpecialClick(int number)
        {
            if (_isGameEnded || _isPaused) return;

            int index = number - 1;
            if (index < 0 || index >= _zones.Length) return;

            ActionZone zone = _zones[index];

            if (zone.CheckReady())
            {
                _stats.AddHit(ADD_SCORE_VALUE);
                zone.PlayPressedAnimation();
                CheckSpecialMove();
            }
            else
            {
                _stats.RemovePoints(ADD_SCORE_VALUE);
                zone.PlayFailedAnimation();
            }

            ScoreChanged?.Invoke(_stats.Score);
        }

        private void CheckSpecialMove()
        {
            if (_stats.Score > 0 && _stats.Score % SPECIAL_SCORE_VALUE == 0)
            {
                _particles.SetActive(false);
                _particles.SetActive(true);
                _animator.PlaySpecial();
            }
        }

        private void EndGame()
        {
            if (_isGameEnded) return;
            _isGameEnded = true;
            _audioSource.Stop();
            _ui.ShowResults(_stats.GetFormattedReport(), _stats.GetRank());
        }
    }
}