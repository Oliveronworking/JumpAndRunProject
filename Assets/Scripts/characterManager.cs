using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class characterManager : MonoBehaviour
{
    public characterDatabase characterDB;

    private GameObject currentCharacterModel;
    private int selectedCharacter = 0;

    public GameObject mainCanvas;
    public GameObject charactersCanvas;
    public GameObject player;

    void Start()
    {
        // Lade den zuletzt ausgewählten Charakter (falls vorhanden)
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        // Zeige den ausgewählten Charakter beim Start an
        LoadCharacter(selectedCharacter);

        // Registriere die Methode zur Behandlung des Szenenwechsels
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void NextOption()
    {
        selectedCharacter++;

        if (selectedCharacter >= characterDB.characterCount)
        {
            selectedCharacter = 0;
        }

        LoadCharacter(selectedCharacter);
    }

    public void PreviousOption()
    {
        selectedCharacter--;

        if (selectedCharacter < 0)
        {
            selectedCharacter = characterDB.characterCount - 1;
        }

        LoadCharacter(selectedCharacter);
    }

    public void SelectOption()
    {
        // Speichere den ausgewählten Charakter in PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        PlayerPrefs.Save(); // Sicherstellen, dass die Einstellungen sofort gespeichert werden

        charactersCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    private void LoadCharacter(int index)
    {
        // Entferne das aktuelle Modell, falls vorhanden
        if (currentCharacterModel != null)
        {
            Destroy(currentCharacterModel);
        }

        // Erstelle das neue Modell
        character character = characterDB.GetCharacter(index);

        if (character == null || character.characterModel == null)
        {
            //Debug.LogError("Character or character model is null at index: " + index);
            return;
        }

        currentCharacterModel = Instantiate(character.characterModel, transform.position, Quaternion.identity);

        if (currentCharacterModel == null)
        {
            //Debug.LogError("Failed to instantiate character model at index: " + index);
            return;
        }

        // Rotiere das Modell um 180 Grad um die Y-Achse
        currentCharacterModel.transform.Rotate(0, 180, 0);

        // Optional: Positioniere und rotiere das Modell, wenn nötig
        currentCharacterModel.transform.SetParent(transform);

        // Rigidbody-Konfiguration
        Rigidbody rb = currentCharacterModel.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = currentCharacterModel.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; // Deaktiviere die Gravitation
        rb.isKinematic = true; // Setze das Modell auf kinematisch

        rb.mass = 60.0f;


    }


    void OnDestroy()
    {
        // Deregistriere die Methode, wenn das Objekt zerstört wird
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Überprüfe den Szenen-Index und initialisiere den PlayerController nur, wenn die gewünschte Szene geladen ist
        int gameSceneIndex = 1;
        if (scene.buildIndex == gameSceneIndex)
        {
            InitializePlayerController();
        }
    }

    private void InitializePlayerController()
    {
        PlayerController playerControllerScript = currentCharacterModel.GetComponent<PlayerController>();
        if (playerControllerScript == null)
        {
            playerControllerScript = currentCharacterModel.AddComponent<PlayerController>();

            // Füge AudioSource und Animator Komponenten hinzu, falls sie nicht existieren
            AudioSource audioSource = currentCharacterModel.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = currentCharacterModel.AddComponent<AudioSource>();
            }
            playerControllerScript.playerAudio = audioSource;

            Animator animator = currentCharacterModel.GetComponent<Animator>();
            if (animator == null)
            {
                animator = currentCharacterModel.AddComponent<Animator>();
            }
            playerControllerScript.playerAnim = animator;

            playerControllerScript.jumpForce = 700.0f;
            playerControllerScript.gravityModifier = 2.0f;

            // Lade die Partikelsysteme aus dem Resources-Ordner und instanziiere sie
            GameObject explosionPrefab = Resources.Load<GameObject>("Particles/FX_Explosion_Smoke");
            GameObject dirtPrefab = Resources.Load<GameObject>("Particles/FX_DirtSplatter");

            if (explosionPrefab != null)
            {
                GameObject explosionInstance = Instantiate(explosionPrefab);
                playerControllerScript.explosionParticle = explosionInstance.GetComponent<ParticleSystem>();
            }

            if (dirtPrefab != null)
            {
                GameObject dirtInstance = Instantiate(dirtPrefab);
                playerControllerScript.dirtParticle = dirtInstance.GetComponent<ParticleSystem>();
            }

            // Lade die AudioClips aus dem Resources-Ordner
            playerControllerScript.jumpSound = Resources.Load<AudioClip>("SFX/jump1");
            playerControllerScript.crashSound = Resources.Load<AudioClip>("SFX/crash1");

            // Finde das GameOverCanvas-Objekt mit dem Tag
            playerControllerScript.GameOverCanvas = GameObject.FindWithTag("GameOverCanvas");

            // Debug-Ausgaben, um sicherzustellen, dass die Ressourcen geladen wurden
            Debug.Log("PlayerController initialisiert: jumpForce=" + playerControllerScript.jumpForce +
                      ", gravityModifier=" + playerControllerScript.gravityModifier);
            Debug.Log("Animator: " + (playerControllerScript.playerAnim != null));
            Debug.Log("AudioSource: " + (playerControllerScript.playerAudio != null));
            Debug.Log("ExplosionParticle: " + (playerControllerScript.explosionParticle != null));
            Debug.Log("DirtParticle: " + (playerControllerScript.dirtParticle != null));
            Debug.Log("JumpSound: " + (playerControllerScript.jumpSound != null));
            Debug.Log("CrashSound: " + (playerControllerScript.crashSound != null));
            Debug.Log("GameOverCanvas: " + (playerControllerScript.GameOverCanvas != null));

            // Spieler richtig ausrichten
            Transform playerTransform = currentCharacterModel.transform;
            playerTransform.Rotate(0f, -90f, 0f);

            // Beispiel für einen BoxCollider
            BoxCollider boxCollider = currentCharacterModel.GetComponent<BoxCollider>();
            if (boxCollider == null)
            {
                boxCollider = currentCharacterModel.AddComponent<BoxCollider>();
            }


            if (boxCollider != null)
            {
                // Anpassen der Größe des BoxColliders
                boxCollider.center = new Vector3(0f, 1.5f, 0f); // Beispiel für die Mitte des BoxColliders
                boxCollider.size = new Vector3(1f, 2.5f, 1f);    // Beispiel für die Größe des BoxColliders
            }

            // Optional: Zeige den Collider im Editor an
            // Hier wird die Methode OnDrawGizmosSelected verwendet, um den Collider im Editor anzuzeigen
            UnityEditor.EditorApplication.delayCall += () =>
            {
                UnityEditor.Selection.activeGameObject = currentCharacterModel;
            };
        }
    }

    private void OnDrawGizmosSelected()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }

}




/*
// Überprüfen, ob ein MeshFilter vorhanden ist
MeshFilter meshFilter = currentCharacterModel.GetComponent<MeshFilter>();
if (meshFilter != null)
{
    // Überprüfen, ob ein MeshCollider bereits vorhanden ist
    MeshCollider meshCollider = currentCharacterModel.GetComponent<MeshCollider>();
    if (meshCollider == null)
    {
        // Füge einen MeshCollider hinzu, falls keiner vorhanden ist
        meshCollider = currentCharacterModel.AddComponent<MeshCollider>();
    }

    // Setze das Mesh des MeshColliders auf das des MeshFilters
    meshCollider.sharedMesh = meshFilter.sharedMesh;

    // Optional: Falls du willst, dass der MeshCollider Convex ist (nur für konvexe Meshes)
    // meshCollider.convex = true;
}
*/
