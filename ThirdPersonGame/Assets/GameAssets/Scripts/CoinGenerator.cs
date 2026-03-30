using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private BoxCollider[] _spawnAreas;
    [SerializeField] private Coin _coinPrefab;

    private async void Start()
    {
        for (int i = 0; i < _spawnAreas.Length; i++)
        {
            BoxCollider Area = _spawnAreas[i];


            Vector3 spawnPosition = new Vector3(
                Area.bounds.center.x,
                Random.Range(Area.bounds.min.y, Area.bounds.max.y),
                Area.bounds.center.z
            );

            await InstantiateAsync(_coinPrefab, spawnPosition, Quaternion.identity);

            await Task.Delay(1000);
        }
    }
}