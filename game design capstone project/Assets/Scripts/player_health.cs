using UnityEngine;

public class player_health : MonoBehaviour
{
    public float health;

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            if (this.gameObject.tag == "Player")
            {
                this.gameObject.GetComponent<player_actions>().die();
            }
            else
            {
                GameObject.Find("game").GetComponent<enemy_manager>().kill_enemy();
                Object.Destroy(this.gameObject);

            }
        }
    }

    public void damage()
    {
        if (this.gameObject.tag != "Player" || !this.gameObject.GetComponent<player_actions>().isInvulnerable)
        {
            health -= 1;

            if (this.gameObject.tag == "Player")
            {
                this.transform.parent.gameObject.GetComponent<game_manager>().update_health(health);
            }
        }
    }
}
