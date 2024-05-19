using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public Dongle lastDongle;
    public GameObject donglePrefab;
    public Transform dongleGroup;
    public GameObject effectPrefab;
    public Transform effectGroup;

    public int maxlevel;
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        NextDongle();
    }

    Dongle GetDongle()
    {
        GameObject instantEffectObj = Instantiate(effectPrefab, effectGroup);
        ParticleSystem instantEffect = instantEffectObj.GetComponent<ParticleSystem>();

        GameObject instantDongleObj = Instantiate(donglePrefab, dongleGroup);
        Dongle instantDongle = instantDongleObj.GetComponent<Dongle>();
        instantDongle.effect = instantEffect;
        return instantDongle;
    }
    void NextDongle()
    {
        Dongle newDongle = GetDongle();
        lastDongle = newDongle;
        lastDongle.manager = this;
        lastDongle.level = Random.Range(0, maxlevel);
        lastDongle.gameObject.SetActive(true);
        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext()
    {

        while (lastDongle != null)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);

        NextDongle();
    }
    public void TouchDown()
    {
        if(lastDongle != null)
        {
            lastDongle.Drag();
        }
    }
    public void TouchUp()
    {
        if(lastDongle != null)
        {
            lastDongle.Drop();
            lastDongle = null;
        }
    }
}
