
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MapGenerator : MonoBehaviour
{
    public float mapWidth;
    public float mapHeight;
    public int boxAmount;
    public GameObject boxPrefab;
    public int treeAmount;
    public GameObject treePrefab;
    public int houseAmount;
    public GameObject housePrefab;
    
    public GameObject playerPrefab;

    public GameObject tilePrefab;
    public float tileSize = 1f;
    public float noiseScale = 5f;
    public Color grassColor = new Color(0.2f, 0.8f, 0.2f);
    public Color dirtColor = new Color(0.4f, 0.26f, 0.13f);

    public int LakeAmount;
    public GameObject lakePrefab;

    void Start()
    {
        GenerateGridWithBiomes();
        GenerateLakes();
        GenerateRandom(boxAmount, boxPrefab);
        GenerateRandom(treeAmount, treePrefab);
        GenerateRandom(houseAmount, housePrefab);
        GenerateRandom(1, playerPrefab, false);

        StartCoroutine(DelayedScan());
    }

    private IEnumerator DelayedScan()
    {
        yield return null;
        AstarPath.active.Scan();
    }

    void GenerateRandom(int Amount, GameObject Prefab, bool Rotate = true)
    {
        int spawned = 0;
        int attempts = 0;
        int maxAttempts = Amount * 10;

        while (spawned < Amount && attempts < maxAttempts)
        {
            attempts++;

            Vector2 position = new Vector2(Random.Range(-mapWidth / 2, mapWidth / 2),
                                           Random.Range(-mapHeight / 2, mapHeight / 2));

            float checkRadius = 3f;

            Collider2D[] hits = Physics2D.OverlapCircleAll(position, checkRadius);
            bool onLake = false;
            foreach (var col in hits)
            {
                if (col.gameObject.CompareTag("Lake"))
                {
                    onLake = true;
                    break;
                }
            }
            if (onLake) continue;



            GameObject obj = Instantiate(Prefab, position, Quaternion.identity);
            if (Rotate)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            }
            spawned++;
        }
    }


    void GenerateGridWithBiomes()
    {
        float startX = -mapWidth / 2;
        float startY = -mapHeight / 2;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector2 position = new Vector2(startX + x, startY + y);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);

                float noiseX = (float)x / mapWidth * noiseScale;
                float noiseY = (float)y / mapHeight * noiseScale;
                float noiseValue = Mathf.PerlinNoise(noiseX, noiseY);
                Color tileColor = Color.Lerp(dirtColor, grassColor, noiseValue);

                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = tileColor;
                }
            }
        }
    }

    void GenerateLakes()
    {
        for (int i = 0; i < LakeAmount; i++)
        {
            Vector2 position = new Vector2(Random.Range(-mapWidth / 2, mapWidth / 2),
                                           Random.Range(-mapHeight / 2, mapHeight / 2));
            GameObject lake = Instantiate(lakePrefab, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            lake.transform.parent = transform;
        }
    }
}
