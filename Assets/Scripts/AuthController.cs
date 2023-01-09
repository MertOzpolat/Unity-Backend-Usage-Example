using System.Collections;
using System.Collections.Generic;
using Duck.Http;
using Duck.Http.Service;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class AuthController : MonoBehaviour
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
        if (!Validator.CheckName(registerNameInput.text)) return;//throw error.
        if (!Validator.CheckMail(registerMailInput.text)) return;//throw error.
        if (!Validator.CheckPassword(registerPassInput.text)) return;//throw error.

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
            LoginSuccess(authResponse);
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
    public void LoginButtonHandler()
    {
        //dont use my backend for nothing
        if (!Validator.CheckMail(loginMailInput.text)) return;//throw error.
        if (!Validator.CheckPassword(loginPassInput.text)) return;//throw error.

        LoginRequest loginRequest = new LoginRequest();
        loginRequest.email = loginMailInput.text;
        loginRequest.password = loginPassInput.text;
        Http.PostJson("http://localhost:5000/api/auth/login/", JsonConvert.SerializeObject(loginRequest)).SetHeader("Content-Type", "application/json")
        .OnSuccess(response =>
        {
            Debug.Log("OnSuccess");
            LoginResponseHandler(response);
        })
        .OnError(response =>
        {
            Debug.Log("OnError");
            LoginResponseHandler(response);
        }).Send();
    }
    void LoginResponseHandler(HttpResponse httpResponse)
    {
        if (httpResponse.StatusCode == 200)
        {
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(httpResponse.Text);
            LoginSuccess(authResponse);
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
    void LoginSuccess(AuthResponse authResponse)
    {
        SampleSceneController.Instance.SetCurrentUser(authResponse.user,authResponse.access_token);
    }
}
