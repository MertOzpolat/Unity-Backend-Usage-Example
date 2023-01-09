using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    [SerializeField] ProfilePrefabController profilePrefabController;
    void OnEnable()
    {
        profilePrefabController.InitProfilePrefab(SampleSceneController.Instance.currentUser);
    }
}
