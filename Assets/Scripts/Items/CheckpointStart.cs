using UnityEngine;
using Cinemachine;

public class CheckpointStart : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerPosition = GetComponent<Transform>().position;
        playerPosition.x += 0.75f;
        playerPosition.y += 0.5f;

        Instantiate(player, playerPosition, Quaternion.identity);
        GameObject mainCam = GameObject.FindWithTag("MainCamera");
        CinemachineVirtualCamera vcam = mainCam.GetComponent<CinemachineVirtualCamera>();

        vcam.LookAt = GameObject.FindWithTag("Player").transform;
        vcam.Follow = GameObject.FindWithTag("Player").transform;


        foreach (GameObject backgroundVertical in GameObject.FindGameObjectsWithTag("BackgroundVertical")) {
            backgroundVertical.GetComponent<BackgroundVertical>().player = GameObject.FindWithTag("Player");
        }

        GameObject.FindWithTag("Tilemap_Platform").GetComponent<PlatformVertical>().player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
}
