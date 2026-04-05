using UnityEngine;

namespace GameAssets.Scripts.DanceBattle
{
    public class PlayerAnimator : MonoBehaviour
    {
        private readonly int[] _specialDances =
        {
            AnimatorParameters.SpecialDance1,
            AnimatorParameters.SpecialDance2
        };

        private Animator _animator;

        public void PlaySpecial()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                _animator.SetTrigger(_specialDances[Random.Range(0, _specialDances.Length)]);
            }
        }
        private void Awake()
        {
            
            _animator = GetComponent<Animator>();
        }

    }
}