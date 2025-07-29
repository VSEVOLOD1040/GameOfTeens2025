using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float mapWidth;
    public float mapHeight;
    public UIManager ui;
    public List<GameObject> enemies;

    public int wave_index = 0;

    public Dictionary<int, GameObject> wave1 = new();
    public Dictionary<int, GameObject> wave2 = new();
    public Dictionary<int, GameObject> wave3 = new();
    public Dictionary<int, GameObject> wave4 = new();
    public Dictionary<int, GameObject> wave5 = new();

    public Transform enemiesParent;

    private bool waveInProgress = false;

    private void Start()
    {
        if (enemies.Count >= 1)
        {
            wave1.Add(5, enemies[0]);
            wave2.Add(5, enemies[0]);
            wave3.Add(6, enemies[0]);
            wave4.Add(8, enemies[0]);
            wave5.Add(10, enemies[0]);
        }

        if (enemies.Count >= 2)
        {
            wave2.Add(3, enemies[1]);
            wave3.Add(4, enemies[1]);
            wave4.Add(5, enemies[1]);
            wave5.Add(7, enemies[1]);
        }
        if (enemies.Count >= 3)
        {
            wave3.Add(1, enemies[2]);
            wave4.Add(3, enemies[2]);
            wave5.Add(8, enemies[2]);
        }
        if (enemies.Count >= 4)
        {
            wave4.Add(6, enemies[3]);
            wave5.Add(4, enemies[3]);
        }
        if (enemies.Count >= 5)
        {
            wave5.Add(9, enemies[4]);
        }



        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            ui.SetTextWaveIndex($"Wave: {wave_index}");
            if (!waveInProgress && enemiesParent.childCount == 0)
            {
                waveInProgress = true;
                yield return new WaitForSeconds(2f);
                StartWave(wave_index);
                wave_index++;
                waveInProgress = false;
            }

            yield return null;
        }
    }

    private void StartWave(int index)
    {
        gameObject.GetComponent<MapGenerator>().GenerateBoxes(index);
        Dictionary<int, GameObject> waveData = index switch
        {
            0 => wave1,
            1 => wave2,
            2 => wave3,
            3 => wave4,
            4 => wave5,
            _ => null
        };

        if (waveData != null)
        {
            foreach (var pair in waveData)
            {
                GenerateEnemiesAtEdges(pair.Key, pair.Value);
            }
        }
        else
        {
            int scale = index - 4;
            GenerateEnemiesAtEdges(10 + scale * 2, enemies[0]);
            if (enemies.Count > 1) GenerateEnemiesAtEdges(7 + scale, enemies[1]);
            if (enemies.Count > 2) GenerateEnemiesAtEdges(4 + scale, enemies[2]);
            if (enemies.Count > 3) GenerateEnemiesAtEdges(2 + scale / 2, enemies[3]);
            if (enemies.Count > 4) GenerateEnemiesAtEdges(1 + scale / 2, enemies[4]);
        }

    }

    void GenerateEnemiesAtEdges(int amount, GameObject enemyPrefab)
    {
        int spawned = 0;
        int attempts = 0;
        int maxAttempts = amount * 10;
        float edgeThickness = 5f;

        float minX = -mapWidth / 2;
        float maxX = mapWidth / 2;
        float minY = -mapHeight / 2;
        float maxY = mapHeight / 2;

        while (spawned < amount && attempts < maxAttempts)
        {
            attempts++;

            int side = Random.Range(0, 4);
            Vector2 position = Vector2.zero;

            switch (side)
            {
                case 0: // Left
                    position = new Vector2(Random.Range(minX, minX + edgeThickness), Random.Range(minY, maxY));
                    break;
                case 1: // Right
                    position = new Vector2(Random.Range(maxX - edgeThickness, maxX), Random.Range(minY, maxY));
                    break;
                case 2: // Bottom
                    position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, minY + edgeThickness));
                    break;
                case 3: // Top
                    position = new Vector2(Random.Range(minX, maxX), Random.Range(maxY - edgeThickness, maxY));
                    break;
            }

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

            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity, enemiesParent);
            spawned++;
        }
    }


}
