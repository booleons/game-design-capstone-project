using UnityEngine;

public class enemy_attacks : MonoBehaviour
{
    private game_manager gameState;

    public Rigidbody2D projectile;
    public Transform player;

    public float speed = 4;
    public Vector2 offset;

    public enum attackPattern { none, test, aimed, sweep, flood };
    public attackPattern currentPattern = attackPattern.none;
    public attackPattern lastPattern;
    public enum attackBehavior { boss, head, gunner };
    public attackBehavior behavior;

    public float cooldown = 0.1f;
    public float cooldownTimer;
    public float burst;
    public float shots_taken;
    public float attack_cooldown = 1f;

    public Vector2 bullet_offset;

    public float angle = 0f;
    public float turn = 20;

    public float direction = 1;

    void Start()
    {
        gameState = GameObject.Find("game").GetComponent<game_manager>();
        player = GameObject.Find("player(Clone)").GetComponent<Transform>();
    }

    void Update()
    {
        if (cooldownTimer <= attack_cooldown)
        {
            cooldownTimer += Time.deltaTime;
        }

        if (gameState.currentState == game_manager.state.active)
        {
            switch (behavior)
            {
                case attackBehavior.boss:
                    bossManager();
                    break;
                case attackBehavior.head:
                    sentryManager();
                    break;
                case attackBehavior.gunner:
                    gunnerManager();
                    break;
            }

            if (cooldownTimer >= cooldown && currentPattern != attackPattern.none)
            {
                cooldownTimer = 0;
                shots_taken += 1;

                switch (currentPattern)
                {
                    case attackPattern.test:
                        testPattern();
                        break;
                    case attackPattern.aimed:
                        aimedShot();
                        break;
                    case attackPattern.sweep:
                        sweep();
                        break;
                    case attackPattern.flood:
                        flood();
                        break;
                }
            }
        }
    }

    void testPattern()
    {
        float rads = angle * Mathf.Deg2Rad;
        makeBullet(rads);

        rads = (angle + 90) * Mathf.Deg2Rad;
        makeBullet(rads);

        rads = (angle + 180) * Mathf.Deg2Rad;
        makeBullet(rads);

        rads = (angle + 270) * Mathf.Deg2Rad;
        makeBullet(rads);


        angle += turn;
    }

    void aimedShot()
    {
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x);
        makeBullet(angle);
    }

    void sweep()
    {
        float rads = angle * Mathf.Deg2Rad;
        makeBullet(rads);

        if (angle >= 0 || angle <= -180)
        {
            turn = turn * -1;
        }
        angle += turn;
    }

    void flood()
    {
        float rads;
        if (shots_taken < (burst / 3))
        {
            for (int i = 0; i <= 180; i += 10)
            {
                if (i >= 60)
                {
                    rads = (angle + i) * Mathf.Deg2Rad;
                    makeBullet(rads);
                }
            }
        }
        else if (shots_taken < (burst / 3) * 2)
        {
            for (int i = 0; i <= 180; i += 10)
            {
                if (i < 60 || i > 120)
                {
                    rads = (angle + i) * Mathf.Deg2Rad;
                    makeBullet(rads);
                }
            }
        }
        else
        {
            for (int i = 0; i <= 180; i += 10)
            {
                if (i <= 120)
                {
                    rads = (angle + i) * Mathf.Deg2Rad;
                    makeBullet(rads);
                }
            }
        }
    }

    void makeBullet(float bulletRotation)
    {

        Vector3 bulletpos = transform.position;

        Rigidbody2D bullet = Instantiate(projectile, bulletpos, transform.rotation, this.transform.parent);

        Vector2 velocity = new Vector2(Mathf.Cos(bulletRotation) * speed, Mathf.Sin(bulletRotation) * speed);
        bullet.linearVelocity = velocity;
        bullet.GetComponent<bulletCollisions>().target = "Player";
        bullet.GetComponent<bulletCollisions>().velocity = velocity;


    }

    void bossManager()
    {
        if ((cooldownTimer >= attack_cooldown) && (currentPattern == attackPattern.none))
        {
            if (lastPattern == attackPattern.sweep)
            {
                angle = -180;
                burst = 100;
                currentPattern = attackPattern.flood;
            }
            else
            {
                angle = 0;
                turn = 20;
                burst = 40;
                currentPattern = attackPattern.sweep;
            }
        }
        else
        {
            if (shots_taken == burst)
            {
                shots_taken = 0;
                lastPattern = currentPattern;
                currentPattern = attackPattern.none;
            }
        }
    }

    void sentryManager()
    {
        if (currentPattern == attackPattern.none && (cooldownTimer >= attack_cooldown))
        {
            currentPattern = attackPattern.test;
        }
        else
        {
            if (shots_taken == burst)
            {
                shots_taken = 0;
                currentPattern = attackPattern.none;
            }
        }
    }

    void gunnerManager()
    {
        if (currentPattern == attackPattern.none && (cooldownTimer >= attack_cooldown))
        {
            currentPattern = attackPattern.aimed;
        }
        else
        {

            if (shots_taken == burst)
            {
                shots_taken = 0;
                currentPattern = attackPattern.none;
            }
        }
    }
}
