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
        for (int i = 0; i < 5; i++)
        {
            BoxCollider Area = _spawnAreas[Random.Range(0, _spawnAreas.Length)];

            await InstantiateAsync(_coinPrefab, new Vector3(Area.bounds.center.x,
                Random.Range(Area.bounds.min.y, Area.bounds.max.y), Area.bounds.center.z), Quaternion.identity);
            await Task.Delay(1000);
        }
    }
}