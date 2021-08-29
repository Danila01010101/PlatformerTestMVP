using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _platformPrefabs;
    private int _randIndex;
    void Start()
    {
        for (int i = 0; i <= 5; i++)
        {
            _randIndex = Random.Range(0, _platformPrefabs.Length);
            Instantiate(_platformPrefabs[_randIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
