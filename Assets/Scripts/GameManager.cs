using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public UIManager ui;
    public int playerTurn = 1;
    public TextMeshProUGUI playerNumber;
    public TextMeshProUGUI truthOrDareText;

    // public GameObject tOrDButton;
    public GameObject completeOrFailButton;
    public GameObject truthOrDareButton;
    public TextFade fade;

    public Player1HealthScripts player1Health;
    public Player2HealthScripts player2Health;


    public PlayerAttackTimeline playerAttackTimeline;


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        completeOrFailButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerNumber.text = "PLAYER " + playerTurn;

        if (player2Health.health <= 0)
        {
            playerAttackTimeline.PlayPlayer1FinisherTimeline();
            GameOver();
        }
    }

    public void Truth()
    {
        completeOrFailButton.SetActive(true);
        truthOrDareButton.SetActive(false);
        if (playerTurn == 1)
        {
            truthOrDareText.text = "display some truth data";
        }
        else
        {
            truthOrDareText.text = "display some truth data";
        }
    }

    public void Dare()
    {
        completeOrFailButton.SetActive(true);
        truthOrDareButton.SetActive(false);
        if (playerTurn == 1)
        {
            truthOrDareText.text= "display some dare data";
        }
        else
        {
            truthOrDareText.text= "display some dare data";
        }
    }

    public void Completed()
    {
        truthOrDareText.text = "";
        completeOrFailButton.SetActive(false);
        if (playerTurn == 1)
        {
            playerAttackTimeline.PlayPlayer1DodgeTimeline();
            playerTurn = 2;
        }
        else
        {
            playerAttackTimeline.PlayPlayer2DodgeTimeline();
            playerTurn = 1;
        }
    }
//Vector3(3.74803948,2.61052036,-0.937654197)
    public void Failed()
    {
        truthOrDareText.text = "";
        completeOrFailButton.SetActive(false);
        if (playerTurn == 1)
        {
            playerAttackTimeline.PlayPlayer2AttackTimeline();
            player1Health.TakeDamage(20);
            playerTurn = 2;
        }
        else
        {
            playerAttackTimeline.PlayPlayer1AttackTimeline();
            player2Health.TakeDamage(20);
            playerTurn = 1;
        }
    }

    public void GameOver()
    {
        ui.uiPanel.SetActive(false);
        SceneManager.LoadScene("MainGame");
        Debug.Log("Game Over");
    }
}
