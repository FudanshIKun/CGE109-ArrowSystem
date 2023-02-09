using UnityEngine;
using UnityEngine.UIElements;
using Wonderland.Manager;

namespace Wonderland.Auth
{
    public class AuthenticationUI : MonoBehaviour
    {
        public static AuthenticationUI Instance;
        
        public VisualTreeAsset SignUpUXML;
        public VisualTreeAsset SignInUXML;

        #region Authentication Components

        public TextField userField;
        public TextField emailField;
        public TextField passwordField;
        public TextField confirmField;
        public RadioButton rememberPassword;
        public Label errorOutput;

        #endregion

        private void LoadSignUpTab()
        {
            UIManager.Instance.ChangeUxml(SignUpUXML);
        }
        
        private void LoadSignInTab()
        {
            UIManager.Instance.ChangeUxml(SignInUXML);
        }
        private void OnUxmlChange()
        {
            if (UIManager.Instance.currentUxml.Q<Label>("Authentication-Type") != null)
            {
                VisualElement currentUxml = UIManager.Instance.currentUxml;
                switch (UIManager.Instance.currentUxml.Q<Label>("Authentication-Type").text)
                {
                    case "New Account":
                        userField = currentUxml.Q<TextField>("User-Field");
                        emailField = currentUxml.Q<TextField>("Email-Field");
                        passwordField = currentUxml.Q<TextField>("Password-Field");
                        confirmField = currentUxml.Q<TextField>("Confirm-Field");
                        rememberPassword = currentUxml.Q<RadioButton>("RememberPassowrd");
                        errorOutput = currentUxml.Q<Label>("ErrorOutput");
                        var signUpButton = currentUxml.Q<Button>("Authentication-Button");
                        signUpButton.clicked += FirebaseManager.Instance.SignUp;
                        var SignInTab = currentUxml.Q<Button>("SignIn");
                        SignInTab.clicked += LoadSignInTab;
                        break;
                    case "Sign In":
                        emailField = currentUxml.Q<TextField>("Email-Field");
                        passwordField = currentUxml.Q<TextField>("Password-Field");
                        rememberPassword = currentUxml.Q<RadioButton>("RememberPassword");
                        errorOutput = currentUxml.Q<Label>("ErrorOutput");
                        var signInButton = currentUxml.Q<Button>("Authentication-Button");
                        signInButton.clicked += FirebaseManager.Instance.SignIn;
                        var SignUpTab = currentUxml.Q<Button>("SignUp");
                        SignUpTab.clicked += LoadSignUpTab;
                        break;
                }
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            UIManager.UxmlChanged += OnUxmlChange;
        }

        private void OnDisable()
        {
            UIManager.UxmlChanged -= OnUxmlChange;
        }
    }
}