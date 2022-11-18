using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    private BulletMananger bulletMananger;
    private Player player;
    private int playerMoney;
    private bool canCollect = false, playerInRange = false, fullMagazineCapacity = true;
    private Animator animator;

    [SerializeField] private int cost = 80;
    [SerializeField] private Text ammoText;
    [SerializeField] private GameObject bulletPackage;

    // Start is called before the first frame update
    void Start()
    {
        bulletMananger = GameObject.Find("/GameMananger").GetComponent<BulletMananger>();
        player = GameObject.Find("/VR Rig").GetComponent<Player>();
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletMananger.GetExtraMagazines() >= 4)
        {
            fullMagazineCapacity = true;
        }
        else
        {
            fullMagazineCapacity = false;
        }

        BuyAmmo();
    }

    private void BuyAmmo()
    {
        if (playerInRange)
        {
            playerMoney = player.GetCurrentMoney();
            if (playerMoney >= cost && !canCollect && !fullMagazineCapacity)
            {
                ammoText.color = Color.white;
                ammoText.text = "Press 'Y' or 'B'\n" + playerMoney + "/" + cost;

                if (OVRInput.GetDown(OVRInput.Button.Four) || OVRInput.GetDown(OVRInput.Button.Two))
                {
                    // animate to open Box
                    animator.SetBool("OpenBox", true);

                    player.TakeMoney(-cost);

                    bulletPackage.SetActive(true);
                    canCollect = true;
                }
            }
            else if (fullMagazineCapacity)
            {
                ammoText.color = Color.red;
                ammoText.text = "Magazine Capacity \n" + "is Full!!";
            }
            else if (canCollect)
            {
                ammoText.color = Color.white;
                ammoText.text = "Press 'X' or 'A'\n" + "to collect";

                if (OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.One))
                {
                    bulletPackage.SetActive(false);
                    canCollect = false;

                    if(SimpleShoot.holdingGun) bulletMananger.SetExtraMagazines(6);
                    else bulletMananger.SetExtraMagazines(4);

                    // animate to close box
                    animator.SetBool("OpenBox", false);
                }
            }
            else
            {
                ammoText.color = Color.red;
                ammoText.text = playerMoney + "/" + cost;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        playerInRange = false;
        ammoText.text = "";
    }
}
