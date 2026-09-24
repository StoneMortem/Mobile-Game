using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance;
    private Transform player;

    public void SetPlayer (Transform player) { this.player = player; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
