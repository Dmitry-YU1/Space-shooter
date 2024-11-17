using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("The target portal where the player will be teleported")]
    public Transform targetPortal; // Портал, куда будет телепортироваться игрок.

    [Header("Teleport Trigger Settings")]
    [Tooltip("The collider to use for detecting the player")]
    public Collider2D teleportObject; // Коллайдер для триггера телепортации.

    [Header("Teleport Effects (Optional)")]
    [Tooltip("Whether to show teleportation effect (e.g., particles or animation)")]
    public bool showTeleportEffect = true; // Показать ли эффект телепортации.
    public GameObject teleportEffect; // Эффект, который можно включить при телепортации.

    private void OnValidate()
    {
        // Проверка, чтобы назначить обязательные ссылки в инспекторе.
        if (teleportObject == null)
        {
            Debug.LogWarning("Teleport trigger is not assigned!", this);
        }

        if (targetPortal == null)
        {
            Debug.LogWarning("Target portal is not assigned!", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что столкнулся игрок
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        // Можно добавить эффект телепортации, если указано в настройках
        if (showTeleportEffect && teleportEffect != null)
        {
            Instantiate(teleportEffect, player.position, Quaternion.identity); // Создаем эффект в месте телепортации.
        }

        // Перемещаем игрока на позицию целевого портала
        player.position = targetPortal.position;

        // Можно добавить эффект или анимацию в момент телепортации, если нужно.
        if (showTeleportEffect && teleportEffect != null)
        {
            // Возможно, стоит удалить эффект после некоторого времени.
            Destroy(teleportEffect, 1f); // Удаляет эффект через 1 секунду.
        }
    }
}