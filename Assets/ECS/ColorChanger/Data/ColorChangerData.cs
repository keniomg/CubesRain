using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu]
    public class ColorChangerData : ScriptableObject
    {
        [field: SerializeField] public Color[] Colors {get; private set; }   
    }
}