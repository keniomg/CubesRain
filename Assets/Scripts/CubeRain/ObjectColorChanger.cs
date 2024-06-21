using UnityEngine;

public class ObjectColorChanger : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    private RainDropObject _rainDropObject;

    public void Initialize(RainDropObject rainDropObject)
    {
        _rainDropObject = rainDropObject;
    }

    private void OnEnable()
    {
        _rainDropObject.RainDropTouchedPlatform += ChangeObjectColor;
    }

    private void OnDisable()
    {
        _rainDropObject.RainDropTouchedPlatform -= ChangeObjectColor;
    }

    private void ChangeObjectColor(RainDropObject rainDropObject)
    {
        int minimumColorIndex = 0;
        int maximumColorIndex = _colors.Length;
        int randomColorIndex = Random.Range(minimumColorIndex, maximumColorIndex);
        MeshRenderer rainDropObjectRenderer = _rainDropObject.GetMeshRenderer();
        rainDropObjectRenderer.material.color = _colors[randomColorIndex];
    }
}