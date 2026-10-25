using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonDisable : MonoBehaviour
{

    public GameObject disabledObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current != null)
        {
            disabledObjects.SetActive(false);
        }
        else
        {
            disabledObjects.SetActive(true);
        }

    }
}
