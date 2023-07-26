using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] movementSprites;
    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private bool loop = true;
    [SerializeField] private float speedModifier = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool randomStartingFrame;

    private Sprite[] currentSprites;
    private int m_Index = 0;
    private float m_Frame = 0;
    public bool isMoving = false; // Track if the character is moving

    void Awake()
    {

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        m_Frame = 1;

        if (randomStartingFrame)
        {
            m_Index = Random.Range(0, movementSprites.Length - 1);
        }
    }

    void Update()
    {
        currentSprites = isMoving ? movementSprites : idleSprites;

        if (Time.timeScale == 0 || !loop && m_Index == currentSprites.Length) return;

        m_Frame += Time.deltaTime * speedModifier;

        if (m_Frame < 1f/12f)
        {
            return;
        }

        spriteRenderer.sprite = currentSprites[m_Index];

        m_Frame = 0;
        m_Index++;

        if (m_Index >= currentSprites.Length)
        {
            if (loop)
            {
                m_Index = 0;
            }
        }
    }

    public void SetIndex(int index)
    {
        m_Index = index;
    }

    public void Draw()
    {
        spriteRenderer.sprite = currentSprites[m_Index];
    }
}
