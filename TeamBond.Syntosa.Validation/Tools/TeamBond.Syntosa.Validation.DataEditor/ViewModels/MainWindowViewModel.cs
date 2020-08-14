namespace TeamBond.Syntosa.Validation.DataEditor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive;

    using ReactiveUI;

    using TeamBond.Core;
    using TeamBond.Core.Engine;
    using TeamBond.Domain.ConfigurationManagement;
    using TeamBond.Domain.User;
    using TeamBond.Services.ConfigurationManagement;
    using TeamBond.Services.Users;

    /// <summary>
    /// The main window view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// The application context.
        /// </summary>
        private readonly IUserContext userContext;

        /// <summary>
        /// The user registration service.
        /// </summary>
        private readonly IUserRegistrationService userRegistrationService;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The user settings.
        /// </summary>
        private readonly UserSettings userSettings;

        /// <summary>
        /// The email.
        /// </summary>
        private string email;

        /// <summary>
        /// The errors.
        /// </summary>
        private string errors;

        /// <summary>
        /// The first name.
        /// </summary>
        private string firstName;

        /// <summary>
        /// The has errors.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// A value indicating whether the logged in user is a super user.
        /// </summary>
        private bool isDeleterVisible;

        /// <summary>
        /// A value indicating whether the logged in user is an admin.
        /// </summary>
        private bool isEditorVisible;

        /// <summary>
        /// A value indicating whether the user has logged in.
        /// </summary>
        private bool isNotLoggingIn;

        /// <summary>
        /// The is registering.
        /// </summary>
        private bool isRegistering;

        /// <summary>
        /// The last name.
        /// </summary>
        private string lastName;

        /// <summary>
        /// The module creator content.
        /// </summary>
        private ViewModelBase moduleCreatorContent;

        /// <summary>
        /// The password.
        /// </summary>
        private string password;

        /// <summary>
        /// The password confirm.
        /// </summary>
        private string passwordConfirm;

        /// <summary>
        /// The prototype editor content.
        /// </summary>
        private ViewModelBase prototypeEditorContent;

        /// <summary>
        /// The successful login.
        /// </summary>
        private bool successfulLogin;

        /// <summary>
        /// The content.
        /// </summary>
        private ViewModelBase typeCreatorContent;

        /// <summary>
        /// The type function creator content.
        /// </summary>
        private ViewModelBase typeFunctionCreatorContent;

        /// <summary>
        /// The type unit creator content.
        /// </summary>
        private ViewModelBase typeUnitCreatorContent;

        /// <summary>
        /// The username.
        /// </summary>
        private string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.IsNotLoggingIn = false;

            this.userRegistrationService = TeamBondEngineContext.Current.Resolve<IUserRegistrationService>();
            this.userService = TeamBondEngineContext.Current.Resolve<IUserService>();
            this.userContext = TeamBondEngineContext.Current.Resolve<IUserContext>();
            var settingsService = TeamBondEngineContext.Current.Resolve<ISettingsService>();
            var applicationContext = TeamBondEngineContext.Current.Resolve<IApplicationContext>();

            if (!Enum.TryParse(applicationContext.CurrentAwsPlatform, out AwsPlatform awsPlatform))
            {
                awsPlatform = AwsPlatform.None;
            }

            this.userSettings = settingsService.LoadSetting<UserSettings>(
                applicationContext.CurrentApplication,
                applicationContext.CurrentApplicationGroup,
                applicationContext.CurrentEnvironment,
                applicationContext.CurrentAwsAccountId,
                awsPlatform,
                applicationContext.CurrentAwsRegion);

            this.LogIn = ReactiveCommand.Create(this.LogInResult);
            this.Register = ReactiveCommand.Create(this.UserRegistration);
            this.CreateUser = ReactiveCommand.Create(this.CreateNewUser);
            this.ReturnToLogin = ReactiveCommand.Create(this.LoginReturn);

            this.ModuleCreatorContent = this.ModuleBuilder = new ModulePrototypeBuilderViewModel();
            this.TypeUnitCreatorContent = this.TypeUnitBuilder = new TypeUnitPrototypeBuilderViewModel();
            this.TypeCreatorContent = this.TypeBuilder = new TypePrototypeBuilderViewModel();
            this.TypeFunctionCreatorContent = this.TypeFunctionBuilder = new TypeFunctionPrototypeBuilderViewModel();
            this.PrototypeEditorContent = this.PrototypeEditor = new PrototypeEditorViewModel();
        }

        /// <summary>
        /// Gets the create user.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CreateUser { get; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get => this.email;
            set => this.RaiseAndSetIfChanged(ref this.email, value);
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public string Errors
        {
            get => this.errors;
            set => this.RaiseAndSetIfChanged(ref this.errors, value);
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName
        {
            get => this.firstName;
            set => this.RaiseAndSetIfChanged(ref this.firstName, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has errors.
        /// </summary>
        public bool HasErrors
        {
            get => this.hasErrors;
            set => this.RaiseAndSetIfChanged(ref this.hasErrors, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the logged in user is a super user.
        /// </summary>
        public bool IsDeleterVisible
        {
            get => this.isDeleterVisible;
            set => this.RaiseAndSetIfChanged(ref this.isDeleterVisible, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the logged in user is an admin.
        /// </summary>
        public bool IsEditorVisible
        {
            get => this.isEditorVisible;
            set => this.RaiseAndSetIfChanged(ref this.isEditorVisible, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is logged in.
        /// </summary>
        public bool IsNotLoggingIn
        {
            get => this.isNotLoggingIn;
            set => this.RaiseAndSetIfChanged(ref this.isNotLoggingIn, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is registering.
        /// </summary>
        public bool IsRegistering
        {
            get => this.isRegistering;
            set => this.RaiseAndSetIfChanged(ref this.isRegistering, value);
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName
        {
            get => this.lastName;
            set => this.RaiseAndSetIfChanged(ref this.lastName, value);
        }

        /// <summary>
        /// Gets the log in.
        /// </summary>
        public ReactiveCommand<Unit, Unit> LogIn { get; }

        /// <summary>
        /// Gets the module builder.
        /// </summary>
        public ModulePrototypeBuilderViewModel ModuleBuilder { get; }

        /// <summary>
        /// Gets or sets the module creator content.
        /// </summary>
        public ViewModelBase ModuleCreatorContent
        {
            get => this.moduleCreatorContent;
            set => this.RaiseAndSetIfChanged(ref this.moduleCreatorContent, value);
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get => this.password;
            set => this.RaiseAndSetIfChanged(ref this.password, value);
        }

        /// <summary>
        /// Gets or sets the password confirm.
        /// </summary>
        public string PasswordConfirm
        {
            get => this.passwordConfirm;
            set => this.RaiseAndSetIfChanged(ref this.passwordConfirm, value);
        }

        /// <summary>
        /// Gets the prototype editor.
        /// </summary>
        public PrototypeEditorViewModel PrototypeEditor { get; }

        /// <summary>
        /// Gets the prototype editor content.
        /// </summary>
        public ViewModelBase PrototypeEditorContent
        {
            get => this.prototypeEditorContent;
            private set => this.RaiseAndSetIfChanged(ref this.prototypeEditorContent, value);
        }

        /// <summary>
        /// Gets the register.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Register { get; }

        /// <summary>
        /// Gets the return to login.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ReturnToLogin { get; }

        /// <summary>
        /// Gets or sets a value indicating whether successful login.
        /// </summary>
        public bool SuccessfulLogin
        {
            get => this.successfulLogin;
            set => this.RaiseAndSetIfChanged(ref this.successfulLogin, value);
        }

        /// <summary>
        /// Gets the type builder.
        /// </summary>
        public TypePrototypeBuilderViewModel TypeBuilder { get; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public ViewModelBase TypeCreatorContent
        {
            get => this.typeCreatorContent;
            private set => this.RaiseAndSetIfChanged(ref this.typeCreatorContent, value);
        }

        /// <summary>
        /// Gets the type function builder.
        /// </summary>
        public TypeFunctionPrototypeBuilderViewModel TypeFunctionBuilder { get; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public ViewModelBase TypeFunctionCreatorContent
        {
            get => this.typeFunctionCreatorContent;
            private set => this.RaiseAndSetIfChanged(ref this.typeFunctionCreatorContent, value);
        }

        /// <summary>
        /// Gets the type unit builder.
        /// </summary>
        public TypeUnitPrototypeBuilderViewModel TypeUnitBuilder { get; }

        /// <summary>
        /// Gets the type unit creator content.
        /// </summary>
        public ViewModelBase TypeUnitCreatorContent
        {
            get => this.typeUnitCreatorContent;
            private set => this.RaiseAndSetIfChanged(ref this.typeUnitCreatorContent, value);
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username
        {
            get => this.username;
            set => this.RaiseAndSetIfChanged(ref this.username, value);
        }

        /// <summary>
        /// Creates a new user inactive.
        /// </summary>
        private void CreateNewUser()
        {
            if (!this.Password.Equals(this.PasswordConfirm))
            {
                this.Errors = "The password and Password confirmation do not match";
                this.HasErrors = true;
                return;
            }

            User user = new User
                            {
                                Email = this.Email,
                                FirstName = this.FirstName,
                                LastName = this.LastName,
                                Username = this.Username,
                                IsActive = false
                            };

            UserRegistrationRequest request = new UserRegistrationRequest(
                user,
                this.Email,
                this.Username,
                this.Password,
                this.userSettings.DefaultPasswordFormat,
                true);

            UserRegistrationResult result = this.userRegistrationService.RegisterUser(request);

            if (!result.Success)
            {
                this.Errors = result.Errors.ToString();
                this.HasErrors = true;
                return;
            }

            this.LoginReturn();
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Email = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.PasswordConfirm = string.Empty;
        }

        /// <summary>
        /// The log in.
        /// </summary>
        private void LogInResult()
        {
            if (string.IsNullOrWhiteSpace(this.Username) || string.IsNullOrWhiteSpace(this.Password))
            {
                this.Errors = "Please make sure both the username and password fields are filled out";
                this.HasErrors = true;
                return;
            }

            UserLogInResults result = this.userRegistrationService.ValidateUser(this.Username, this.Password);

            switch (result)
            {
                case UserLogInResults.Successful:
                    this.IsNotLoggingIn = true;
                    this.SuccessfulLogin = true;
                    this.HasErrors = false;
                    this.errors = string.Empty;
                    this.userContext.CurrentUser = this.userService.GetUserByEmail(this.Username);
                    IList<UserRole> userRoles = this.userService.GetUserRoles(this.userContext.CurrentUser);
                    if (userRoles.Any(
                        userRole => userRole.SystemName.Equals(
                            SystemUserRoleNames.TeamBondSuperUsers,
                            StringComparison.OrdinalIgnoreCase)))
                    {
                        this.IsDeleterVisible = true;
                    }

                    if (userRoles.Any(
                            userRole => userRole.SystemName.Equals(
                                SystemUserRoleNames.TeamBondAdministrators,
                                StringComparison.OrdinalIgnoreCase)) || userRoles.Any(
                            userRole => userRole.SystemName.Equals(
                                SystemUserRoleNames.TeamBondSuperUsers,
                                StringComparison.OrdinalIgnoreCase)))
                    {
                        this.IsEditorVisible = true;
                    }

                    return;
                case UserLogInResults.UserDoesNotExist:
                    this.Errors = "There is no user associated with this account information";
                    this.HasErrors = true;
                    return;
                case UserLogInResults.NotActive:
                    this.Errors = "The user associated with this account has been deactivated";
                    this.HasErrors = true;
                    return;
                case UserLogInResults.LockedOut:
                    User user = this.userService.GetUserByUsername(this.Username);
                    this.Errors =
                        $"You have been locked out due to inputting your password incorrectly too many times. You can attempt to login again at {user.CannotLoginUntilDateTimeUtc} UTC";
                    this.HasErrors = true;
                    return;
                case UserLogInResults.WrongPassword:
                    this.Errors = "Your password is incorrect";
                    this.HasErrors = true;
                    return;
            }
        }

        /// <summary>
        /// The login return.
        /// </summary>
        private void LoginReturn()
        {
            this.IsNotLoggingIn = false;
            this.IsRegistering = false;
        }

        /// <summary>
        /// The user registration.
        /// </summary>
        private void UserRegistration()
        {
            this.IsNotLoggingIn = true;
            this.IsRegistering = true;
        }
    }
}