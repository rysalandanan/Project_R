using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerTankStatus : MonoBehaviour
{
    private MainGunShell shell;
    private bool canMove = true;
    private bool isLeftTrackHit = false;
    private bool isRightTrackHit = false;
    [Header ("Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;

    [Header("Tank Internal")]
    [SerializeField] private GameObject tankInternal;
    [SerializeField] private SpriteRenderer tankTrackLeft;
    [SerializeField] private SpriteRenderer tankTrackRight;
    [SerializeField] private SpriteRenderer tankTransmission;
    [SerializeField] private SpriteRenderer tankEngine;
    

    [Header("Tank Crew")]
    [SerializeField] private GameObject[] tankCrew;
    [SerializeField] private SpriteRenderer tankCrewDriver;
    [SerializeField] private SpriteRenderer tankCrewRadioOperator;
    [SerializeField] private SpriteRenderer tankCrewLoader;
    [SerializeField] private SpriteRenderer tankCrewGunner;
    [SerializeField] private SpriteRenderer tankCrewCommander;
    private bool isDriverDead = false;
    private bool isRadioOperatorDead = false;
    private bool isLoaderDead = false;
    private bool isGunnerDead = false;
    private bool isCommanderDead = false;

    private Color hitColor = new(0.97f, 0.27f, 0.38f, 0.27f);
    private string statusReport;
    private const float mobility_Kill_Chance = 30f;
    private bool tankHit = false;

    private void Start()
    {
        tankInternal.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            tankInternal.SetActive(true);
            for(int i = 0; i < tankCrew.Length; i++)
            {
                tankCrew[i].SetActive(true);
            }
        }
        else
        {
            tankInternal.SetActive(false);
            for (int i = 0; i < tankCrew.Length; i++)
            {
                tankCrew[i].SetActive(false);
            }
        }
    }
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
        statusReport = "";
        float randomOutcome = Random.Range(0f, 100f);
        if (randomOutcome <= mobility_Kill_Chance)
        {
            // 30% chance for mobility kill
            if (shell.hitTag == "FrontArmor")
            {
                statusReport = "Driver: Transmission's taken a hit! We're losing gears!";
                tankTransmission.color = hitColor;
            }
            else if (shell.hitTag == "SideArmorR" || shell.hitTag == "SideArmorL")
            {
                statusReport = "Driver: They've hit our tracks! We're stuck here!";
                if (shell.hitTag == "SideArmorR")
                {
                    tankTrackRight.color = hitColor;
                }
                else if (shell.hitTag == "SideArmorL")
                {
                    tankTrackLeft.color = hitColor;
                }
            }
            else if (shell.hitTag == "RearArmor")
            {
                statusReport = "Driver: We've lost engine power! We're immobilized!";
                tankEngine.color = hitColor;
            }
            canMove = false;
        }
        else if (randomOutcome <= 45f)
        {
            // 15% chance for one of the crew to be killed
            float randomCrewPick = Random.Range(0, 15);
            if (randomCrewPick <= 3 && !isDriverDead)
            {
                //Driver dead (3%)
                statusReport = "Radio Operator: We've lost the driver!";
                tankCrewDriver.color = hitColor;
                canMove = false;
                isDriverDead = true;
            }
            else if (randomCrewPick <= 6 && !isRadioOperatorDead)
            {
                //Radio operator dead (3%)
                statusReport = "Driver: Radio operator's gone! Communication's shot!";
                tankCrewRadioOperator.color = hitColor;
                isRadioOperatorDead = true;
            }
            else if (randomCrewPick <= 9 && !isLoaderDead)
            {
                //Loader dead (3%)
                statusReport = "Commander: Loader is dead! loading speed decrease";
                tankCrewLoader.color = hitColor;
                isLoaderDead = true;
                //reload speed decrease
            }
            else if (randomCrewPick <= 12 && !isGunnerDead)
            {
                //Gunner dead (3%)
                statusReport = "Commander: Gunner is dead! Gun not operational";
                tankCrewGunner.color = hitColor;
                isGunnerDead = true;
                //turret unoperational
                //Once replaced by the commander, turret rotation speed decrease
            }
            else if (randomCrewPick <= 15 && !isCommanderDead)
            {
                //Commander dead (3%)
                statusReport = "Loader: Commander is dead!";
                tankCrewCommander.color = hitColor;
                isCommanderDead = true;
            }
        }
        else if(randomOutcome <= 50)
        {
            //5% chance to hit the ammo rack
            //Death of the tank
            Debug.Log("hit ammo");
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
        tankHit = false;
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
    public bool IsGunnerDead()
    {
        return isGunnerDead;
    }
}

