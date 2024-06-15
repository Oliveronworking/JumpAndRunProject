using UnityEngine;

public class doNotDestroy : MonoBehaviour
{
    void Awake()
    {
        // Finde alle GameObjects mit dem Tag "GameMusic"
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");

        // Wenn mehr als ein "GameMusic" GameObject gefunden wird, zerstöre dieses Skript, um Duplikate zu vermeiden.
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        // Finde alle GameObjects mit dem Tag "Player"
        GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");

        // Wenn mehr als ein "Player" GameObject gefunden wird, zerstöre dieses Skript, um Duplikate zu vermeiden.
        if (playerObj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        // Markiere dieses GameObject (das Skript) als "DontDestroyOnLoad",
        // damit es beim Szenenwechsel erhalten bleibt.
        DontDestroyOnLoad(this.gameObject);
    }
}
