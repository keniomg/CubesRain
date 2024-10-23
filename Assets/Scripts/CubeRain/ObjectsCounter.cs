using TMPro;
using UnityEngine;

public abstract class ObjectsCounter<ObjectsSpawner, Spawnable> : MonoBehaviour where ObjectsSpawner : ObjectsSpawner<Spawnable> where Spawnable : SpawnableObject
{
    [SerializeField] private ObjectsSpawner _spawner;
    [SerializeField] private TextMeshProUGUI _objectsName;
    [SerializeField] private TextMeshProUGUI _objectsCountInstantiated;
    [SerializeField] private TextMeshProUGUI _objectsCountGetted;
    [SerializeField] private TextMeshProUGUI _objectsCountActive;

    private void OnEnable()
    {
        _spawner.InstantiatedObjectsChanged += OnObjectsCountInstantiatedChanged;
        _spawner.GettedObjectsChanged += OnObjectsCountGettedChanged;
        _spawner.ActiveObjectsChanged += OnObjectsCountActiveChanged;
    }

    private void OnDisable()
    {
        _spawner.InstantiatedObjectsChanged -= OnObjectsCountInstantiatedChanged;
        _spawner.GettedObjectsChanged -= OnObjectsCountGettedChanged;
        _spawner.ActiveObjectsChanged -= OnObjectsCountActiveChanged;
    }

    private void OnObjectsCountInstantiatedChanged(int value)
    {
        _objectsCountInstantiated.text = $"{_objectsName.text} created - {value}";
    }

    private void OnObjectsCountGettedChanged(int value)
    {
        _objectsCountGetted.text = $"{_objectsName.text} getted - {value}";
    }

    private void OnObjectsCountActiveChanged(int value)
    {
        _objectsCountActive.text = $"{_objectsName.text} active - {value}";
    }
}