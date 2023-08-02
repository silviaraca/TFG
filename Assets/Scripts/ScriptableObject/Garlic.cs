using UnityEngine;

public class Garlic : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isVisible = false;
    private Player player;
    public TextMeshProUGUI textoE;

    void Start()
    {
        // Obtener el componente SpriteRenderer del GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ocultar el sprite al inicio del juego
        spriteRenderer.enabled = false;
        textoE.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = collision.GetComponent<Player>();
        if(player){
            textoE.gameObject.SetActive(true);
            spriteRenderer.enabled = true;
            isVisible = true;
        }                  
    }

    void OnTriggerExit2D(Collider2D other)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            textoE.gameObject.SetActive(false);
            spriteRenderer.enabled = false;
            isVisible = false;
    
 
        } 
    }

    void Update()
    {
        // Verificar si el sprite está visible y si el usuario presiona la tecla "E"
        if (isVisible && Input.GetKeyDown(KeyCode.E))
        {
            // Ocultar el sprite nuevamente
            spriteRenderer.enabled = false;
            isVisible = false;

            // Aquí puedes agregar cualquier otro comportamiento que desees cuando el usuario presione "E".
            // Por ejemplo, puedes incrementar una puntuación, reproducir un sonido, etc.
        }
    }
}
