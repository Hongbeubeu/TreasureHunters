using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    public TileBase tileBase;
    public Tilemap tilemap;

    [Button(ButtonSizes.Gigantic)]
    public void GenMap(int _interval)
    {
        var mapPerlinNoise =
            GenerateMapAlgorithm.PerlinNoiseSmooth(GenerateMapAlgorithm.GenerateArray(10, 10, true),
                Random.Range(0f, 1f), _interval);
        GenerateMapAlgorithm.RenderMap(mapPerlinNoise, tilemap, tileBase);
    }
}