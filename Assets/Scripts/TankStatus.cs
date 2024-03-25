using System.Collections;
using TMPro;
using UnityEngine;


public class TankStatus : MonoBehaviour
{
    private Shell shell;
    private bool canMove = true;
    private bool isGunnerAlive = true;
    private bool isLeftTrackHit = false;
    private bool isRightTrackHit = false;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;

    [Header("Tank Internal")]
    [SerializeField] private GameObject tankInternal;
    [SerializeField] private SpriteRenderer tankTrackLeft;
    [SerializeField] private SpriteRenderer tankTrackRight;
    [SerializeField] private SpriteRenderer tankTransmission;
    [SerializeField] private SpriteRenderer tankEngine;
    private Color damageColor = new Color(0.97f, 0.27f, 0.38f, 0.27f);

    private string statusReport;
    private const float mobility_Kill_Chance = 30f;

    private void Start()
    {
        tankInternal.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            tankInternal.SetActive(true);
        }
        else
        {
            tankInternal.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shell"))
        {
            shell = collision.gameObject.GetComponent<Shell>();
            switch (shell.hitTag)
            {
                case "FrontArmor":
                    Debug.Log("Shell hit front armor!");
                    break;
                case "SideArmorR":
                    Debug.Log("Shell hit right side armor!");
                    break;
                case "SideArmorL":
                    Debug.Log("Shell hit left side armor!");
                    break;
                case "RearArmor":
                    Debug.Log("Shell hit rear armor!");
                    break;
                default:
                    Debug.Log("Shell hit unknown part of the tank!");
                    break;
            }
            RandomArmorHitEffect();
        }
    }
    private void RandomArmorHitEffect()
    {
        float randomOutcome = Random.Range(0f, 100f);
        if (randomOutcome <= mobility_Kill_Chance)
        {
            // 30% chance for mobility kill
            if (shell.hitTag == "FrontArmor")
            {
                canMove = false;
                statusReport = "Driver: Transmission's taken a hit! We're losing gears!";
                tankTransmission.color = damageColor;
            }
            else if (shell.hitTag == "SideArmorR" || shell.hitTag == "SideArmorL")
            {
                statusReport = "Driver: They've hit our tracks! We're stuck here!";
                if (shell.hitTag == "SideArmorR")
                {
                    tankTrackRight.color = damageColor;
                    isRightTrackHit = true;
                }
                else if (shell.hitTag == "SideArmorL")
                {
                    tankTrackLeft.color = damageColor;
                    isLeftTrackHit = true;
                }
            }
            else if (shell.hitTag == "RearArmor")
            {
                canMove = false;
                statusReport = "Driver: We've lost engine power! We're immobilized!";
                tankEngine.color = damageColor;
            }
        }
        else if (randomOutcome <= 45f)
        {
            // 15% chance for one of the crew to be killed
            float randomCrewPick = Random.Range(0, 15);
            if (randomCrewPick <= 3)
            {
                //Driver dead (3%)
                statusReport = "Radio Operator: We've lost the driver!";
                canMove = false;
            }
            else if (randomCrewPick <= 6)
            {
                //Radio operator dead (3%)
                statusReport = "Driver: Radio operator's gone! Communication's shot!";
            }
            else if (randomCrewPick <= 9)
            {
                //Loader dead (3%)
                statusReport = "Commander: Loader is dead! loading speed decrease";
                //reload speed decrease
            }
            else if (randomCrewPick <= 12)
            {
                //Gunner dead (3%)
                statusReport = "Commander: Gunner is dead! Gun not operational";
                isGunnerAlive = false;
                //turret unoperational
                //Once replaced by the commander, turret rotation speed decrease
            }
            else if (randomCrewPick <= 15)
            {
                //Commander dead (3%)
                statusReport = "Loader: Commander is dead!";
            }

        }
        else if(randomOutcome <= 50)
        {
            Debug.Log("Hit the ammo rack!");
            //5% chance to hit the ammo rack
            //Death of the tank
        }
        StartCoroutine(ReportStatus());
        CheckTrackStatus();
    }
    private IEnumerator ReportStatus()
    {
        dialogueText.text = "";
        dialogueBox.SetActive(true);
        foreach (char c in statusReport)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.5f);
        dialogueBox.SetActive(false);
    }
    private void CheckTrackStatus()
    {
        //can't move if both tracks are disabled
        if(isLeftTrackHit && isRightTrackHit)
        {
            canMove = false;
        }
    }
    public bool CanMove()
    {
        return canMove;
    }
    public bool IsGunnerAlive()
    {
        return isGunnerAlive;
    }
    public bool IsLeftTrackHit()
    {
        return isLeftTrackHit;
    }
    public bool IsRightTrackHit()
    {
        return isRightTrackHit;
    }    
}

