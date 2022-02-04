using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using Views;
using Zenject;
using Random = System.Random;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField]
    private PoolBundle _pools;

    [Inject]
    private SignalBus _signalBus;

    private Random _random;
    private List<GameObject> _clouds;

    private void Start()
    {
        _signalBus.Subscribe<LevelStarting>(() => StartCoroutine(SpawnCloud()));
        _signalBus.Subscribe<LevelFailing>(() =>
        {
            StopCoroutine(SpawnCloud());
            Decompose();
        });
        _random = new Random();
        _clouds = new List<GameObject>();
    }

    private void Decompose()
    {
        foreach (var cloud in _clouds)
        {
            _pools.ReturnObject(cloud.name, cloud);
        }
    }

    private IEnumerator SpawnCloud()
    {
        while (true)
        {
            var tag = $"Cloud_{_random.Next(3)}";
            var cloud = _pools.GetObject(tag);
            cloud.name = tag;
            cloud.transform.position = new Vector3(_random.Next(45, 60), 5f, 0);
            _clouds.Add(cloud);
            cloud.GetComponent<Cloud>().StartMove();

            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        }
    }
}