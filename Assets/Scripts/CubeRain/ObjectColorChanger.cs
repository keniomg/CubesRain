using UnityEngine;

public class ObjectColorChanger : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    private RainDropObject _rainDropObject;

    private void OnEnable()
    {
        _rainDropObject.RainDropTouchedPlatform += ChangeObjectColor;
    }

    private void OnDisable()
    {
        _rainDropObject.RainDropTouchedPlatform -= ChangeObjectColor;
    }

    public void Initialize(RainDropObject rainDropObject)
    {
        _rainDropObject = rainDropObject;
    }

    private void ChangeObjectColor(RainDropObject rainDropObject)
    {
        int minimumColorIndex = 0;
        int maximumColorIndex = _colors.Length;
        int randomColorIndex = Random.Range(minimumColorIndex, maximumColorIndex);
        MeshRenderer rainDropObjectRenderer = _rainDropObject.Renderer;
        rainDropObjectRenderer.material.color = _colors[randomColorIndex];
    }
}