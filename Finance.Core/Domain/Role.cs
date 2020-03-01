namespace Finance.Core.Domain
{
    public class Role
    {

        public int RoleId { get; protected set; }
        public string RoleName { get; protected set; }


        protected Role()
        {

        }

        public Role(UserRole role)
        {
            RoleId = (int)role;
            RoleName = role.ToString();
        }
    }
}
