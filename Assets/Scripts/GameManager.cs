using UnityEngine;



public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    public Pacman pacman;

    public Transform pellets;

    private int ghostMultiplier = 1;

    public int score { get; private set; }

    public int lives { get; private set; }


    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(true);
        }

        this.pacman.gameObject.SetActive(true);
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + ghost.points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }
}
