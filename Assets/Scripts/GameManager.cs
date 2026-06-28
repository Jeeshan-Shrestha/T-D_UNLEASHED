using TMPro;
using UnityEngine;

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


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        completeOrFailButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerNumber.text = "PLAYER " + playerTurn;
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
        StartCoroutine(fade.FadeRoutine());
        if (playerTurn == 1)
        {
            playerTurn = 2;
        }
        else
        {
            playerTurn = 1;
        }
    }

    public void Failed()
    {
        truthOrDareText.text = "";
        completeOrFailButton.SetActive(false);
        StartCoroutine(fade.FadeRoutine());
        if (playerTurn == 1)
        {
            playerTurn = 2;
        }
        else
        {
            playerTurn = 1;
        }
    }
}
