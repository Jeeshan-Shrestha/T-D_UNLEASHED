using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public UIManager ui;
    public TextMeshProUGUI playerNumber;
    public TextMeshProUGUI truthOrDareText;
    public Player1HealthScripts player1Health;
    public Player2HealthScripts player2Health;
    public PlayerAttackTimeline playerAttackTimeline;

    [Header("UI Panels/Buttons")]
    public GameObject levelSelectButtons;   // panel with Level 1 / 2 / 3 buttons
    public GameObject truthOrDareButton;    // panel with Truth / Dare buttons
    public GameObject completeOrFailButton; // panel with Complete / Fail buttons
    public TextFade fade;
    [Header("Game State")]
    public int playerTurn = 1;

    private TruthDareLoader loader;
    private bool gameEnded = false;

    private int selectedLevel = 1;
    private string selectedType;
    public GameObject truthOrDare;

    void Start()
    {
        loader = FindObjectOfType<TruthDareLoader>();

        completeOrFailButton.SetActive(false);
        truthOrDareButton.SetActive(false);
        levelSelectButtons.SetActive(true);

        playerAttackTimeline.OnFinisherFinished += GameOver;
    }

    void OnDestroy()
    {
        playerAttackTimeline.OnFinisherFinished -= GameOver;
    }

    void Update()
    {
        playerNumber.text = "PLAYER " + playerTurn;

        if (gameEnded) return;

        if (player2Health.health <= 0)
        {
            gameEnded = true;
            HideGameplayUI();
            playerAttackTimeline.PlayPlayer1FinisherTimeline();
        }
        else if (player1Health.health <= 0)
        {
            gameEnded = true;
            HideGameplayUI();
            playerAttackTimeline.PlayPlayer2FinisherTimeline();
        }
    }


    public void SelectLevel1() => SelectLevel(1);
    public void SelectLevel2() => SelectLevel(2);
    public void SelectLevel3() => SelectLevel(3);

    private void SelectLevel(int level)
    {
        selectedLevel = level;
        levelSelectButtons.SetActive(false);
        truthOrDare.SetActive(true);
        StartCoroutine(fade.FadeRoutine());
    }

    public void Truth()
    {
        selectedType = "Truth";
        ShowPrompt();
    }

    public void Dare()
    {
        selectedType = "Dare";
        ShowPrompt();
    }

    private void ShowPrompt()
    {
        truthOrDareButton.SetActive(false);
        completeOrFailButton.SetActive(true);

        TruthDareItem prompt = loader.GetRandomExact(selectedLevel, selectedType);
        truthOrDareText.text = prompt != null ? prompt.summary : "No prompt found.";
    }


   public void Completed()
    {
        truthOrDareText.text = "";
        truthOrDare.SetActive(false);
        completeOrFailButton.SetActive(false);
        truthOrDareButton.SetActive(false);

        if (playerTurn == 1)
            playerAttackTimeline.PlayPlayer1DodgeTimeline(EndTurn);
        else
            playerAttackTimeline.PlayPlayer2DodgeTimeline(EndTurn);
    }

    public void Failed()
    {
        truthOrDareText.text = "";
        completeOrFailButton.SetActive(false);
        truthOrDare.SetActive(false);
        truthOrDareButton.SetActive(false);

        int damage = GetDamageForLevel(selectedLevel);

        if (playerTurn == 1)
        {
            playerAttackTimeline.PlayPlayer2AttackTimeline(EndTurn);
            player1Health.TakeDamage(damage);
        }
        else
        {
            playerAttackTimeline.PlayPlayer1AttackTimeline(EndTurn);
            player2Health.TakeDamage(damage);
        }
    } 

    private void EndTurn()
    {
        playerTurn = playerTurn == 1 ? 2 : 1;
        levelSelectButtons.SetActive(true);
    }

    private int GetDamageForLevel(int level)
    {
        switch (level)
        {
            case 1: return 30;
            case 2: return 20;
            case 3: return 10;
            default: return 20;
        }
    }

    private void HideGameplayUI()
    {
        truthOrDareText.text = "";
        levelSelectButtons.SetActive(false);
        truthOrDareButton.SetActive(false);
        completeOrFailButton.SetActive(false);
    }


    public void GameOver()
    {
        ui.uiPanel.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }
}