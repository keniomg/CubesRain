using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class ColorChangerSystem : IEcsRunSystem
{
    private EcsFilter<RaindropComponent, MeshRendererChangableMarker, TouchedPlatformEvent>.Exclude<ColorChangedMarker> _colorChangableEntities;
    private EcsFilter<ColorChangerComponent> _colorChangers;

    public void Run()
    {
        foreach (var colorChanger in _colorChangers)
        {
            ref var colorChangerComponent = ref _colorChangers.Get1(colorChanger);

            foreach (var entity in _colorChangableEntities)
            {
                ref EcsEntity raindropEntity = ref _colorChangableEntities.GetEntity(entity);
                ref var raindropComponent = ref _colorChangableEntities.Get1(entity);
                var meshRenderer = raindropComponent.meshRenderer;

                if (meshRenderer != null)
                {
                    int minimumColorIndex = 0;
                    int maximumColorIndex = colorChangerComponent.colors.Length;
                    int randomColorIndex = Random.Range(minimumColorIndex, maximumColorIndex);
                    meshRenderer.material.color = colorChangerComponent.colors[randomColorIndex];
                    raindropEntity.Get<ColorChangedMarker>();
                }
            }
        }
    }
}