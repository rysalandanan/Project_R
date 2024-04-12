using UnityEngine;

public class EnemyTankStatus : MonoBehaviour
{
    private MainGunShell shell;
    private bool canMove = true;

    private const float mobility_Kill_Chance = 30f;
    private bool tankHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shell") && !tankHit)
        {
            shell = collision.gameObject.GetComponent<MainGunShell>();
            shell.gameObject.SetActive(false);
            RandomArmorHitEffect();
            tankHit = true;
        }
    }
    private void RandomArmorHitEffect()
    {
        float randomOutcome = Random.Range(0f, 100f);
        if (randomOutcome <= mobility_Kill_Chance)
        {
            // 30% chance for mobility kill
            canMove = false;
        }
        else if (randomOutcome <= 45f)
        {
            // 15% chance for one of the crew to be killed
            float randomCrewPick = Random.Range(0, 15);
            if (randomCrewPick <= 3)
            {
                //Driver dead (3%)
                canMove = false;
            }
            else if (randomCrewPick <= 6)
            {
                //Radio operator dead (3%)
            }
            else if (randomCrewPick <= 9)
            {
                //Loader dead (3%)
                //reload speed decrease
            }
            else if (randomCrewPick <= 12)
            {
                //Gunner dead (3%)
                //turret unoperational
                //Once replaced by the commander, turret rotation speed decrease
            }
            else if (randomCrewPick <= 15)
            {
                //Commander dead (3%)
            }
        }
        else if (randomOutcome <= 50)
        {
            //5% chance to hit the ammo rack
            //Death of the tank
            Debug.Log("hit ammo");
        }

    }
    public bool CanMove()
    {
        return canMove;
    }
}
