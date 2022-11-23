using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    [Header("Intro")]
    public Camera main_camera;
    private float FOV;
    GameObject Weapons, UI;
    public GameObject spider_cut, body_cut, bodies_cut, gun_cut, endpoint_cut;
    public Transform spider_transform, body_transform, bodies_transform, gun_transform, endpoint_transform;
    //public float rSpeed = 0.1f;
    RigidbodyConstraints originalConstraints;

    private Canvas img;

    bool isCoroutineStarted = false;
    bool doneZoom = false;

    //Timeline check
    bool bodies_looked = false;
    bool spider_looked = false;
    bool gun_looked = false;
    bool end_looked = false;
    void Start()
    {
        main_camera = Camera.main;
        FOV = main_camera.fieldOfView;

        main_camera.transform.Rotate(36f, -70f, 0f);

        spider_cut = GameObject.FindWithTag("cutscene_spider");
        body_cut = GameObject.FindWithTag("cutscene_body");
        bodies_cut = GameObject.FindWithTag("cutscene_bodies");
        gun_cut = GameObject.FindWithTag("cutscene_gun");
        endpoint_cut = GameObject.FindWithTag("cutscene_endpoint");
        img = GameObject.FindGameObjectWithTag("cutscene_eye").GetComponent<Canvas>();

        bodies_transform = bodies_cut.transform;
        spider_transform = spider_cut.transform;
        gun_transform = gun_cut.transform;
        endpoint_transform = endpoint_cut.transform;

        Weapons = GameObject.FindWithTag("Weapon_bag");
        UI = GameObject.FindWithTag("UI");
        Weapons.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        UI.SetActive(false);
        GetComponent<PlayerLook>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<WeaponBob>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoroutineStarted == false)
        {
            StartCoroutine(phase01());
        }
    }

    
    IEnumerator phase01()
    {
        

        //----------- Phase 01: Mở mắt, đợi vài giây ---
        img.GetComponent<Image>().CrossFadeAlpha(0.51f, 6.0f, false);
        yield return new WaitForSeconds(2);

        //----------- Phase 02: Nhìn về hướng các xác chết, zoom lại ---
        if (bodies_looked == false) 
        {
            lookAt(bodies_transform, 0.5f);
            
        }

        yield return new WaitForSeconds(4.5f);
        bodies_looked = true;
        if (main_camera.fieldOfView != 30 && doneZoom == false)
        {
            zoomIn(25f);
        }
        else doneZoom = true;

        //----------- Phase 03: Ngảnh lại nhìn nhện mẹ phía trên, nhìn xuống khẩu súng trên xác đồng đội phía dưới ---
        yield return new WaitForSeconds(3f);
        if (spider_looked == false)
        {
            lookAt(spider_transform, 4f);
        }

        yield return new WaitForSeconds(2.5f);
        spider_looked = true;

        yield return new WaitForSeconds(3f);
        if (gun_looked == false)
        {
            lookAt(gun_transform, 5f);
        }

        //----------- Phase 04: Nhặt khẩu súng lên, nhìn về phía trước, kết thúc ---
        yield return new WaitForSeconds(2.5f);
        gun_cut.SetActive(false);
        gun_looked = true;

        yield return new WaitForSeconds(1f);
        if (end_looked == false)
        {
            lookAt(endpoint_transform, 5f);
        }
        if (doneZoom == true) { zoomOut(80f); }
        yield return new WaitForSeconds(1f);
        end_looked = true;

        //Start: Hiện súng và giao diện cho người chơi, người chơi bắt đầu có thể di chuyển và nhìn
        yield return new WaitForSeconds(1f);
        Weapons.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        UI.SetActive(true);
        GetComponent<PlayerLook>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<WeaponBob>().enabled = true;
        isCoroutineStarted = true;
    }

    void lookAt(Transform target_transform, float rSpeed)
    {
        Vector3 lookDirection = target_transform.position - main_camera.transform.position;
        lookDirection.Normalize();
        main_camera.transform.rotation = Quaternion.Slerp(main_camera.transform.rotation, Quaternion.LookRotation(lookDirection), rSpeed * Time.deltaTime);
    }

    void zoomIn(float fov)
    {
        if (main_camera.fieldOfView > fov) main_camera.fieldOfView--;
        else if (main_camera.fieldOfView == fov) main_camera.fieldOfView = fov;
    }

    void zoomOut(float fov)
    {
        if (main_camera.fieldOfView < fov) main_camera.fieldOfView++;
        else if (main_camera.fieldOfView == fov) main_camera.fieldOfView = fov;
    }
}
