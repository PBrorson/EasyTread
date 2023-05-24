using EasyTread.Services.Interface.services;
using System.Text.RegularExpressions;

namespace EasyTread.Services.Services;

public class ValidationService : IValidationService
{
    private static readonly Regex SwedishLicensePlateRegex = new Regex(@"(?i)(^[A-Za-zÅÄÖåäö]{3} ?\d{2}[A-Za-z0-9ÅÄÖåäö]$)|(^\d{3,6}$)|^(?=.*[A-PR-UWYZÅÄÖa-pr-uwyzåäö0-9][A-PR-UWYZÅÄÖa-pr-uwyzåäö0-9])[A-PR-UWYZÅÄÖa-pr-uwyzåäö0-9 ]{2,6}[A-PR-UWYZÅÄÖa-pr-uwyzåäö0-9](?<!^O{2,6}|^0{2,6})$", RegexOptions.Compiled);

    public bool IsValidSwedishLicensePlate(string licensePlate)
    {
        return SwedishLicensePlateRegex.IsMatch(licensePlate);
    }
}