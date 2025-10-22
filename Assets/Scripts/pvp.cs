using UnityEngine;
using UnityEngine.UI;

public class pvp : MonoBehaviour
{
    public int nextPlayer = 2;
    public int score1 = 0;
    public int score2 = 0;
    public void SwitchPlayer()
    {
        nextPlayer = 3 - nextPlayer;
        Button switchBut = GetComponent<Button>();
        Text buttonText = switchBut.GetComponentInChildren<Text>();
        buttonText.text = "To Player " + nextPlayer;

        switchBut.OnDeselect(null);
    }
}
