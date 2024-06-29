using Firebase;
using Firebase.Auth;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;

    public FirebaseAuth auth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase is ready for use.");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

public void LoginUser(string email, string password)
{
    auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
    {
        if (task.IsCanceled)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            return;
        }
        if (task.IsFaulted)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }

        FirebaseUser newUser = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
    });
}

public void RegisterUser(string email, string password)
{
    auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
    {
        if (task.IsCanceled)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            return;
        }
        if (task.IsFaulted)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }

        FirebaseUser newUser = task.Result;
        Debug.LogFormat("User created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
    });
}

public void UpdateUserProfile(string newName)
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            UserProfile profile = new UserProfile { DisplayName = newName };
            user.UpdateUserProfileAsync(profile).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("User profile updated successfully.");
            });
        }
    }

    public void SendPasswordResetEmail(string email)
    {
        auth.SendPasswordResetEmailAsync(email).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                return;
            }
            Debug.Log("Password reset email sent successfully.");
        });
    }

    public void SignInWithGoogle()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            WebClientId = "<YOUR-WEB-CLIENT-ID-from-Firebase-console>"
        };

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithGoogle was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithGoogle encountered an error: " + task.Exception);
                return;
            }

            Credential credential = GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
            auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
            {
                if (authTask.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + authTask.Exception);
                    return;
                }

                FirebaseUser newUser = authTask.Result;
                Debug.LogFormat("User signed in with Google: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
        });
    }

    public void SignOutUser()
    {
        auth.SignOut();
        Debug.Log("User signed out.");
    }

    public void DeleteUser()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            user.DeleteAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("User deleted successfully.");
            });
        }
    }

    public void LinkWithGoogle()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            WebClientId = ""
        };

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithGoogle was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithGoogle encountered an error: " + task.Exception);
                return;
            }

            Credential credential = GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
            auth.CurrentUser.LinkWithCredentialAsync(credential).ContinueWith(authTask =>
            {
                if (authTask.IsCanceled)
                {
                    Debug.LogError("LinkWithCredentialAsync was canceled.");
                    return;
                }
                if (authTask.IsFaulted)
                {
                    Debug.LogError("LinkWithCredentialAsync encountered an error: " + authTask.Exception);
                    return;
                }

                FirebaseUser newUser = authTask.Result;
                Debug.LogFormat("User linked with Google successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
        });
}