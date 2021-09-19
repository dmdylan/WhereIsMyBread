using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine.UI;
using Mirror;
using System.Collections;
using TMPro;
using System;

public class GameManager : NetworkBehaviour
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

    //TODO: Might work as SO instead of syncvar.
    [SyncVar(hook = nameof(UpdateTimer))]
    private int gameTimer;

    //Game Settings
    [Header("Game Settings")]
    [SerializeField] private int gameLength;

    //UI Info
    [Header("UI Fields")]
    [SerializeField] private TMP_Text gameTimerText;

    //Ability UI
    [Header("Ability UI Info")]
    [SerializeField] private Image abilityOneImage;
    [SerializeField] private Image abilityTwoImage;
    [SerializeField] private TMP_Text abilityOneCDText;
    [SerializeField] private TMP_Text abilityTwoCDText;

    //Game Over UI
    [Header("Game Over UI Info")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject returnToLobbyButton;
    [SerializeField] private GameObject guraWinsObject;
    [SerializeField] private GameObject breadWinsObject;
    [SerializeField] private List<GameObject> playerPanels;
    [SerializeField] private List<GameObject> playerDeadText;

    //Cameras
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera[] cinemachineGuraCameras;
    [SerializeField] private CinemachineVirtualCamera[] cinemachineBreadCameras;

    private AbilitySO abilityOne;
    private AbilitySO abilityTwo;

    #region Event Stuff

    public event Action<NetworkConnection> OnPlayerDied;
    public void PlayerDied(NetworkConnection conn) => OnPlayerDied?.Invoke(conn);

    #endregion

    private void Start()
    {
        gameTimer = gameLength;
        //PlayerSetup();
        StartCoroutine(GameTimer());
    }

    private void OnEnable()
    {
        OnPlayerDied += GameManager_OnPlayerDied;
    }

    private void OnDisable()
    {
        OnPlayerDied -= GameManager_OnPlayerDied;
    }

    private void GameManager_OnPlayerDied(NetworkConnection conn)
    {
        PlayerInfo info = WMBNetworkManager.players[conn.connectionId];
        info.IsDead = true;
        WMBNetworkManager.players[conn.connectionId] = info;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        GameOverUISetup();
    }

    #region UI

    //TODO: Setup UI portraits for each player?
    public void AbilityUISetup(AbilitySO abilityOne, AbilitySO abilityTwo)
    {
        this.abilityOne = abilityOne;
        this.abilityTwo = abilityTwo;
        abilityOneImage.sprite = abilityOne.Icon;
        abilityTwoImage.sprite = abilityTwo.Icon;
    }

    private void GameOverUISetup()
    {
        gameOverPanel.SetActive(true);
        int count = 0;

        foreach(PlayerInfo playerInfo in WMBNetworkManager.players.Values)
        {
            playerPanels[count].SetActive(true);
            playerPanels[count].GetComponent<TMP_Text>().text = playerInfo.PlayerName;

            if (playerInfo.IsDead)
            {
                playerDeadText[count].SetActive(true);
            }

            count++;
        }
    }

    IEnumerator GameTimer()
    {
        while(gameTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            gameTimer--;
        }

        //TODO: Stuff when timer is 0
        GameOver();
    }

    public IEnumerator AbilityOneCountdown(float duration)
    {
        abilityOneImage.sprite = abilityOne.IconCD;
        float totalTime = 0;
        while (totalTime <= duration)
        {
            abilityOneImage.fillAmount = totalTime / duration;
            totalTime += Time.deltaTime;
            var integer = (int)duration - (int)totalTime;
            abilityOneCDText.text = integer.ToString();
            yield return null;
        }

        abilityOneImage.sprite = abilityOne.Icon;
        abilityOneCDText.text = "";
    }

    public IEnumerator AbilityTwoCountdown(float duration)
    {
        abilityTwoImage.sprite = abilityTwo.IconCD;
        float totalTime = 0;
        while (totalTime <= duration)
        {
            abilityTwoImage.fillAmount = totalTime / duration;
            totalTime += Time.deltaTime;
            var integer = (int)duration - (int)totalTime;
            abilityTwoCDText.text = integer.ToString();
            yield return null;
        }

        abilityTwoImage.sprite = abilityTwo.Icon;
        abilityTwoCDText.text = "";
    }

    #endregion

    #region Camera
    public List<Transform> SpectateTransforms { get; set; }

    public CinemachineVirtualCamera[] CinemachineGuraCameras
    {
        get { return cinemachineGuraCameras; }
    }

    public CinemachineVirtualCamera[] CinemachineBreadCameras
    {
        get { return cinemachineBreadCameras; }
    }

    //TODO: Get list of all players that join match. Can be used to set other players follow cameras for spectating.

    #endregion

    #region Hooks

    void UpdateTimer(int oldValue, int newValue)
    {
        var minutes = newValue / 60;
        var seconds = newValue % 60;

        gameTimerText.text = $"{minutes:00} : {seconds:00}";
    }

    #endregion
}
