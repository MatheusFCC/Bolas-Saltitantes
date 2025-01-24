using UnityEngine;

public class ColisaoComBola : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab da bola
    public Transform initialPosition; // Transform que define a posição inicial das bolas
    [SerializeField] private int maxBalls = 10; // Número máximo de bolas permitido
    [SerializeField] private bool aumentarTamanho = false; // Define o comportamento da colisão

    private int currentBallCount = 0; // Contador de bolas instanciadas


    private void Start()
    {
        // Carrega o valor salvo do toggle Crescer ao iniciar a cena
        if (PlayerPrefs.HasKey("CrescerToggle"))
        {
            aumentarTamanho = PlayerPrefs.GetInt("CrescerToggle") == 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Transform ballTransform = collision.gameObject.transform;

            if (ballRb != null)
            {
                Vector2 normal = collision.contacts[0].normal;
                Vector2 incomingVelocity = ballRb.linearVelocity;
                Vector2 reflection = Vector2.Reflect(incomingVelocity, normal);

                // Adiciona variação de ângulo aleatória
                float randomAngle = Random.Range(-20f, 20f);
                reflection = Quaternion.Euler(0, 0, randomAngle) * reflection;

                // Ajusta a velocidade com um pequeno impulso extra para evitar "grudar"
                float speedMultiplier = Random.Range(0.9f, 1.1f);
                ballRb.linearVelocity = reflection * speedMultiplier;

                // Adiciona um impulso extra para afastar da parede
                ballRb.AddForce(normal * 1.8f, ForceMode2D.Impulse);
            }

            if (aumentarTamanho)
            {
                AumentarTamanhoInstantaneo(ballTransform, ballRb);
            }
            else
            {
                if (currentBallCount < maxBalls)
                {
                    Instantiate(ballPrefab, initialPosition.position, Quaternion.identity);
                    currentBallCount++;
                }
                else
                {
                    Debug.Log("Limite de bolas atingido!");
                }
            }
        }
    }

    private void AumentarTamanhoInstantaneo(Transform ballTransform, Rigidbody2D ballRb)
    {
        Vector3 targetScale = ballTransform.localScale * 1.1f;
        ballTransform.localScale = targetScale;

        if (ballTransform.localScale.x >= 8.2f)
        {
            targetScale = new Vector3(8.2f, 8.2f, 8.2f); // Limitando o crescimento a 8.2
            ballRb.linearVelocity = Vector2.zero; // Parar movimento
            ballRb.isKinematic = true; // Parar de ser afetado por física
            ballRb.simulated = false; // Desativar colisões
            ballTransform.position = Vector3.zero; // Teleporta para 0, 0, 0
        }
    }
}
