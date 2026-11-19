using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Referensi")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image fillImage;

    void Update()
    {
        if (playerHealth != null)
        {
            int hp = playerHealth.GetCurrentHp();
            int hpMax = playerHealth.GetMaxHp();

            if (hpBar != null)
            {
                hpBar.maxValue = hpMax;
                hpBar.value = hp;
            }

            if (hpText != null)
                hpText.text = $"{hp} / {hpMax}";

            if (fillImage != null && hpMax > 0)
            {
                float persen = (float)hp / hpMax;

                if (persen > 0.5f)
                    fillImage.color = Color.green;
                else if (persen > 0.25f)
                    fillImage.color = Color.yellow;
                else
                    fillImage.color = Color.red;
            }
        }
    }
}