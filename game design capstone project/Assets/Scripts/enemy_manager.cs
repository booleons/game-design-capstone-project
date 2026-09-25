using UnityEngine;

public class enemy_manager : MonoBehaviour
{
    public Transform enemy;
    public Transform boss;
    public Transform sentry;
    public Transform gunner;

    public int current_wave = -1;
    int starting_wave = 7;

    public int enemies_left = -1;

    private game_manager game_manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game_manager = this.transform.gameObject.GetComponent<game_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies_left == 0)
        {
            print(current_wave);
            switch (current_wave)
            {
                case 0:
                    game_manager.endGame("win");
                    enemies_left = -1;
                    break;
                case 1:
                    boss_wave();
                    break;
                case 2:
                    sixth_wave();
                    break;
                case 3:
                    fifth_wave();
                    break;
                case 4:
                    fourth_wave();
                    break;
                case 5:
                    third_wave();
                    break;
                case 6:
                    second_wave();
                    break;
                case 7:
                    first_wave();
                    break;
            }

            current_wave -= 1;
        }
    }

    public void gameStart()
    {
        enemies_left = 0;
        current_wave = starting_wave;
    }

    public void boss_wave()
    {
        Instantiate(boss, new Vector2(0, 4), this.transform.rotation, this.transform);
        enemies_left = 1;
    }

    public void sixth_wave()
    {
        Instantiate(sentry, new Vector2(-7, 0), this.transform.rotation, this.transform);
        Instantiate(sentry, new Vector2(7, 0), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(0, -3.5f), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(-3, 3), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(3, 3), this.transform.rotation, this.transform);

        enemies_left = 5;
    }

    public void fifth_wave()
    {
        Instantiate(sentry, new Vector2(-1.75f, -3), this.transform.rotation, this.transform);
        Instantiate(sentry, new Vector2(-4.5f, 2.5f), this.transform.rotation, this.transform);
        Instantiate(sentry, new Vector2(5.5f, 1.5f), this.transform.rotation, this.transform);

        enemies_left = 3;
    }

    public void fourth_wave()
    {
        Instantiate(gunner, new Vector2(-6, 0), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(6, 0), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(-3, 3), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(3, 3), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(0, 1.75f), this.transform.rotation, this.transform);

        enemies_left = 5;
    }

    public void third_wave()
    {
        Instantiate(sentry, new Vector2(0, -3), this.transform.rotation, this.transform);

        enemies_left = 1;
    }

    public void second_wave()
    {
        Instantiate(gunner, new Vector2(-5, -2.75f), this.transform.rotation, this.transform);
        Instantiate(gunner, new Vector2(5, 2.75f), this.transform.rotation, this.transform);

        enemies_left = 2;
    }

    public void first_wave()
    {
        Instantiate(gunner, new Vector2(0, 4), this.transform.rotation, this.transform);

        enemies_left = 1;
    }

    public void kill_enemy()
    {
        enemies_left -= 1;
    }
}
