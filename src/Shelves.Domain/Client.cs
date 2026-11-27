using Shelves.Domain.Shared;
using System.Text.RegularExpressions;

namespace Shelves.Domain;

public partial class Client : BaseEntity
{
    public string NationalID { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;


    private Client(string name, string nationalID, string email, string phoneNumber) : base()
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        NationalID = nationalID;
    }

    [GeneratedRegex(@"^[0-9]{14}$")]
    private static partial Regex NationalIdRegex();

    public static ResultOf<Client> MakeNewClient(string name, string nationalID, string? email = null, string? phoneNumber = null)
    {
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Error.Validation(code: "Domain.Client.ClientShouldHaveEmailOrPhoneNumber", message: "At least one of Email or PhoneNumber is required.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(code: "Domain.Client.ClientNameIsRequired", message: "Client name is required.");
        }

        if (string.IsNullOrWhiteSpace(nationalID))
        {
            return Error.Validation(code: "Domain.Client.ClientNationalIDIsRequired", message: "Client national ID is required.");
        }

        if (!NationalIdRegex().IsMatch(nationalID))
        {
            return Error.Validation(code: "Domain.Client.InvalidNationalID", message: "National ID must be exactly 14 digits.");
        }

        return new Client(name, nationalID, email, phoneNumber);
    }

}
