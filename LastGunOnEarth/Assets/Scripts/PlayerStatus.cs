using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private int current_life;
    private float current_stamina, max_stamina;

    public TMP_Text life_text, stamina_text, Ammo_text;
    public Image Stamina_bar;
    public PlayerMovement PlayerInfo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        current_stamina = PlayerInfo.getStamina();
        max_stamina = PlayerInfo.getMaxStamina();

        float fill_amount = current_stamina / max_stamina;
        Stamina_bar.fillAmount = fill_amount;
    }

}
