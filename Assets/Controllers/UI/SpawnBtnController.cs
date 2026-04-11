using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SpawnBtnController : MonoBehaviour
{
    //private Button button;
    [SerializeField] private AudioClip somClick;
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

    public void OnPointerDown()
    {
        if (somClick) audioSource.PlayOneShot(somClick);
    }

}