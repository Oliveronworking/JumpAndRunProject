using UnityEngine;

public class doNotDestroy : MonoBehaviour
{
    void Awake()
    {
        // Finde alle GameObjects mit dem Tag "GameMusic"
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");

        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        // Finde alle GameObjects mit dem Tag "Player"
        GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");

        if (playerObj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
