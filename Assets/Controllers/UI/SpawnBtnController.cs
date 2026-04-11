using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class SpawnBtnController : MonoBehaviour
{
    [SerializeField] private AudioClip somClick;
    [SerializeField] private GameObject enemy;
    private AudioSource audioSource;

    void Start()
    {
        //button = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TesteDeMapeamento(InputAction.CallbackContext context)
    {
        //Go Horse!
        //A UI estava "roubando" o WASD e o player não se movia
        //Começou a funcionar após esse método ser criado para testar se o mapeamento estava chegando
        //Lembrete para ajeitar no futuro
    }

    public void SpawnEnemy()
    {
        ButtonSound();

        Instantiate(enemy, SpawnPosition(), Quaternion.identity); 
    }

    private void ButtonSound()
    {
        if (somClick) audioSource.PlayOneShot(somClick);
    }

    private Vector3 SpawnPosition()
    {
        //Adicionar algoritmo de posicionamento
        return new Vector3(-3, 0, 0);
    }
}