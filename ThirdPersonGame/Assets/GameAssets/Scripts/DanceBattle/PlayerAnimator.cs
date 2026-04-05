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

        [SerializeField] private Animator _animator;

        public void PlaySpecial()
        {
            int randomIndex = Random.Range(0, _specialDances.Length);
            _animator.SetTrigger(_specialDances[randomIndex]);
        }
    }
}