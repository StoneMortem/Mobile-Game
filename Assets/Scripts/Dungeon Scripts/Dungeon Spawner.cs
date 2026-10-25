using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonSpawner : MonoBehaviour
{
    [Header("SETUP")]
    [SerializeField] GameObject[] roomPrefabs;
    [SerializeField] GameObject entrancePrefab;
    [SerializeField] GameObject exitPrefab;
    [SerializeField] Transform[] enemyPrefabs;
    [SerializeField] Transform playerPrefab;
    [SerializeField] Transform playerSpawner;

    [Header("VALUES")]
    [SerializeField] int roomAmount = 8;
    private float nextRoom = 0f;

    [Header("UI")]
    [SerializeField] GameObject dungeonCompleteCanvas;

    void Awake()
    {
        dungeonCompleteCanvas.SetActive(false);
    }

    void Start()
    {
        // Entrance is always first
        GameObject newRoomObject = Instantiate(entrancePrefab, new Vector3(nextRoom, 0f, 0f), Quaternion.identity);
        RoomScript newRoom = newRoomObject.GetComponent<RoomScript>();
        float roomWidth = newRoom.GetWidth();
        nextRoom += roomWidth - 6; // Dunno why only this works, fix it later

        for (int i = 0; i < roomAmount; i++)
        {
            SpawnRoom();
        }

        //Spawn Exit last
        SpawnExit();

        Transform player = Instantiate(playerPrefab, playerSpawner.position, Quaternion.identity);
        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        Debug.Log("Playerscript: " + playerScript);

        CombatManager.Instance.SetPlayer(playerScript);
        CombatManager.Instance.SetPlayerMovement(playerMovement);
    }

    void SpawnRoom()
    {
        int randomIndex = Random.Range(0, roomPrefabs.Length);

        GameObject newRoomObject = Instantiate(roomPrefabs[randomIndex], new Vector3(nextRoom, 0f, 0f), Quaternion.identity);
        RoomScript newRoom = newRoomObject.GetComponent<RoomScript>();

        float roomWidth = newRoom.GetWidth();

        nextRoom += roomWidth;

        Transform enemySpawn = newRoom.transform.Find("Enemy Spawner");

        SpawnEnemy(enemySpawn, newRoom);
    }

    void SpawnExit()
    {
        GameObject newRoomObject = Instantiate(exitPrefab, new Vector3(nextRoom, 0f, 0f), Quaternion.identity);
        Transform completeDungeonTrigger = newRoomObject.transform.Find("Dungeon Complete Trigger");
        DungeonCompleteScript dcScript = completeDungeonTrigger.GetComponent<DungeonCompleteScript>();

        dcScript.SetDungeonCompleteCanvas(dungeonCompleteCanvas);
    }

    void SpawnEnemy(Transform enemySpawn, RoomScript room)
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        Transform enemy = Instantiate(enemyPrefabs[randomIndex], enemySpawn.position, Quaternion.identity);

        // Find combat trigger and assign enemy to it
        Transform combatTrigger = room.transform.Find("Combat Trigger");
        CombatScript combatScript = combatTrigger.GetComponent<CombatScript>();

        combatScript.SetEnemy(enemy);
    }
}
