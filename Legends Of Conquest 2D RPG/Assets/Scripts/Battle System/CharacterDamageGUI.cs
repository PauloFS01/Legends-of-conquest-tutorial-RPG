using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDamageGUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] float lifeTime = 1f, moveSpeed = 1f, textVivration = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime);
    }

    public void SetDamage(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        float jitterAmount = Random.Range(-textVivration, +textVivration);
        transform.position += new Vector3(jitterAmount, jitterAmount, 0f);
    }
}
