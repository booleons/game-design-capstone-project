using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class game_manager : MonoBehaviour
{
    public enum state { active, pause, menu, game_over }
    public state currentState;

    private Button start_button;
    private Button menu_exit_button;
    private Button credits_button;

    private Button resume_button;
    private Button pause_exit_button;

    public Image health01;
    public Image health02;
    public Image health03;

    private Button exit_credits_button;

    private Button restart_button;
    private Button end_exit_button;
    private TMPro.TextMeshProUGUI end_game_text;

    public GameObject room;

    public GameObject player;
    public GameObject enemy;

    public Sprite health_full;
    public Sprite health_half;
    public Sprite health_empty;

    private enemy_manager enemy_manager;

    void Start()
    {
        currentState = state.menu;
        enemy_manager = this.transform.GetComponent<enemy_manager>();

        start_button = GameObject.Find("start_game").GetComponent<Button>();
        start_button.onClick.AddListener(gameStart);

        menu_exit_button = GameObject.Find("menu_exit_game").GetComponent<Button>();
        menu_exit_button.onClick.AddListener(exitGame);

        credits_button = GameObject.Find("credits").GetComponent<Button>();
        credits_button.onClick.AddListener(openCredits);


        resume_button = GameObject.Find("resume_game").GetComponent<Button>();
        resume_button.onClick.AddListener(resumeGame);

        pause_exit_button = GameObject.Find("pause_exit_game").GetComponent<Button>();
        pause_exit_button.onClick.AddListener(exitGame);


        exit_credits_button = GameObject.Find("exit_credits").GetComponent<Button>();
        exit_credits_button.onClick.AddListener(exitCredits);


        restart_button = GameObject.Find("restart_game").GetComponent<Button>();
        restart_button.onClick.AddListener(gameStart);

        end_exit_button = GameObject.Find("menu_exit_game").GetComponent<Button>();
        end_exit_button.onClick.AddListener(exitGame);

        end_game_text = GameObject.Find("end_type").GetComponent<TMPro.TextMeshProUGUI>();

        resume_button.transform.parent.gameObject.SetActive(false);
        health01.transform.parent.gameObject.SetActive(false);
        restart_button.transform.parent.gameObject.SetActive(false);
        exit_credits_button.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseGame();
        }
    }

    void gameStart()
    {
        currentState = state.active;
        start_button.transform.parent.gameObject.SetActive(false);
        restart_button.transform.parent.gameObject.SetActive(false);
        health01.transform.parent.gameObject.SetActive(true);

        test_layout();

        Instantiate(player, new Vector2(0, -3), transform.rotation, this.transform);
        enemy_manager.gameStart();
    }

    public void test_layout()
    {
        GameObject new_room = Instantiate(room, this.transform);
    }

    void resumeGame()
    {
        currentState = state.active;
        resume_button.transform.parent.gameObject.SetActive(false);
    }

    void pauseGame()
    {
        currentState = state.pause;
        resume_button.transform.parent.gameObject.SetActive(true);
    }

    void exitGame()
    {
        Application.Quit();
    }

    public void endGame(string type)
    {
        restart_button.transform.parent.gameObject.SetActive(true);

        currentState = state.game_over;

        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        health01.sprite = health_full;
        health02.sprite = health_full;
        health03.sprite = health_full;
        health01.transform.parent.gameObject.SetActive(false);

        switch (type)
        {
            case "game over":
                end_game_text.text = type;
                break;
            case "win":
                end_game_text.text = type;
                break;
        }
    }

    void openCredits()
    {
        credits_button.transform.parent.gameObject.SetActive(false);
        exit_credits_button.transform.parent.gameObject.SetActive(true);
    }

    void exitCredits()
    {
        credits_button.transform.parent.gameObject.SetActive(true);
        exit_credits_button.transform.parent.gameObject.SetActive(false);
    }

    //probably the worst way to do this but the "smarter" ways i could think of don't work
    //fix later if there's time
    public void update_health(float health)
    {
        switch (health)
        {
            case 5:
                health03.sprite = health_half;
                break;
            case 4:
                health03.sprite = health_empty;
                break;
            case 3:
                health02.sprite = health_half;
                break;
            case 2:
                health02.sprite = health_empty;
                break;
            case 1:
                health01.sprite = health_half;
                break;
        }
    }
}
