using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;


public class Coin : MonoBehaviour
{
    //[SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 2f;
    [SerializeField] private float _shrinkDuration = 1f;

    public static event Action OnCoinCollected;
    private bool _isCollected = false;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), _rotateSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
       // StartCoroutine(DoRotate());
        //Destroy(gameObject, 10f);
    }


   // private IEnumerator DoRotate()
   //     while (!_isCollected)
    //    {
            //transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
            //transform.Translate(Vector3.back * _speed * Time.deltaTime, Space.World);
     //       yield return null;
      //  }
   // }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isCollected && other.CompareTag("Player"))
        {
            _isCollected = true;
            OnCoinCollected?.Invoke();
            StartCoroutine(TookCoin());
        }
    }

    private IEnumerator TookCoin()
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < _shrinkDuration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, elapsed / _shrinkDuration);
            yield return null;
        }

        Destroy(gameObject);
    }
}