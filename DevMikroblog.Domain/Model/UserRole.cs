using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMikroblog.Domain.Model
{
    public sealed class UserRole
    {
        private readonly string _name;
        private readonly int _id;

        public static readonly UserRole Administrator = new UserRole("Administrator", 1);
        public static readonly UserRole User = new UserRole("User", 2);

        public UserRole(string name, int id)
        {
            this._name = name;
            this._id = id;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
