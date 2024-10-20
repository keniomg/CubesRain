using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class TransparencyCountdownSystem : IEcsRunSystem
{
    private EcsFilter<TransparencyCountdownComponent, LifetimeCountdownComponent, MeshRendererChangableMarker, HasGameObjectComponent>.Exclude<ObjectSpawnedEvent> _filter;
    
    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var transparencyCountdownComponent = ref _filter.Get1(entity);
            ref var lifetimeCountdownComponent = ref _filter.Get2(entity);
            ref var hasGameObjectComponent = ref _filter.Get4(entity);

            transparencyCountdownComponent.materialAlpha = lifetimeCountdownComponent.currentLifetime / lifetimeCountdownComponent.startLifetime;
            var meshRenderers = hasGameObjectComponent.gameObject.GetComponentsInChildren<MeshRenderer>();

            foreach (var meshRenderer in meshRenderers)
            {
                ChangeTransparency(meshRenderer, transparencyCountdownComponent.materialAlpha);
            }

            ChangeTransparency(transparencyCountdownComponent.meshRenderer, transparencyCountdownComponent.materialAlpha);
        }
    }

    private void ChangeTransparency(MeshRenderer meshRenderer, float value)
    {
        meshRenderer.material.SetFloat("_Surface", 1);
        meshRenderer.material.SetOverrideTag("RenderType", "Transparent");
        meshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        meshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        meshRenderer.material.SetInt("_ZWrite", 0);
        meshRenderer.material.DisableKeyword("_ALPHATEST_ON");
        meshRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        meshRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        meshRenderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        var color = meshRenderer.material.color;
        color.a = value;
        meshRenderer.material.color = color;
    }
}