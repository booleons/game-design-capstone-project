using UnityEngine;

public class bulletCollisions : MonoBehaviour
{
    private game_manager gameState;

    public string target;
    private Rigidbody2D self;

    public Vector2 velocity;
    private Vector2 currentVelocity;

    void Start()
    {
        gameState = GameObject.Find("game").GetComponent<game_manager>();
        self = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameState.currentState == game_manager.state.active)
        {
            self.constraints = RigidbodyConstraints2D.None;
            self.linearVelocity = velocity;
        }
        else
        {
            self.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "wall" || other.gameObject.tag == target)
        {
            if (other.gameObject.tag == target)
            {
                other.gameObject.GetComponent<player_health>().damage();
            }
            Object.Destroy(this.gameObject);
        }

    }
}
