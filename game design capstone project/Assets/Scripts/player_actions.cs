using UnityEngine;
using UnityEngine.InputSystem;

public class player_actions : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody2D self;

    public bool isInvulnerable = false;
    public float dodgeSpeed = 30f;
    public float dodgeLength = 20f;
    public float dodgeCooldown = 30f;
    public float dodgeTimer = 0f;

    public Rigidbody2D projectile;
    public float bulletSpeed = 10f;

    private game_manager gameState;

    public GameObject room;

    void Start()
    {
        self = GetComponent<Rigidbody2D>();
        gameState = this.transform.parent.gameObject.GetComponent<game_manager>();

        dodgeTimer = dodgeCooldown;
    }

    void Update()
    {
        if (gameState.currentState == game_manager.state.active)
        {
            Vector2 movementDirection = new Vector2(0, 0);

            if (isInvulnerable)
            {
                if (dodgeTimer >= dodgeLength)
                {
                    dodgeToggle();
                }

                if (self.linearVelocity.x != 0)
                {
                    movementDirection.x = self.linearVelocity.x / Mathf.Abs(self.linearVelocity.x);
                }
                if (self.linearVelocity.y != 0)
                {
                    movementDirection.y = self.linearVelocity.y / Mathf.Abs(self.linearVelocity.y);
                }

                MoveBody(movementDirection, dodgeSpeed);
                dodgeTimer += Time.deltaTime;

            }
            else
            {
                if (dodgeTimer <= dodgeCooldown)
                {
                    dodgeTimer += Time.deltaTime;
                }

                movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                MoveBody(movementDirection, movementSpeed);

                if (Input.GetButtonDown("Fire1"))
                {
                    shootBullet();
                }

                if (Input.GetButtonDown("Jump") && dodgeTimer >= dodgeCooldown)
                {
                    dodgeToggle();
                }
            }
        }
    }

    void shootBullet()
    {
        Vector3 bulletpos = transform.position;

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float bulletRotation = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x);


        Rigidbody2D bullet = Instantiate(projectile, bulletpos, transform.rotation, this.transform);
        //Rigidbody2D bullet = Instantiate(projectile, bulletpos, bulletRotation, this.transform);

        Vector2 velocity = new Vector2(Mathf.Cos(bulletRotation) * bulletSpeed, Mathf.Sin(bulletRotation) * bulletSpeed);

        //bullet.AddForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);

        bullet.linearVelocity = velocity;
        bullet.GetComponent<bulletCollisions>().target = "enemy";
        bullet.GetComponent<bulletCollisions>().velocity = bullet.linearVelocity;
    }

    public void MoveBody(Vector2 movementDirection, float speed)
    {
        self.linearVelocity = movementDirection * speed;
    }

    public void die()
    {
        gameState.endGame("game over");
    }

    private void dodgeToggle()
    {
        if (isInvulnerable)
        {
            isInvulnerable = false;

        }
        else
        {
            isInvulnerable = true;
            dodgeTimer = 0;
        }
    }
}
