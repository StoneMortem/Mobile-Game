using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance;
    private Transform player;
    private bool followPlayer = true;

    [SerializeField] float cameraOffset = 0;

    // SETTERS
    public void SetPlayer (Transform player) { this.player = player; }
    public void SetFollowPlayer(bool followPlayer) { this.followPlayer = followPlayer; }

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
        if (followPlayer)
            transform.position = new Vector3(player.position.x + cameraOffset, transform.position.y, transform.position.z);
    }
}
