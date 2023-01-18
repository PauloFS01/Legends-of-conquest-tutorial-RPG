using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    public static Player instance;

    public string transitionName;

    [SerializeField] int moveSpeed;

    [SerializeField] Rigidbody2D palyerRigidbody;
    [SerializeField] Animator playerAnimator;

    private Vector3 leftBottomEdge;
    private Vector3 rightTopEdge;

    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMoviment = Input.GetAxisRaw("Horizontal");
        float verticalMoviment = Input.GetAxisRaw("Vertical");

        palyerRigidbody.velocity = new Vector2(horizontalMoviment, verticalMoviment) * moveSpeed;

        playerAnimator.SetFloat("movementX", palyerRigidbody.velocity.x);
        playerAnimator.SetFloat("movementY", palyerRigidbody.velocity.y);

        if(horizontalMoviment == 1 || horizontalMoviment == -1 || verticalMoviment == 1 || verticalMoviment == -1)
        {
            playerAnimator.SetFloat("lastX", horizontalMoviment);
            playerAnimator.SetFloat("lastY", verticalMoviment);
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBottomEdge.x, rightTopEdge.x),
            Mathf.Clamp(transform.position.y, leftBottomEdge.y, rightTopEdge.y),
            Mathf.Clamp(transform.position.z, leftBottomEdge.z, rightTopEdge.z)
            );

    }

    public void SetLimt(Vector3 bottomEdgeToSet, Vector3 leftEdgeToSet)
    {
        leftBottomEdge = bottomEdgeToSet;
        rightTopEdge = leftEdgeToSet;
    }
}
