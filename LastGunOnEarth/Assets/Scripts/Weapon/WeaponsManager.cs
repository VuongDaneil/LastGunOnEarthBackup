using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour
{
    public GameObject[] weapon_types = new GameObject[3];
    public GameObject[] rifle_list = new GameObject[5];
    public GameObject[] weapon_icons = new GameObject[3];

    public Animator Anim;
    int current_weapon;
    int rifle_no = 0;
    // Start is called before the first frame update
    void Start()
    {
        weapon_icons[0].GetComponent<Image>().color = new Color32(144, 125, 37, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeRifle(rifle_no);
            rifle_no++;
            if (rifle_no > 3)
            {
                rifle_no = rifle_no % 4;
            }
        }
    }

    void changeWeapon(int num)
    {
        current_weapon = num;
        for (int i = 0; i < 3; i++)
        {
            if (i == num)
            {
                weapon_icons[i].GetComponent<Image>().color = new Color32(144, 125, 37, 255);
                weapon_types[i].SetActive(true);
            }
            else
            {
                weapon_icons[i].GetComponent<Image>().color = new Color32(56, 56, 56, 255);
                weapon_types[i].SetActive(false);
            }
        }
    }
    void changeRifle(int num)
    {
        current_weapon = num;
        for (int i = 0; i < 4; i++)
        {
            if (i == num)
            {
                rifle_list[i].SetActive(true);
            }
            else
            {
                rifle_list[i].SetActive(false);
            }
        }
    }
}
