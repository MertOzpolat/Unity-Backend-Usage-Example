using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Duck.Http;
using Newtonsoft.Json;
using Duck.Http.Service;

public class FriendCardController : MonoBehaviour
{
    internal User cardUser;
    [SerializeField] TMP_Text friendName;
    [SerializeField] TMP_Text friendMail;
    public void InitCard(User usr)
    {
        cardUser = usr;
        friendMail.text = usr.email;
        friendName.text = usr.name;
    }
    public void RemoveFriendButtonHandler()
    {
        byte[] array = new byte[0];
        string token = PlayerPrefs.GetString("Token");
        var headers = new[] {
            new KeyValuePair<string,string>("Content-Type","application/json"),
            new KeyValuePair<string,string>("Authorization",$"Bearer: {token}")
        };
        Http.Put($"http://localhost:5000/api/auth/{cardUser._id}/deletefriend/", array).SetHeaders(headers)
        .OnSuccess(response =>
        {
            Debug.Log("RemoveFriendButtonHandler OnSuccess");
            RemoveFriendResponseHandler(response);
        })
        .OnError(response =>
        {
            Debug.Log("RemoveFriendButtonHandler OnError");
            RemoveFriendResponseHandler(response);
        }).Send();
    }
    public void RemoveFriendResponseHandler(HttpResponse httpResponse)
    {
        if (httpResponse.StatusCode == 200)
        {
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(httpResponse.Text);
            FriendsPopupController.Instance.RemoveFriendCard(this);
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
}
