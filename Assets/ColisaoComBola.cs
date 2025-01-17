using UnityEngine;

public class ColisaoComBola : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab da bola
    public Transform initialPosition; // Transform que define a posição inicial das bolas
    [SerializeField] public int maxBalls = 10; // Número máximo de bolas permitido

    private int currentBallCount = 0; // Contador de bolas instanciadas

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto que colidiu é uma bola
        if (collision.gameObject.CompareTag("Bola"))
        {
            // Apenas cria uma nova bola se não ultrapassar o limite
            if (currentBallCount < maxBalls)
            {
                Instantiate(ballPrefab, initialPosition.position, Quaternion.identity);
                currentBallCount++; // Incrementa o contador de bolas
            }
            else
            {
                Debug.Log("Limite de bolas atingido!");
            }
        }
    }
}

