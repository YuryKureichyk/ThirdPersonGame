using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace GameAssets.Scripts.DanceBattle
{
    public class GameCore : MonoBehaviour

    {
        private const int ADD_SCORE_VALUE = 10;
        private const int SPECIAL_SCORE_VALUE = 50;

        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private InputService _inputService;
        [SerializeField] private ActionZone[] _zones;
        [SerializeField] private GameObject _particles;

        private Action[] _failHandlers;

        private int _score = 0;
        private int _currentZoneIndex = 0;


        public event Action<int> ScoreChanged;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreChanged?.Invoke(value);
            }
        }

        private void OnEnable()
        {
            _inputService.SpecialClick += OnSpecialClick;
        }

        private void OnDisable()
        {
            _inputService.SpecialClick -= OnSpecialClick;
        }

        private void Start()
        {
            Score = 0;

            _failHandlers = new Action[_zones.Length];

            for (int i = 0; i < _zones.Length; i++)
            {
                int zoneIndex = i;
                _zones[i].Init(() => HandleFail(zoneIndex));
            }

            Invoke(nameof(ActivateNextZone), 1f);
        }

        private void ActivateNextZone()
        {
            _zones[_currentZoneIndex].PlayStartAnimation();
        }

        private void HandleFail(int index)
        {
            if (index == _currentZoneIndex)
            {
                _zones[index].PlayFailedAnimation();
                Score = Math.Max(0, Score - ADD_SCORE_VALUE);
                SwitchToNext();
            }
        }

        private void OnSpecialClick(int number)
        {
            int clickedIndex = number - 1;

            if (clickedIndex != _currentZoneIndex) return;

            if (_zones[clickedIndex].CheckReady())
            {
                _zones[clickedIndex].PlayPressedAnimation();
                Score += ADD_SCORE_VALUE;

                if (Score >= SPECIAL_SCORE_VALUE && Score % SPECIAL_SCORE_VALUE == 0)
                {
                    _particles.SetActive(false);
                    _particles.SetActive(true);
                    _animator.PlaySpecial();
                }

                SwitchToNext();
            }
            else
            {
                _zones[clickedIndex].PlayFailedAnimation();
                Score = Math.Max(0, Score - ADD_SCORE_VALUE);
                SwitchToNext();
            }
        }

        private void SwitchToNext()
        {
            _currentZoneIndex = Random.Range(0, _zones.Length);
            Invoke(nameof(ActivateNextZone), 0.5f);
        }
    }
}