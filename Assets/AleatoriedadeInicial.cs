using UnityEngine;

public class AleatoriedadeInicial : MonoBehaviour
{
    [SerializeField] private float minVeloci; // Velocidade mínima
    [SerializeField] private float maxVeloci; // Velocidade máxima
    [SerializeField, Range(0f, 360f)] private float anguloInicial; // Ângulo inicial configurável (0 a 360 graus)

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Converte o ângulo inicial de graus para radianos
        float anguloRadianos = anguloInicial * Mathf.Deg2Rad;

        // Calcula a direção com base no ângulo
        Vector2 direcao = new Vector2(Mathf.Cos(anguloRadianos), Mathf.Sin(anguloRadianos)).normalized;

        // Gera uma velocidade aleatória dentro do intervalo
        float velocidade = Random.Range(minVeloci, maxVeloci);

        // Aplica a velocidade à bola
        rb.linearVelocity = direcao * velocidade;
    }
}

