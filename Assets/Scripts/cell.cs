using UnityEngine;

public class cell : MonoBehaviour
{
    private bool isAlive = false;
    private SpriteRenderer sprite;
    private int parent = 0;
    private pvp pvpManager;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        pvpManager = FindObjectOfType<pvp>();
        UpdateColor();
    }

    private void UpdateColor()
    {        
        if (sprite)
        {
            if (isAlive)
            {
                if (parent == 1)
                {
                    sprite.color = new Color(0.4f, 0.3f, 0.2f);
                } else
                {
                    sprite.color = new Color(0.6f, 0.7f, 0.3f);
                }
            }
            else
            {
                sprite.color = Color.white;
            }
        } 
    }

    public bool GetAlive()
    {
        return isAlive;
    }

    public void SetAlive(bool alive, int par = 1)
    {
        isAlive = alive;
        parent = par;
        UpdateColor();
    }

    public void Toggle()
    {
        isAlive = !isAlive;
        if (isAlive)
        {
            parent = 3 - pvpManager.nextPlayer;
        }
        UpdateColor();
    }
    public int GetParent()
    {
        return parent;
    }
}