using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    public Ghost[] ghosts;

    public Pacman pacman;

    public Transform pellets;

    public int score { get; private set; }

    public int lives { get; private set; }

    public int ghostMultiplier { get; private set; } = 1;

    // Variables pour l'UI
    public Text scoreText;          // Texte du score
    public Transform livesLayout;   // Layout contenant les vies (objets "Lives")
    public GameObject livesText;              // Texte des vies

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        
        SetScore(0);
        SetLives(3);
        NewRound();
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

        ResetState();


    }

    private void ResetState()
    {

        ResetGhostMultiplier();
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
        scoreText.text = this.score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;

        // Supprime toutes les vies affichées
        foreach (Transform child in livesLayout)
        {
            Destroy(child.gameObject);
        }

        // Ajoute les nouvelles vies à afficher
        for (int i = 0; i < this.lives; i++)
        {
            GameObject life = Instantiate(livesText, livesLayout);

            // Assure une taille correcte pour le texte
            RectTransform rectTransform = life.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // Définis une taille par défaut (par exemple, 50x50)
                rectTransform.sizeDelta = new Vector2(50, 50);
            }

        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points );
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

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for(int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(powerPellet.duration);
        }


        PelletEaten(powerPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration); 

    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
