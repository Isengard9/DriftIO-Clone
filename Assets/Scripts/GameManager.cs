using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool isGameStarted = false;
    public static bool isGameEnded = false;


    [SerializeField] List<GameObject> Players = new List<GameObject>();

    [SerializeField] GameObject EndPanel;
    [SerializeField] Text EndLevelTitle;
    [SerializeField] GameObject RestartButton, NextLevelButton;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }


    
    void Start()
    {

    }

  
    void Update()
    {

    }

    /// <summary>
    /// Using for adding objects to the players list
    /// </summary>
    /// <param name="player"></param>
    public void AddToPlayerToPlayers(GameObject player)
    {
        Players.Add(player);
    }

    /// <summary>
    /// Returning randomly player
    /// If the random player is point of the main player try again
    /// </summary>
    /// <param name="finderPlayer"></param>
    /// <returns></returns>
    public GameObject ReturnRandomPlayer(GameObject finderPlayer)
    {
        GameObject randomPlayer = null;

        if (Players.Count > 1)
        {
            randomPlayer = Players[Random.Range(0, Players.Count)];
            if (randomPlayer == finderPlayer)
              randomPlayer = ReturnRandomPlayer(finderPlayer);
        }

        return randomPlayer;
    }

    /// <summary>
    /// Returns list of player count
    /// </summary>
    /// <returns></returns>
    public int PlayersCount() { return Players.Count; }

    /// <summary>
    /// If player is dead, remove from the list
    /// </summary>
    /// <param name="player"></param>
    public void RemovePlayerFromList(GameObject player)
    {
        if (Players.Contains(player))
            Players.Remove(player);

        ControlIsGameEnded();
    }

    /// <summary>
    /// End game if 1 player left
    /// </summary>
    public void ControlIsGameEnded()
    {
        if(Players.Count == 1)
        {
            OnGameEnded();
        }
    }

    /// <summary>
    /// used to start the game
    /// </summary>
    public void OnGameStarted()
    {
        isGameStarted = true;
    }

    /// <summary>
    /// called when the game is over and checks if the player has won the game
    /// </summary>
    public void OnGameEnded()
    {
        

        if (Players[0].GetComponent<CarMovement>().playerType == CarMovement.PlayerType.User)
            LevelSucceeded();
        else
            LevelFailed();
        EndPanel.SetActive(true);

        isGameEnded = true;
    }

    /// <summary>
    /// called when game failed
    /// </summary>
    public void LevelFailed()
    {
        EndLevelTitle.text = "You Failed";
        RestartButton.SetActive(true);
    }

    /// <summary>
    /// called when game successed
    /// </summary>
    public void LevelSucceeded()
    {
        EndLevelTitle.text = "You Win";
        NextLevelButton.SetActive(true);
    }

    /// <summary>
    /// used to load the next level
    /// </summary>
    public void TryAgain()
    {
        isGameEnded = false;
        isGameStarted = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
