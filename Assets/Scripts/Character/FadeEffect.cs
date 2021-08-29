using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 8.0f;
    private float alphaMultiplayer = 0.85f;

    private Color _color;

    private Transform player;

    private SpriteRenderer _sr;
    private SpriteRenderer _PlayerSR;


    private void OnEnable()
    {
        _sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _PlayerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        _sr.sprite = _PlayerSR.sprite;
        transform.position = player.transform.position;
        timeActivated = Time.time;

    }

    private void Update()
    {
        alpha *= alphaMultiplayer;
        _color = new Color(1f,1f,1f, alpha);
        _sr.color = _color;

        if (Time.time >= (timeActivated + activeTime))
        {
            //add back to pool
        } 
    }
}
