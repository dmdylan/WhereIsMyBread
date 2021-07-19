using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;

    public CinemachineVirtualCamera[] CinemachineVirtualCameras
    {
        get { return cinemachineVirtualCameras; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
