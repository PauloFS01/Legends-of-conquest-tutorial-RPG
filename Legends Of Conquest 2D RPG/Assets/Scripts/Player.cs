using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D palyerRigidbody;
    [SerializeField] Animator playerAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMoviment = Input.GetAxisRaw("Horizontal");
        float verticalMoviment = Input.GetAxisRaw("Vertical");

        palyerRigidbody.velocity = new Vector2(horizontalMoviment, verticalMoviment);

        playerAnimator.SetFloat("movementX", palyerRigidbody.velocity.x);
        playerAnimator.SetFloat("movementY", palyerRigidbody.velocity.y);

        if(horizontalMoviment == 1 || horizontalMoviment == -1 || verticalMoviment == 1 || verticalMoviment == -1)
        {
            playerAnimator.SetFloat("lastX", horizontalMoviment);
            playerAnimator.SetFloat("lastY", verticalMoviment);
        }

    }
}
