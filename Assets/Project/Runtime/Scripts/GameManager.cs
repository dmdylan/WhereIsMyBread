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

    //TODO: Get list of all players that join match. Can be used to set other players follow cameras for spectating.
}
