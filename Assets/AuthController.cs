﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.CodeDom;
using Firebase.Database;
using Firebase.Extensions;

public class AuthController : MonoBehaviour {

    public Text emailInput, passwordInput, username;
    public static Player aktualni=null;
    

    public void Login()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
                passwordInput.text).ContinueWithOnMainThread(( task=>
                {
                    //prekinjen
                    if (task.IsCanceled)
                    {
                        Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;
                    }

                    //ce se je pojavila kaka napaka
                    if (task.IsFaulted)
                    {

                        Firebase.FirebaseException e = 
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;

                    }
                    //uspešen
                    if (task.IsCompleted)
                    {
                        Firebase.Auth.FirebaseAuth auth=FirebaseAuth.DefaultInstance;
                        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
                        if (user != null) {
                            string name = user.DisplayName;
                            string email = user.Email;
                            // The user's Id, unique to the Firebase project.
                            // Do NOT use this value to authenticate with your backend server, if you
                            // have one; use User.TokenAsync() instead.
                            string uid = user.UserId;
                            Debug.Log(name+ " "+email +" "+uid);
                        }
                       

                        print("Prijava uspešna!");
                        SceneManager.LoadScene("start");

                    }


                }));


    }

    private void SaveCurrentUser()
    {
        
    }
    public void Login_Anonymus()
    {
        SceneManager.LoadScene("start");
    }

    public void RegisterUser()
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text,
            passwordInput.text).ContinueWithOnMainThread((task =>
            {
                if(emailInput.text.Equals("") && passwordInput.text.Equals(""))
                {
                    print("Vnesite email in geslo!");
                    return;
                }
                //prekinjen
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                    GetErrorMessage((AuthError)e.ErrorCode);

                    return;
                }

                //ce se je pojavila kaka napaka
                if (task.IsFaulted)
                {

                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                    GetErrorMessage((AuthError)e.ErrorCode);

                    return;

                }
                //uspešen
                if (task.IsCompleted)
                {
                    //dodan del za kreiranje username-a
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    if (newUser != null)
                    {
                        Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
                        {
                            //tu vstavi text od vnosa za username
                            DisplayName = username.text,

                        };
                        newUser.UpdateUserProfileAsync(profile).ContinueWith(task2 =>
                        {
                            if (task2.IsCanceled)
                            {
                                Debug.LogError("UpdateUserProfileAsync was canceled.");
                                return;
                            }

                            if (task2.IsFaulted)
                            {
                                Debug.LogError("UpdateUserProfileAsync encountered an error: " + task2.Exception);
                                return;
                            }

                            Debug.Log("User profile updated successfully.");
                        });
                    }

                    print("Registracija uspešna!");
                    SceneManager.LoadScene("ZacetnaStran");
                }



            }));
    }

    
    public void Logout()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
            print("Odjava uspešna!");
        }
    }

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();


        //napake in izpis
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                break;
            case AuthError.MissingPassword:
                    print("Vpišite geslo");
                break;
            case AuthError.WrongPassword:
                    print("Napačno geslo!");
                break;
            case AuthError.InvalidEmail:
                    print("Nepravilen email!");
                break;
        }


        print(msg);
    }

    public void registracijaStran()
    {
        SceneManager.LoadScene("registracija1");
    }

} 







































