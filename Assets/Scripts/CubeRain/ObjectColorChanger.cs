using UnityEngine;

public class ObjectColorChanger : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    private Raindrop _raindropObject;

    private void OnEnable()
    {
        _raindropObject.RaindropTouchedPlatform += ChangeObjectColor;
    }

    private void OnDisable()
    {
        _raindropObject.RaindropTouchedPlatform -= ChangeObjectColor;
    }

    public void Initialize(Raindrop rainDropObject)
    {
        _raindropObject = rainDropObject;
    }

    private void ChangeObjectColor(Raindrop rainDropObject)
    {
        int minimumColorIndex = 0;
        int maximumColorIndex = _colors.Length;
        int randomColorIndex = Random.Range(minimumColorIndex, maximumColorIndex);
        MeshRenderer rainDropObjectRenderer = _raindropObject.Renderer;
        rainDropObjectRenderer.material.color = _colors[randomColorIndex];
    }
}