using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonSpawner : MonoBehaviour
{
    [Header("SETUP")]
    [SerializeField] GameObject[] roomPrefabs;
    [SerializeField] Transform[] enemyPrefabs;
    [SerializeField] Transform playerPrefab;
    [SerializeField] Transform playerSpawner;

    [Header("VALUES")]
    [SerializeField] int roomAmount = 8;
    private float nextRoom = 0f;

    void Start()
    {
        for (int i = 0; i < roomAmount; i++)
        {
            SpawnRoom();
        }

        Instantiate(playerPrefab, playerSpawner.position, Quaternion.identity);
    }

    void SpawnRoom()
    {
        int randomIndex = Random.Range(0, roomPrefabs.Length);

        GameObject newRoomObject = Instantiate(roomPrefabs[randomIndex], new Vector3(nextRoom, 0f, 0f), Quaternion.identity);
        RoomScript newRoom = newRoomObject.GetComponent<RoomScript>();

        float roomWidth = newRoom.GetWidth();

        nextRoom += roomWidth;

        Transform enemySpawn = newRoom.transform.Find("Enemy Spawner");

        SpawnEnemy(enemySpawn);
    }

    void SpawnEnemy(Transform enemySpawn)
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        Transform enemy = Instantiate(enemyPrefabs[randomIndex], enemySpawn.position, Quaternion.identity);
    }
}
