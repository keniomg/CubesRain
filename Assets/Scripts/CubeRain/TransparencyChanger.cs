using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class TransparencyChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _startMaterialAlpha;

    private void Awake()
    {
        _startMaterialAlpha = 1;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnDisable()
    {
        float materialAlpha = _meshRenderer.material.color.a;
        materialAlpha = _startMaterialAlpha;
    }

    public void StartTransparencyCountdown(float totalTransparencyTime)
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            StartCoroutine(TransparencyCountdown(totalTransparencyTime, meshRenderer));
        }
        
        StartCoroutine(TransparencyCountdown(totalTransparencyTime, _meshRenderer));
    }

    private IEnumerator TransparencyCountdown(float totalTransparencyTime, MeshRenderer meshRenderer)
    {
        float waitForSecondsDelay = Time.deltaTime;
        WaitForSeconds waitForSeconds = new(waitForSecondsDelay);
        float leftTransparencyTime = totalTransparencyTime;

        meshRenderer.material.SetFloat("_Surface", 1);
        meshRenderer.material.SetOverrideTag("RenderType", "Transparent");
        meshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        meshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        meshRenderer.material.SetInt("_ZWrite", 0);
        meshRenderer.material.DisableKeyword("_ALPHATEST_ON");
        meshRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        meshRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        meshRenderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        Material material = meshRenderer.material;
        Color color = material.color;

        while (leftTransparencyTime > 0)
        {
            leftTransparencyTime -= waitForSecondsDelay;
            float alphaValue = leftTransparencyTime / totalTransparencyTime;
            color.a = alphaValue;
            material.color = color;

            yield return waitForSeconds;
        }
    }
}