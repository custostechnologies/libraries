namespace Custos.XrmHelpers.Shared.Validation
{
    public static class OrgAddressValidator
    {
        public static bool Validate(string address)
        {
            var domain = "@stpauls.nsw.edu.au";
            var addr = new System.Net.Mail.MailAddress(address);
            return addr.Address.Equals(address, StringComparison.OrdinalIgnoreCase)
                && address.EndsWith(domain, StringComparison.OrdinalIgnoreCase);
        }
    }
}