using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject player1_go, player2_go, endGamePanel;
    PlayerController player1, player2;
    // Start is called before the first frame update
    void Start()
    {
        player1 = player1_go.GetComponent<PlayerController>();
        player2 = player2_go.GetComponent<PlayerController>();

        //endGamePanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener( () => Application.Quit());
        //endGamePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.life <= 0)
        {
            endGamePanel.transform.GetChild(2).GetComponent<Text>().text = "Player 2 is the Winner !";
            endGamePanel.SetActive(true);
        }
        else if (player2.life <= 0)
        {
            endGamePanel.transform.GetChild(2).GetComponent<Text>().text = "Player 1 is the Winner !";
            endGamePanel.SetActive(true);
        }

    }
}
