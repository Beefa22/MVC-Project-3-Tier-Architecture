using Demo.DAL.Models;

namespace Demo.Pl.Helper
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);
    }
}
