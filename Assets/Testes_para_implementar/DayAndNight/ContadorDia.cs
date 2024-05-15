using UnityEngine;
using UnityEngine.UI;

public class ContadorDia : MonoBehaviour
{
    [SerializeField] private float minutesInDay = 10f; // Defina a duração do dia em minutos
    [SerializeField] private Material material;
    [SerializeField] private Texture texture1;
    [SerializeField] private Texture texture2;
    [SerializeField] private Text horaText;

    // corrigir rotação pelo tempo
    private float elapsedTime = 0f;
    private float blendSpeed = 0.05f; // Velocidade de transição entre texturas

    private void Start()
    {
        material.SetFloat("_Blend", 1f);
        material.SetFloat("_Rotation1", 0f);
        material.SetFloat("_Rotation2", 0f);
    }
    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        // Incrementa o tempo decorrido
        elapsedTime += Time.deltaTime;

        // Calcula a proporção do dia concluída
        float progress = elapsedTime / (minutesInDay * 60f); // deltaTime é em segundos
        // Ajusta progress para o intervalo de 0 a 1
        progress = Mathf.Clamp01(progress);

        // Se passou 24 horas, reinicia o contador
        if (progress >= 1f)
        {
            elapsedTime = 0f;
        }

        // Calcula e exibe a hora atual em formato de 24 horas
        int currentHour = Mathf.FloorToInt(progress * 24f);
        int currentMinute = Mathf.FloorToInt((progress * 24f * 60f) % 60f);
        //Debug.Log("Hora atual: " + currentHour.ToString("00") + ":" + currentMinute.ToString("00"));
        horaText.text = currentHour.ToString("00") + ":" + currentMinute.ToString("00");

        //proporção de horas por minuto do dia
        float proportionHoursPerMinute = 24f / (minutesInDay * 60f);
        Debug.Log("Proporção de horas por minuto: " + proportionHoursPerMinute);


        // Calcula a rotação alvo baseada no tempo decorrido desde o início do dia
        float targetRotation = progress * 360f; // Uma rotação completa em 24 horas (360 graus)
        float currentRotation = Mathf.LerpAngle(material.GetFloat("_Rotation1"), targetRotation, Time.deltaTime);
        material.SetFloat("_Rotation1", currentRotation);
        material.SetFloat("_Rotation2", currentRotation);

        // Define a interpolação entre as duas texturas
        float targetBlend = 1f;
        if (currentHour >= 5 && currentHour <= 10) // Transição das 5h às 9h
        {
            float transitionTimeHours = 4f;
            targetBlend = Mathf.Clamp01((elapsedTime / (transitionTimeHours * 3600f)) * 2f);
        }
        else if (currentHour > 10 && currentHour < 14)
        {
            targetBlend = 0.0f;
        }

        // corrigir transição para noite
        else if (currentHour >= 14 && currentHour < 18) // Transição das 14h às 18h
        {
            float transitionTimeHours = 4f;
            targetBlend = Mathf.Clamp01((1f - (elapsedTime / (transitionTimeHours * 3600f))) * 2f);
        }
        else if(currentHour>=18 && currentHour<5)
        {
            targetBlend = 1.0f;
        }


        float currentBlend = Mathf.Lerp(material.GetFloat("_Blend"), targetBlend, Time.deltaTime * blendSpeed);
        material.SetFloat("_Blend", currentBlend);

        // Aplica as texturas
        material.SetTexture("_Texture1", texture1);
        material.SetTexture("_Texture2", texture2);
    }
}
