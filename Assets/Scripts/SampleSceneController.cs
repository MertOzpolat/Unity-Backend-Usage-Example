using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Duck.Http;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Duck.Http.Service;

public class SampleSceneController : MonoBehaviour
{
    [Header("Register Section")]
    [SerializeField] TMP_InputField registerNameInput;
    [SerializeField] TMP_InputField registerMailInput;
    [SerializeField] TMP_InputField registerPassInput;
    [Header("Login Section")]
    [SerializeField] TMP_InputField loginMailInput;
    [SerializeField] TMP_InputField loginPassInput;

    public void RegisterButtonHandler()
    {
        //Validations will be here

        RegisterRequest registerRequest = new RegisterRequest();
        registerRequest.email = registerMailInput.text;
        registerRequest.name = registerNameInput.text;
        registerRequest.password = registerPassInput.text;
        Http.PostJson("http://localhost:5000/api/auth/register/", JsonConvert.SerializeObject(registerRequest)).SetHeader("Content-Type", "application/json")
        .OnSuccess(response =>
        {
            RegisterResponseHandler(response);
        })
        .OnError(response =>
        {
            RegisterResponseHandler(response);
        })
        .Send();
    }
    void RegisterResponseHandler(HttpResponse httpResponse)
    {
        if (httpResponse.StatusCode == 200)
        {
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(httpResponse.Text);
            PlayerPrefs.SetString("Token", authResponse.access_token);
            Debug.Log(authResponse.access_token);
        }
        if(httpResponse.Error !=null || httpResponse.Error!="")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
    public void LoginButtonHandler()
    {
        //Validations will be here
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.email = loginMailInput.text;
        loginRequest.password = loginPassInput.text;
        Http.PostJson("http://localhost:5000/api/auth/login/", JsonConvert.SerializeObject(loginRequest)).SetHeader("Content-Type", "application/json")
        .OnSuccess(response =>{
            Debug.Log("OnSuccess");
            LoginResponseHandler(response);
        })
        .OnError(response =>{
            Debug.Log("OnError");
            LoginResponseHandler(response);
        }).Send();
    }
    void LoginResponseHandler(HttpResponse httpResponse){
        if(httpResponse.StatusCode ==200){
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(httpResponse.Text);
            PlayerPrefs.SetString("Token",authResponse.access_token);
            Debug.Log(authResponse.access_token);
        }
        if(httpResponse.Error!=null || httpResponse.Error !="")
            Debug.Log(httpResponse.Text + " "+ httpResponse.Error);
    }
}
public class LoginRequest{
    public string email;
    public string password;
}
public class RegisterRequest
{
    public string name;
    public string email;
    public string password;
}
public class AuthResponse
{
    public bool success;
    public string access_token;
    public User user;
}
public class User
{
    public string name;
    public string email;
    public string password;
    public string createdAt;
    public string about;
    public string profile_image;
    public string blocked;
    public string resetPasswordToken;
    public string resetPasswordExpire;
    public int level;
    public int exprience;
    public string clan;//clan ref object.
    public User[] friends;
    public User[] blockList;
    public int balance;
    public object inventory;
}
