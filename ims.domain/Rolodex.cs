using DevExpress.Xpo;

namespace ims.domain
{
    public class Rolodex : XPObject
    {
        public Rolodex(Session session) : base(session)
        {
        }

        string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetPropertyValue(nameof(FirstName), ref firstName, value);
        }

        string lastName;
        public string LastName
        {
            get => lastName;
            set => SetPropertyValue(nameof(LastName), ref lastName, value);
        }

        string phone;
        public string Phone
        {
            get => phone;
            set => SetPropertyValue(nameof(Phone), ref phone, value);
        }

        string email;
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }
    }
}