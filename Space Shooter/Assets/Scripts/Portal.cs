using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("The target portal where the player will be teleported")]
    public Transform targetPortal; // ������, ���� ����� ����������������� �����.

    [Header("Teleport Trigger Settings")]
    [Tooltip("The collider to use for detecting the player")]
    public Collider2D teleportObject; // ��������� ��� �������� ������������.

    [Header("Teleport Effects (Optional)")]
    [Tooltip("Whether to show teleportation effect (e.g., particles or animation)")]
    public bool showTeleportEffect = true; // �������� �� ������ ������������.
    public GameObject teleportEffect; // ������, ������� ����� �������� ��� ������������.

    private void OnValidate()
    {
        // ��������, ����� ��������� ������������ ������ � ����������.
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
        // ���������, ��� ���������� �����
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        // ����� �������� ������ ������������, ���� ������� � ����������
        if (showTeleportEffect && teleportEffect != null)
        {
            Instantiate(teleportEffect, player.position, Quaternion.identity); // ������� ������ � ����� ������������.
        }

        // ���������� ������ �� ������� �������� �������
        player.position = targetPortal.position;

        // ����� �������� ������ ��� �������� � ������ ������������, ���� �����.
        if (showTeleportEffect && teleportEffect != null)
        {
            // ��������, ����� ������� ������ ����� ���������� �������.
            Destroy(teleportEffect, 1f); // ������� ������ ����� 1 �������.
        }
    }
}