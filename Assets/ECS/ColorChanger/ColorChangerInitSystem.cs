using Leopotam.Ecs;
using ECS.Data;
using ECS.Components;

public class ColorChangerInitSystem : IEcsInitSystem
{
    private readonly EcsWorld _world;

    private readonly ColorChangerData _colorChangerData;

    public ColorChangerInitSystem(ColorChangerData colorChangerData)
    {
        _colorChangerData = colorChangerData;
    }

    public void Init()
    {
        var colorChanger = _world.NewEntity();

        ref var colorChangerComponent = ref colorChanger.Get<ColorChangerComponent>();
        colorChangerComponent.colors = _colorChangerData.Colors;
    }
}