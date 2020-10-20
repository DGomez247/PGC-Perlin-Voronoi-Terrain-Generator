using UnityEngine;

[ExecuteInEditMode]

public class PGCTerrain : MonoBehaviour

{
    public Texture2D heightMapImage;
    public Terrain terrain;
    public TerrainData tData;
    public Vector2 randomHeight = new Vector2(0, 0.1f);
    public Vector3 mapScale = new Vector3(1, 1, 1);

   
    public int perlinXOffset = 0;
    public int perlinYOffset = 0;
    public float perlinScaleX = 0.01f;
    public float perlinScaleY = 0.01f;
    public float Persistance = 3;
    public float perlinHeightScale = 0.02f;
    public int Octaves = 3;
   

    [System.Serializable]
    public class MultiplePerlin
    {
        public float perlinScaleX = 0.01f;
        public float perlinScaleY = 0.01f;
        public int perlinXOffset = 0;
        public int perlinYOffset = 0;
        public int perlinOctaves = 3;
        public float Persistance = 3;
        public float perlinHeightScale = 0.02f;
    }

    public float vorMaxPeak = 0.5f;
    public float vorMinPeak = 0.1f;
    public int vorPeaks = 5;
    public float vorDrop = 0.6f;
    public float vorFall = 0.2f;

    public enum vType { Linear = 0, Power = 1, SinPow = 2, Combined = 3 }

    public vType voronoiType = vType.Linear;



    private float[,] GetHeightMap()
    {
        return tData.GetHeights(0, 0, tData.heightmapResolution, tData.heightmapResolution);
    }

    private void OnEnable()
    {
        terrain = this.GetComponent<Terrain>();
        tData = Terrain.activeTerrain.terrainData;

    }
    public void Perlin()
    {

        float[,] heightMap = GetHeightMap();

        for (int x = 0; x < tData.heightmapResolution; x++)
        {
            for (int y = 0; y < tData.heightmapResolution; y++)
            {
                heightMap[x, y] += Mathf.PerlinNoise(perlinScaleX * (x + perlinXOffset), perlinScaleY * (y + perlinYOffset));
            }
            tData.SetHeights(0, 0, heightMap);

        }
    }

    public void MultiplePerlinTerrain()
    {
        float[,] heightMap = GetHeightMap();
        for (int y = 0; y < tData.heightmapResolution; y++)
        {
            for (int x = 0; x < tData.heightmapResolution; x++)
            {
                heightMap[x, y] += fBM((x + perlinXOffset) * perlinScaleX, (y + perlinYOffset) * perlinScaleY, Octaves, Persistance) * perlinHeightScale;
            }
        }
        tData.SetHeights(0, 0, heightMap);
    }


    public void ResetTerrain()
    {

        float[,] heightMap;
        heightMap = new float[tData.heightmapResolution, tData.heightmapResolution];
        for (int x = 0; x < tData.heightmapResolution; x++)
        {
            for (int z = 0; z < tData.heightmapResolution; z++)
            {
                heightMap[x, z] = 0;
            }
        }
        tData.SetHeights(0, 0, heightMap);
    }
      public void Voronoi()

    {
        float[,] heightMap = GetHeightMap();
        for (int p = 0; p < vorPeaks; p++)
        {
            Vector3 peak = new Vector3(UnityEngine.Random.Range(0, tData.heightmapResolution), UnityEngine.Random.Range(vorMinPeak, vorMaxPeak), UnityEngine.Random.Range(0, tData.heightmapResolution));
            if (heightMap[(int)peak.x, (int)peak.z] < peak.y)
            {
                heightMap[(int)peak.x, (int)peak.z] = peak.y;
            }
            Vector2 peakLocation = new Vector2(peak.x, peak.z);
            float maxDistance = Vector2.Distance(new Vector2(0, 0), new Vector2(tData.heightmapResolution, tData.heightmapResolution));
            for (int y = 0; y < tData.heightmapResolution; y++)

            {
                for (int x = 0; x < tData.heightmapResolution; x++)

                {
                    if (!(x == peak.x && y == peak.z))
                    {
                        float range = Vector2.Distance(peakLocation, new Vector2(x, y)) / maxDistance;
                        float h;
                        if (voronoiType == vType.Combined)
                        {
                            h = peak.y - range * vorFall - Mathf.Pow(range, vorDrop); //combined                        }

                        }
                        else if (voronoiType == vType.Power)
                        {
                            h = peak.y - Mathf.Pow(range, vorDrop) * vorFall; //power                        }
                        }
                        else if (voronoiType == vType.SinPow)
                        {
                            h = peak.y - Mathf.Pow(range * 3, vorFall) - Mathf.Sin(range * 2 * Mathf.PI) / vorDrop; //sinpow                        }
                        }
                        else

                        {

                            h = peak.y - range * vorFall; //linear

                        }
                        if (heightMap[x, y] < h)

                            heightMap[x, y] = h;

                    }
                }
            }

        }
        tData.SetHeights(0, 0, heightMap);
    }

    public void RandomTerrain()
    {
        float[,] heightMap;
        heightMap = new float[tData.heightmapResolution, tData.heightmapResolution];
        for (int x = 0; x < tData.heightmapResolution; x++)
        {
            for (int z = 0; z < tData.heightmapResolution; z++)
            {
                heightMap[x, z] = UnityEngine.Random.Range(randomHeight.x, randomHeight.y);
            }
        }
        tData.SetHeights(0, 0, heightMap);
    }



    public static float fBM(float x, float y, int oct, float persistance)
    {
        float total = 0;
        float frequency = 1;
        float amp = 1;
        float maxValue = 0;
        for (int i = 0; i < oct; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, y * frequency) * amp;
            maxValue += amp;
            amp *= persistance;
            frequency *= 2;
        }
        return total / maxValue;
    }
}
