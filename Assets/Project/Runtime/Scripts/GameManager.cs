using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine.UI;
using Mirror;
using System.Collections;
using TMPro;

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

    [SerializeField] private int gameLength;
    [SerializeField] private Image abilityOneImage;
    [SerializeField] private Image abilityTwoImage;
    [SerializeField] private TMP_Text abilityOneCDText;
    [SerializeField] private TMP_Text abilityTwoCDText;
    [SerializeField] private TMP_Text gameTimerText;
    [SerializeField] private CinemachineVirtualCamera[] cinemachineGuraCameras;
    [SerializeField] private CinemachineVirtualCamera[] cinemachineBreadCameras;

    private AbilitySO abilityOne;
    private AbilitySO abilityTwo;

    private void Start()
    {
        gameTimer = gameLength;
        StartCoroutine(GameTimer());
    }

    void GameOver()
    {
        Debug.Log("Game over!");
    }

    #region UI

    public void UISetup(AbilitySO abilityOne, AbilitySO abilityTwo)
    {
        this.abilityOne = abilityOne;
        this.abilityTwo = abilityTwo;
        abilityOneImage.sprite = abilityOne.Icon;
        abilityTwoImage.sprite = abilityTwo.Icon;
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
