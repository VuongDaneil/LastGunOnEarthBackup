using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public Animator Gunanim;
    GameObject Weapons, currentWeapon_slot, currentWeapon_gun;
    Animator currentWeapon_animator;
    public Rigidbody rb;
    float currentSpeed;
    Camera maincam;
    float default_FOV;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Weapons = GameObject.FindWithTag("Weapon_bag");
        maincam = GetComponentInChildren<Camera>();
        default_FOV = maincam.fieldOfView;
    }
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
                        currentWeapon_animator = currentWeapon_gun.GetComponent<Animator>(); //Lấy ra animator cuar vũ khí hiện tại
                    }
                }
            }
        }

        //Animation khi chạy
        if (currentSpeed > 18.5f)
        {
            currentWeapon_animator.SetTrigger("startbob");
            currentWeapon_animator.ResetTrigger("stopbob");
            currentWeapon_animator.SetTrigger("startrun");
            currentWeapon_animator.ResetTrigger("stoprun");
            currentWeapon_animator.SetBool("runLoop", true);
            print(currentSpeed);
        }
        if (currentSpeed < 18.5f)
        {
            currentWeapon_animator.SetTrigger("stopbob");
            currentWeapon_animator.ResetTrigger("startbob");
            currentWeapon_animator.SetTrigger("stoprun");
            currentWeapon_animator.ResetTrigger("startrun");
            currentWeapon_animator.SetBool("runLoop", false);
        }
        //Animation đi bộ
        if (currentSpeed > 10f)
        {
            currentWeapon_animator.SetTrigger("startbob");
            currentWeapon_animator.ResetTrigger("stopbob");
        }
        if (currentSpeed < 10f)
        {
            currentWeapon_animator.SetTrigger("stopbob");
            currentWeapon_animator.ResetTrigger("startbob");
        }
    }
}
