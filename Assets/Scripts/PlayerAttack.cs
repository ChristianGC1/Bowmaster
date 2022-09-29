using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] SpriteRenderer ArrowGFX;
    [SerializeField] Slider BowPowerSlider;
    [SerializeField] Transform Bow;
    [SerializeField] private AudioSource bowPullSoundEffect;
    [SerializeField] private AudioSource bowReleaseSoundEffect;
    public AudioClip bowPullSoundClip;
    public bool alreadyPlayed = false;

    [Range(0, 10)]
    [SerializeField] float BowPower;

    [Range(0, 3)]
    [SerializeField] float MaxBowCharge;

    float BowCharge;
    bool CanFire = true;

    private void Start()
    {
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = MaxBowCharge;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && CanFire)
        {
            if (!alreadyPlayed)
            {
                bowPullSoundEffect.PlayOneShot(bowPullSoundClip);
                alreadyPlayed = true;
            }
            ChargeBow();
        }
        else if (Input.GetMouseButtonUp(0) && CanFire)
        {
            FireBow();
            alreadyPlayed = false;
        }
        else
        {
            if (BowCharge > 0f)
            {
                BowCharge -= 2f * Time.deltaTime;
            }
            else
            {
                BowCharge = 0f;
                CanFire = true;
            }

            BowPowerSlider.value = BowCharge;
        }
    }

    void ChargeBow()
    {
        ArrowGFX.enabled = true;

        BowCharge += 2.5f * Time.deltaTime;

        BowPowerSlider.value = BowCharge;

        if (BowCharge > MaxBowCharge)
        {
            BowPowerSlider.value = MaxBowCharge;
        }
    }

    void FireBow()
    {
        if (BowCharge > MaxBowCharge) BowCharge = MaxBowCharge;

        float ArrowSpeed = BowCharge + BowPower * 2f;
        float ArrowDamage = BowCharge * BowPower;

        float angle = Utility.AngleTowardsMouse(Bow.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        Arrow Arrow = Instantiate(ArrowPrefab, Bow.position, rot).GetComponent<Arrow>();
        Arrow.ArrowVelocity = ArrowSpeed;
        Arrow.ArrowDamage = ArrowDamage;

        bowReleaseSoundEffect.Play();

        CanFire = false;
        ArrowGFX.enabled = false;
    }
}
