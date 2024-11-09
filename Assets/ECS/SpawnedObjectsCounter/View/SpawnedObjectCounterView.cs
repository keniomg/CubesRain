using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnedObjectCounterView : MonoBehaviour
{
    [SerializeField] private SpawnedObjectsData _spawnedObjectsData;
    [SerializeField] private TextMeshProUGUI _spawnedRaindrops;
    [SerializeField] private TextMeshProUGUI _spawnedBombs;

    private void Update()
    {
        _spawnedRaindrops.text = $"���������� ����� - {_spawnedObjectsData.SpawnedRaindropsCount}";
        _spawnedBombs.text = $"���������� ���� - {_spawnedObjectsData.SpawnedBombsCount}";
    }
}