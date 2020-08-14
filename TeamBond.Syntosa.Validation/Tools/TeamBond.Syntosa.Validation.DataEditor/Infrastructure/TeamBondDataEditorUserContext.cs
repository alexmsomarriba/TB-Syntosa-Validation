namespace TeamBond.Syntosa.Validation.DataEditor.Infrastructure
{
    using System.Collections.Generic;

    using TeamBond.Domain.User;
    using TeamBond.Services.Users;

    /// <summary>
    /// The user context.
    /// </summary>
    public class TeamBondDataEditorUserContext : IUserContext
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamBondDataEditorUserContext"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public TeamBondDataEditorUserContext(IUserService userService)
        {
            this.userService = userService;
        }

        /// <inheritdoc />
        public User CurrentUser { get; set; }

        /// <inheritdoc />
        public IList<UserRole> CurrentUserRoles => this.userService.GetUserRoles(this.CurrentUser);
    }
}