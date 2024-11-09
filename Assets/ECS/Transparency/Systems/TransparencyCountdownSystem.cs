using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class TransparencyCountdownSystem : IEcsRunSystem
{
    private EcsFilter<MaterialAlphaComponent, LifetimeCountdownComponent, MeshRendererComponent, GameObjectComponent>.Exclude<ObjectSpawnedMarker> _filter;
    
    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var materialAlphaComponent = ref _filter.Get1(entity);
            ref var lifetimeCountdownComponent = ref _filter.Get2(entity);
            ref var meshRendererComponent = ref _filter.Get3(entity);
            ref var hasGameObjectComponent = ref _filter.Get4(entity);

            materialAlphaComponent.MaterialAlpha = lifetimeCountdownComponent.CurrentLifetime / lifetimeCountdownComponent.StartLifetime;
            var meshRenderers = hasGameObjectComponent.OwnObject.GetComponentsInChildren<MeshRenderer>();

            foreach (var meshRenderer in meshRenderers)
            {
                ChangeTransparency(meshRenderer, materialAlphaComponent.MaterialAlpha);
            }

            ChangeTransparency(meshRendererComponent.MeshRenderer, materialAlphaComponent.MaterialAlpha);
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