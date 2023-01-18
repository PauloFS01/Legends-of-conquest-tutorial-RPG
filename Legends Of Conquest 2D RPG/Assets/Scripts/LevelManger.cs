using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManger : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    private Vector3 leftBottomEdge;
    private Vector3 rightTopEdge;

    private Player playerClass;

    void Start()
    {
        leftBottomEdge = tilemap.localBounds.min + new Vector3(-2f, 3f, 0f);
        rightTopEdge = tilemap.localBounds.max + new Vector3(-5f, -0.5f, 0f);

        StartCoroutine(WaitForPlayerClass());

    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator WaitForPlayerClass()
    {
        while(playerClass == null)
        {
            playerClass = FindObjectOfType<Player>();
            yield return null;
        }
        Player.instance.SetLimt(leftBottomEdge, rightTopEdge);
    }
}
