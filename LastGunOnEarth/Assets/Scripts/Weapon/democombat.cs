using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class democombat : MonoBehaviour
{
    public Animator anim;
    GameObject Weapons, currentWeapon_slot, currentWeapon_gun;
    Animator currentWeapon_animator;
    public Rigidbody rb;
    float currentSpeed;
    Camera maincam;
    float default_FOV;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Weapons = GameObject.FindWithTag("Weapon_bag");
        maincam = GetComponentInChildren<Camera>();
        default_FOV = maincam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = rb.velocity.magnitude;

        for (int i = 0; i < 3; i++)
        {
            //Lấy animator của vũ khí hiện tại
            if (Weapons.transform.GetChild(i).gameObject.activeSelf)
            {
                currentWeapon_slot = Weapons.transform.GetChild(i).gameObject; //Lấy loại vũ khí hiện tại (1,2,3)
                for (int j = 0; j < currentWeapon_slot.transform.childCount; j++)
                {
                    if (currentWeapon_slot.transform.GetChild(j).gameObject.activeSelf)
                    {
                        currentWeapon_gun = currentWeapon_slot.transform.GetChild(j).gameObject; //Lấy vũ khí hiện tại đang được active
                        currentWeapon_animator = currentWeapon_gun.GetComponent<Animator>(); //Lấy ra animator của vũ khí hiện tại
                    }
                }
            }
        }

        //Test ADS
        if (Input.GetMouseButtonDown(1))
        {
            currentWeapon_animator.SetTrigger("startADS");
            currentWeapon_animator.ResetTrigger("stopADS");
            currentWeapon_animator.SetBool("keepADS", true);
            maincam.fieldOfView = 40;
            if (Input.GetMouseButton(0))
            {
                currentWeapon_animator.SetTrigger("startShooting");
                currentWeapon_animator.ResetTrigger("stopShooting");
            }
            if (Input.GetMouseButtonUp(0))
            {
                currentWeapon_animator.SetTrigger("stopShooting");
                currentWeapon_animator.ResetTrigger("startShooting");
            }

        }
        if (Input.GetMouseButtonUp(1))
        {
            currentWeapon_animator.SetTrigger("stopADS");
            currentWeapon_animator.ResetTrigger("startADS");
            currentWeapon_animator.SetBool("keepADS", false);
            maincam.fieldOfView = default_FOV;
        }
        if (Input.GetMouseButton(0))
        {
            currentWeapon_animator.SetTrigger("startShooting");
            currentWeapon_animator.ResetTrigger("stopShooting");
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentWeapon_animator.SetTrigger("stopShooting");
            currentWeapon_animator.ResetTrigger("startShooting");
        }

        if (Input.GetKeyDown(KeyCode.R)){
            currentWeapon_animator.SetTrigger("startReload");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentWeapon_animator.ResetTrigger("startReload");
        }
    }
}
