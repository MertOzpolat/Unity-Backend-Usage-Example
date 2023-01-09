using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Duck.Http;
using Newtonsoft.Json;
using Duck.Http.Service;

public class ProfilePrefabController : MonoBehaviour
{
    [SerializeField] TMP_InputField nameText;
    [SerializeField] TMP_Text createdAtText;
    [SerializeField] TMP_InputField aboutText;
    [SerializeField] TMP_Text clanText;
    [SerializeField] TMP_Text friendsText;
    [SerializeField] TMP_Text balanceText;
    [SerializeField] TMP_Text itemsText;
    [SerializeField] Button updateButton;

    public void InitProfilePrefab(User user)
    {
        nameText.text = user.name;
        createdAtText.text = user.createdAt;
        aboutText.text = user.about;
        clanText.text = user.clan;
        friendsText.text = user.friends.Length.ToString();
        balanceText.text = user.balance.ToString();
        itemsText.text = user.inventory.Length.ToString();
    }
    public void UpdateButtonHandler()
    {
        if (nameText.text != SampleSceneController.Instance.currentUser.name || aboutText.text != SampleSceneController.Instance.currentUser.about)
        {
            SampleSceneController.Instance.currentUser.name = nameText.text;
            SampleSceneController.Instance.currentUser.about = aboutText.text;
            updateButton.enabled = false;
            SendNewData();
        }
    }

    private void SendNewData()
    {
        if (!Validator.CheckName(nameText.text)) return;//throw error.
        string token = PlayerPrefs.GetString("Token");
        var headers = new[] {
            new KeyValuePair<string,string>("Content-Type","application/json"),
            new KeyValuePair<string,string>("Authorization",$"Bearer: {token}")
        };
        Http.Put("http://localhost:5000/api/auth/edit/", JsonConvert.SerializeObject(SampleSceneController.Instance.currentUser)).SetHeaders(headers)
        .OnSuccess(response =>
        {
            UpdateUserResponseHandler(response);
        })
        .OnError(response =>
        {
            UpdateUserResponseHandler(response);
        })
        .Send();
    }
    private void UpdateUserResponseHandler(HttpResponse httpResponse)
    {
        if (httpResponse.StatusCode == 200)
        {
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(httpResponse.Text);
            SampleSceneController.Instance.SetCurrentUser(authResponse.user, authResponse.access_token);
            Debug.Log("User updated successfully");
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);

        updateButton.enabled = true;
    }
}
