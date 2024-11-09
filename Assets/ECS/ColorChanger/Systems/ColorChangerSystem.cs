using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class ColorChangerSystem : IEcsRunSystem
{
    private EcsFilter<RaindropMarker, MeshRendererComponent, TouchedPlatformMarker>.Exclude<ColorChangedMarker> _colorChangableEntities;
    private EcsFilter<ColorChangerComponent> _colorChangers;

    public void Run()
    {
        foreach (var colorChanger in _colorChangers)
        {
            ref var colorChangerComponent = ref _colorChangers.Get1(colorChanger);

            foreach (var entity in _colorChangableEntities)
            {
                ref EcsEntity raindropEntity = ref _colorChangableEntities.GetEntity(entity);
                ref var meshRendererComponent = ref _colorChangableEntities.Get2(entity);
                var meshRenderer = meshRendererComponent.MeshRenderer;

                if (meshRenderer != null)
                {
                    int minimumColorIndex = 0;
                    int maximumColorIndex = colorChangerComponent.Colors.Length;
                    int randomColorIndex = Random.Range(minimumColorIndex, maximumColorIndex);
                    meshRenderer.material.color = colorChangerComponent.Colors[randomColorIndex];
                    raindropEntity.Get<ColorChangedMarker>();
                }
            }
        }
    }
}