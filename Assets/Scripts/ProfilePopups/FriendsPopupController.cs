using System.Collections;
using System.Collections.Generic;
using Duck.Http;
using Duck.Http.Service;
using Newtonsoft.Json;
using UnityEngine;

public class FriendsPopupController : Singleton<FriendsPopupController>
{
    [SerializeField] FriendCardController friendCardController;
    [SerializeField] Transform friendCardParent;
    List<FriendCardController> friendCards = new List<FriendCardController>();
    public void OpenPopup()
    {
        string token = PlayerPrefs.GetString("Token");
        var headers = new[] {
            new KeyValuePair<string,string>("Content-Type","application/json"),
            new KeyValuePair<string,string>("Authorization",$"Bearer: {token}")
        };
        Http.Get($"http://localhost:5000/api/auth/friends").SetHeaders(headers)
        .OnSuccess(response =>
        {
            Debug.Log("OpenPopup OnSuccess");
            GetFriendsResponse(response);
        })
        .OnError(response =>
        {
            Debug.Log("OpenPopup OnError");
            GetFriendsResponse(response);
        }).Send();
    }
    void GetFriendsResponse(HttpResponse httpResponse)
    {

        if (httpResponse.StatusCode == 200)
        {
            User[] friends = JsonConvert.DeserializeObject<User[]>(httpResponse.Text);

            foreach (var item in friends)
            {
                if (friendCards.Exists(x => x.cardUser._id == item._id)) return;
                FriendCardController nO = Instantiate(friendCardController, Vector3.zero, Quaternion.identity, friendCardParent);
                nO.InitCard(item);
                friendCards.Add(nO);
            }
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
    public void RemoveFriendCard(FriendCardController card)
    {
        friendCards.Remove(card);
        Destroy(card.gameObject);
    }
}
